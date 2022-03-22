using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    [SerializeField] float runspeed;
    [SerializeField] float gravity;

    public List<int> floorKeys;

    private float horizontalInput;
    private float depthInput;
    private CharacterController characterController;
    private Animator animator;
    private bool isGrounded;
    private float yVelocityForGravity;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        floorKeys.Add(1); //set first floor key to 1

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //speed values:
        horizontalInput = Input.GetAxis("Horizontal") * runspeed;
        depthInput = Input.GetAxis("Vertical") * runspeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput) + Mathf.Abs(depthInput)); //abs bc either could be negative

        isGrounded = characterController.isGrounded;

        print("Grounded: " + isGrounded);

        if (isGrounded )
        {
            yVelocityForGravity = 0; //reset gravity application
        }
        else
        {
            //add gravity: (assumes 'gravity' is negative)
            yVelocityForGravity += gravity * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        //apply movement:
        characterController.Move(new Vector3(horizontalInput * Time.deltaTime, yVelocityForGravity * Time.deltaTime, depthInput * Time.deltaTime));
    }
}

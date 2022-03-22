using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //included to use '.Max()' on a list of its to find the largest int

public class ElevatorButtonTrigger : MonoBehaviour
{
    //private CharacterControllerScript interactingCharacter;

    private Elevator elevator;
    private int i;

    public int keyNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        elevator = FindObjectOfType<Elevator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        //player pressed elevator button:
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))  
        {
            i++;
            if (i % 2 == 1)
            {
                //change to taking input from a UI in the future so player can choose a floor if they have the correct key
                //finds largest key in list of player keys:
                keyNumber = other.GetComponent<PlayerItems>().floorKeys.Max();

                elevator.setFloor();

                print("keyNumber changed to: " + keyNumber);
            }
        }
    }
}

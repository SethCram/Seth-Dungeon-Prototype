using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    //private GameManager gameManager;

    private Elevator elevator;

    //ethan's collider doesn't flip triggers twice, unlike the capsule ones:
    private int i; //to makes sure only 1 char added to list for each char stepping on platform
    private int j;

    // Start is called before the first frame update
    void Start()
    {
        elevator = FindObjectOfType<Elevator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            //adds half of the characters that trigger it (bc counts 1 person as 2)
            //i++;
           // if (i % 2 == 1)
           // {
                elevator.players.Add(other.gameObject);
                print("Player added");
          //  }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        //print("Something left the elevator");
        if (other.CompareTag("Player"))
        {

            //i--;
            
           // if (i % 2 == 1)
            //{
                elevator.players.Remove(other.gameObject);
                print("Player removed");
           // }
        }
    }
}

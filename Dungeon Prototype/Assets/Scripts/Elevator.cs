using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] float elevatorSpeed;

    public List<GameObject> players;

    private float floorDepth = 0;
    private Levels levelsCollection;
    private ElevatorButtonTrigger elevatorButtonTrigger;
    private Transform elevatorTransform;
    private int currFloor;
    private int desiredFloor;
    private Rigidbody elevatorRigidbody;
    private bool shouldMoveDown;
    private float appliedElevatorSpeed = 0;
    private bool alreadyResetElevatorSpeed;
    private float elevatorMidpoint;
    private float elevatorUpperMidpoint;
    private float elevatorLowerMidpoint;

    // Start is called before the first frame update
    void Start()
    {
        elevatorButtonTrigger = FindObjectOfType<ElevatorButtonTrigger>();

        levelsCollection = FindObjectOfType<Levels>();

        elevatorTransform = GetComponent<Transform>();

        elevatorRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //called when player uses button:
    public void setFloor()
    {
        currFloor = desiredFloor;

        desiredFloor = elevatorButtonTrigger.keyNumber;

        print("desiredFloor is floor: " + desiredFloor);

        print("currFloor is floor: " + currFloor);

        floorDepth = levelsCollection.levels[desiredFloor].GetComponent<Transform>().position.y; //sets floorDepth to later test if we're at floor yet

        //sets move up or down based on current and desired floor:
        if(currFloor < desiredFloor)
        {
            shouldMoveDown = true;
        }
        else if( currFloor > desiredFloor)
        {
            shouldMoveDown = false;
        }

        //elevator midpnts set tween this and next floor:
        elevatorMidpoint = (elevatorRigidbody.position.y + floorDepth)/2;
        elevatorUpperMidpoint = (elevatorRigidbody.position.y + elevatorMidpoint) / 2;
        elevatorLowerMidpoint = (elevatorMidpoint + floorDepth) / 2;
    }

    private void FixedUpdate()
    {
        //move elevator down to appropriate floor if player's on elevator after 1 second:
        if (players.Count != 0)
        {
            alreadyResetElevatorSpeed = false;

            if (shouldMoveDown)
            {
                Invoke("MoveElevatorDown", 1); //only delays for 1 second first time players step on
            }
            else
            {
                Invoke("MoveElevatorUp", 1);
            }
        }
        else
        {
            //reset elevator speed while nobody is on it:
            if (alreadyResetElevatorSpeed == false)
            {
                resetAppliedElevatorSpeed();
            }
        }
    }

    private void resetAppliedElevatorSpeed()
    {
        appliedElevatorSpeed = 0;

        print("Elevator speed reset");

        alreadyResetElevatorSpeed = true;
    }

    private void MoveElevatorDown()
    {

        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, floorDepth, 0), 0.05f);


        /*
        foreach (GameObject onePlayer in players)
        {
            //translate each player at same speed as elevator:
            //onePlayer.GetComponent<CharacterController>().Move(transform.position + new Vector3(0, floorDepth, 0) * Time.deltaTime * elevatorSpeed);

            //playerTransform = onePlayer.GetComponent<Transform>();

            //desiredPlayerPositionDepth = new Vector3(0, floorDepth, 0) + playerTransform.position;

            //playerTransform.position = Vector3.MoveTowards(playerTransform.position, desiredPlayerPositionDepth, 0.05f);
        }
        ?*/

        //move down:
        if (elevatorTransform.position.y >= floorDepth)
        {

            //accelerate descent for first half, decelerate for 2nd half (make sure not to reverse travel direction):
            if (elevatorRigidbody.position.y >  elevatorUpperMidpoint)
            {
                appliedElevatorSpeed += elevatorSpeed * 0.1f;
            }
            else if (elevatorRigidbody.position.y > elevatorMidpoint)
            {
                appliedElevatorSpeed += elevatorSpeed * 0.3f;
            }
            else if (elevatorRigidbody.position.y > elevatorLowerMidpoint && appliedElevatorSpeed > elevatorSpeed)
            {
                appliedElevatorSpeed -= elevatorSpeed * 0.1f;
            }
            else if (elevatorRigidbody.position.y >  floorDepth && appliedElevatorSpeed > elevatorSpeed)
            {
                appliedElevatorSpeed -= elevatorSpeed * 0.3f;
            }

            /*
            print("elevator speed:" + elevatorSpeed);
            print("floor depth:" + floorDepth);
            print("elevator y coord:" + elevatorRigidbody.position.y);
            */

            //move elevator:
            //elevatorRigidbody.MovePosition(elevatorTransform.position + Vector3.down * Time.deltaTime * appliedElevatorSpeed);
            elevatorTransform.Translate(Vector3.down * Time.deltaTime * appliedElevatorSpeed);

            foreach (GameObject onePlayer in players)
            {
                //moves each player at same speed as elevator:
                if (onePlayer.GetComponent<CharacterController>() != null)
                {
                    onePlayer.GetComponent<CharacterController>().Move(elevatorTransform.position + Vector3.down * Time.deltaTime * appliedElevatorSpeed);
                }
                else
                {
                    onePlayer.GetComponent<Transform>().Translate(Vector3.down * Time.deltaTime * elevatorSpeed);
                }
                    //onePlayer.GetComponent<Transform>().Translate(Vector3.down * Time.deltaTime * elevatorSpeed);
            }

            //print("Elevator and player moving down to floordepth: " + floorDepth);
            print("applied elevator speed: " + appliedElevatorSpeed);
        }
    }
  private void MoveElevatorUp()
    { 
        //move up:
        if (elevatorTransform.position.y <= floorDepth)
        {
            
            //accelerate descent for first half, decelerate for 2nd half (make sure not to reverse travel direction):
            if (elevatorRigidbody.position.y < elevatorLowerMidpoint)
            {
                appliedElevatorSpeed += elevatorSpeed * 0.1f;
            }
            else if (elevatorRigidbody.position.y < elevatorMidpoint)
            {
                //appliedElevatorSpeed += elevatorSpeed * 0.3f;
            }
            else if (elevatorRigidbody.position.y < elevatorUpperMidpoint && appliedElevatorSpeed > elevatorSpeed)
            {
                //appliedElevatorSpeed -= elevatorSpeed * 0.1f;
            }
            else if (elevatorRigidbody.position.y < floorDepth && appliedElevatorSpeed > elevatorSpeed * 0.5)
            {
                appliedElevatorSpeed -= elevatorSpeed * 0.1f;
            }
            
            appliedElevatorSpeed = elevatorSpeed;

            //elevatorRigidbody.MovePosition(elevatorTransform.position + Vector3.up * Time.deltaTime * appliedElevatorSpeed);
            elevatorTransform.Translate(Vector3.up * Time.deltaTime * appliedElevatorSpeed);

            foreach (GameObject onePlayer in players)
            {
                //moves each player at same speed as elevator:
                if (onePlayer.GetComponent<CharacterController>() != null)
                {
                    onePlayer.GetComponent<CharacterController>().Move(elevatorTransform.position + Vector3.up * Time.deltaTime * appliedElevatorSpeed);
                }
                else
                {
                    onePlayer.GetComponent<Transform>().Translate(Vector3.up * Time.deltaTime * appliedElevatorSpeed);
                }
            }   

            print("applied elevator speed: " + appliedElevatorSpeed);
            //print("Elevator and player moving up to floordepth: " + floorDepth);
        }
    }
}

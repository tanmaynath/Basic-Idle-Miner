using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LiftController : MonoBehaviour
{

    Transform liftTransform;
    List<Vector3> floors = new List<Vector3>();     //Store positions of mining floors
    public float liftSpeed;
    int count = 1;
    public float amountInLift;

    int moveDirection = 1;      //Flag to check for direction. 1 for downward, -1 for upward

    // Use this for initialization
    void Start()
    {
        amountInLift = 0.0f;
        floors.Add(transform.localPosition);
        floors.Add(new Vector3(transform.localPosition.x, transform.localPosition.y - 150.0f, transform.localPosition.z));
        floors.Add(new Vector3(transform.localPosition.x, transform.localPosition.y - 300.0f, transform.localPosition.z));
    }

    // Update is called once per frame
    void Update()
    {
        TapToStart();
    }

    void TapToStart()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            try
            {
                liftTransform = hit.collider.gameObject.transform;
            }
            catch (NullReferenceException e) 
            { 
                Debug.Log("Caught stray touch" + e.ToString()); 
            }
        }
        if (liftTransform != null)
        {
            //Start Movement
            StartCoroutine(MoveLift());
        }
    }

    IEnumerator MoveLift()
    {
        var numberOfFloors = floors.Count;
        if (count < numberOfFloors)     //Till the lift doesnt reach the last floor
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, floors[count], Time.deltaTime * liftSpeed);
            if (transform.localPosition == floors[count])
            {
                count++;
            }
        }
        else    //When last floor is reached, go back to starting position and change direction flag
        {
            moveDirection = -1;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, floors[0], Time.deltaTime * liftSpeed);
            if (transform.localPosition == floors[0])
            {
                moveDirection = 1;
                count = 1;
            }
        }
        yield return null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "box")       //if lift enters the drop box, collect the amount in box and reset it to 0.
        {
            BoxController box = other.gameObject.GetComponent<BoxController>();
            if (moveDirection == 1)     //only collect value if lift is going down and then reset box amount to 0.
            {
                amountInLift += box.amountInBox;
                box.amountInBox = 0.0f;
            }
        }
    }


}
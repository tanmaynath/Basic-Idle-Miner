using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerping : MonoBehaviour {


    public float movementSpeed;
    public float miningSpeed;
    public float transferSpeed;
    public float amountMined;
    public int moveDirection = 1;
    Vector3 startPos;
    Vector3 endPos;
    Transform objectHit;
    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(startPos.x + 5.0f, startPos.y, startPos.z);
        Debug.Log("startPos: " + startPos);

        Debug.Log("endPos: " + endPos);

    }

    // Update is called once per frame
    void Update()
    {



        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            objectHit = hit.collider.gameObject.transform;


        }

        if (objectHit != null)
        {
            if (moveDirection == 1)
            {
                StartCoroutine(MoveMiner(endPos, movementSpeed));
            }
            else
                StartCoroutine(MoveMiner(startPos, movementSpeed));
        }

        //if (objectHit != null && objectHit.position == endPos)
        //{
        //    moveDirection = -1;
        //    Debug.Log("Reached: " + objectHit.localPosition);
        //    Debug.Log("param 2:" + objectHit.localPosition + " " + startPos + " " + Time.deltaTime * movementSpeed);
        //    transform.localPosition = Vector3.MoveTowards(transform.localPosition, startPos, Time.deltaTime * movementSpeed);

        //}
        Debug.Log("movedirection: " + moveDirection);
        //if (objectHit != null && moveDirection == -1)
        //{
        //    Debug.Log("opposite");
        //    StartCoroutine(MoveMiner(startPos, movementSpeed));
        //}


    }

    IEnumerator MoveMiner(Vector3 pos, float speed)
    {
        Debug.Log("param 1:" + objectHit.localPosition + " " + pos + " " + Time.deltaTime * speed);

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, pos, Time.deltaTime * speed);
        if(transform.localPosition == pos)
        {
            moveDirection *= -1;
        }

        yield return null;
    }
}

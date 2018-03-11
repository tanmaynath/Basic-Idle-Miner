using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MinerController : MonoBehaviour {


    public float movementSpeed;
    public float miningSpeed;   
    //public float transferSpeed;
    public float amountMined; 
    public int moveDirection = 1;   //1 for forward, -1 for backward
    Vector3 startPos;
    Vector3 posMine;    //position of mine
    Vector3 posBox;     //position of drop box
    Transform minerTransform;   //transform of miner character

    // Use this for initialization
    void Start()
    {
        startPos = transform.localPosition;     //Initial position of miner at start of the game
        posMine = new Vector3(startPos.x + 40.0f, startPos.y, startPos.z);      //Mine position, remains fixed
        posBox = new Vector3(startPos.x - 25.0f, startPos.y, startPos.z);       //drop box position, remains fixed
        amountMined = 0.0f;     //initial mined amount
        miningSpeed = 10.0f;    //base mining speed
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
                minerTransform = hit.collider.gameObject.transform;
            }
            catch (NullReferenceException e)
            {
                Debug.Log("Caught stray touch: " + e.ToString());
            }
        }

        if (minerTransform != null)
        {
            //Start Movement
            StartCoroutine(MoveMiner(posBox, posMine, movementSpeed));
        }
    }

    IEnumerator MoveMiner(Vector3 posA, Vector3 posB, float speed)
    {
        if (moveDirection == 1)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, posB, Time.deltaTime * speed);
            if (transform.localPosition == posB)    //wait at posB and then change direction to move towards drop box
            {
                yield return new WaitForSeconds(2);
                moveDirection = -1;
            }
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, posA, Time.deltaTime * speed);
            if (transform.localPosition == posA)    //wait at posA and then change direction to move back to mine
            {
                yield return new WaitForSeconds(2);
                moveDirection = 1;
            }
        }
        yield return null;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "mine")      //when reached mine, collect amount
        {
            amountMined += miningSpeed * Time.deltaTime;
        }  
    }
}

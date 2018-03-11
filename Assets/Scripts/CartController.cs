using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CartController : MonoBehaviour {


    Vector3 startPos;
    Vector3 storagePos;     //position of tank storage
    Vector3 liftPos;        //position of lift storage
    Transform cartTransform;    //transform of cart
    int moveDirection = 1;      //flag to check direction. 1 for moving towards lift storage, -1 for moving towards storage tank
    public float cartSpeed;
    public float amountInCart;
    public LiftStorageController liftStorageController;     //to collect value from lift storage

	// Use this for initialization
	void Start () {
        startPos = transform.localPosition;     //position of cart at the start of game
        liftPos = new Vector3(startPos.x - 78.0f, startPos.y, startPos.z);      //position of lift storage. Remains fixed
        storagePos = new Vector3(startPos.x + 40.0f, startPos.y, startPos.z);   //position of tank storage. Remains fixed
	}
	
	// Update is called once per frame
	void Update () {
        TapToStart();
	}

    void TapToStart()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            try
            {
                cartTransform = hit.collider.gameObject.transform;
            }
            catch (NullReferenceException e)
            {
                Debug.Log("Caught stray touch: " + e.ToString());
            }
        }
        if (cartTransform != null)
        {
            //Start Movement
            StartCoroutine(MoveCart(liftPos, storagePos, cartSpeed));
        }
    }

    IEnumerator MoveCart(Vector3 posA, Vector3 posB, float speed)
    {
        if(moveDirection == 1)      //Move towards lift storage
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, posA, Time.deltaTime * speed);
            if(transform.localPosition == posA)
            {
               yield return new WaitForSeconds(2);
                moveDirection = -1;
            }
        }
        else     //move towards tank storage
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, posB, Time.deltaTime * speed);
            if(transform.localPosition == posB)
            {
                yield return new WaitForSeconds(2);
                moveDirection = 1;
            }
        }
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "LiftStorage")       //collect amount from lift storage and reset its amount to 0.
        {
            amountInCart += liftStorageController.amountInLiftStorage;
            liftStorageController.amountInLiftStorage = 0.0f;
        }
    }
}

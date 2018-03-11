using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftStorageController : MonoBehaviour {

    public float amountInLiftStorage;
    public LiftController lift;
    public TextMesh liftStorageScore;
	// Use this for initialization
	void Start () {
        amountInLiftStorage = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        liftStorageScore.text = amountInLiftStorage.ToString(); //display amount in lift storage
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "lift")     //if lift enters, collect amount from lift and reset its amount to 0.
        {
            amountInLiftStorage += lift.amountInLift;
            amountInLiftStorage = Mathf.Floor(amountInLiftStorage);
            lift.amountInLift = 0.0f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour {

    public float amountInBox;
    public MinerController miner;
    public TextMesh boxScore;
	// Use this for initialization
	void Start () {
        amountInBox = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        boxScore.text =  amountInBox.ToString();        //Display amount in drop box
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "miner")     //if miner enters, collect amount from him and reset his amount
        {
            amountInBox += miner.amountMined;
            amountInBox = Mathf.Floor(amountInBox);
            miner.amountMined = 0.0f;
        }
    }
}

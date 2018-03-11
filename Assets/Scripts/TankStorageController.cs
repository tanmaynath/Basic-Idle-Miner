using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStorageController : MonoBehaviour {
    
    public float amountInStorage;
    public CartController cartController;
    public TextMesh tankStorageScore;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        tankStorageScore.text = amountInStorage.ToString();     //Display amount in the storage tank
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        //collect amount from cart and reset its amount to 0
        amountInStorage += cartController.amountInCart;
        amountInStorage = Mathf.Floor(amountInStorage);
        cartController.amountInCart = 0.0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {


    public TankStorageController tankStorageController;
    public float baseLevelUpValue;
    public float amountForLevelUp;
    GameObject[] miners;
    float minerSpeed;
	// Use this for initialization
	void Start () {
        baseLevelUpValue = 100.0f;
        amountForLevelUp = baseLevelUpValue;
	}
	
	// Update is called once per frame
	void Update () {
        if(tankStorageController.amountInStorage >= amountForLevelUp)
        {
            amountForLevelUp *= amountForLevelUp * 2.0f;
            LevelUpMiner();
        }
	}

    void LevelUpMiner()
    {
        miners = GameObject.FindGameObjectsWithTag("miner");
        foreach(GameObject miner in miners)
        {
            minerSpeed = miner.GetComponent<MinerController>().movementSpeed;
            minerSpeed += minerSpeed * 0.2f;
        }
    }
}

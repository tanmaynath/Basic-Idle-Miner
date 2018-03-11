using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPath : MonoBehaviour {


    public int moveDirection = 1; // 1 for forward, -1 for backward
    public int nextPoint = 0;
    public Transform[] wayPoints;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnDrawGizmos()
    {
        if(wayPoints == null || wayPoints.Length < 2)
        {
            return; //a path needs at least 2 points, so exit if a path is missing.
        }

        for (int i = 1; i < wayPoints.Length; i++)
        {
            Gizmos.DrawLine(wayPoints[i-1].position, wayPoints[i].position);
        }
    }


    public IEnumerator<Transform> GetNextWayPoint()
    {
        if(wayPoints == null || wayPoints.Length<1)
        {
            yield break; //If no way points are present, break out of coroutine.
        }


        while(true) 
        {
            yield return wayPoints[nextPoint];

            if (wayPoints.Length == 1)
            {
                continue;
            }

            if(nextPoint <=0) //Beginning of the path, we move forward
            {
                moveDirection = 1;
            }

            else if (nextPoint >= wayPoints.Length - 1) //End of path reached, move backwards
            {
                moveDirection = -1;
            }

            nextPoint = nextPoint + moveDirection;
        }

    }
}


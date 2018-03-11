using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {


    public SetPath path;
    public float speed = 0.1f;
    public float maxProximity = 0.1f;
    private IEnumerator<Transform> nextWayPoint;
	// Use this for initialization
	void Start () {
        if(path == null)
        {
            Debug.LogError("No path is set", gameObject);
            return;
        }

        nextWayPoint = path.GetNextWayPoint();
        nextWayPoint.MoveNext();

        if(nextWayPoint.Current == null)
        {
            Debug.LogError("No way points to follow", gameObject);
            return;
        }

        transform.position = nextWayPoint.Current.position;
	}
	
	// Update is called once per frame
	void Update () {

        if(nextWayPoint == null || nextWayPoint.Current == null)
        {
            return;
        }
        transform.position = Vector3.Lerp(transform.position, nextWayPoint.Current.position, Time.deltaTime * speed);

        float proximityToNextPoint = (transform.position - nextWayPoint.Current.position).sqrMagnitude;

        if(proximityToNextPoint < maxProximity)
        {
            nextWayPoint.MoveNext();
        }
	}
}

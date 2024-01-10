using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMoving : MonoBehaviour
{
    private Transform target;
    
    public float speed = 10f;
    public int waypointIndex = 0;


    private void Start()
    {
        target = Waypoints.points[0];
    }
    // Update is called once per frame
    void Update()
    {      
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed *Time.deltaTime,Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            
            GetnextWaypoint();
        }
    }
    void GetnextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex];
        transform.LookAt(target.position);
    }


    void EndPath()
    {
        Destroy(gameObject);
    }
}
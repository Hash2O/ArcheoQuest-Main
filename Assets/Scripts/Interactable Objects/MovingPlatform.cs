using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float platformSpeed;
    public Transform waypoint1;
    public Transform waypoint2;

    public Rigidbody platformRb;

    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = waypoint1.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = (targetPosition - platformRb.position).normalized;
        platformRb.MovePosition(platformRb.position + platformSpeed  * direction * Time.fixedDeltaTime);

        if(Vector3.Distance(platformRb.position, targetPosition) < 0.05f)
        {
            if(targetPosition == waypoint1.position) 
                targetPosition = waypoint2.position;
            else
                targetPosition = waypoint1.position;

        }

    }
}

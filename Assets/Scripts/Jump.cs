using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    Rigidbody xrOriginRb;
    public float upThrust = 20f;

    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        xrOriginRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            xrOriginRb.AddForce(transform.up * upThrust);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRevolutionManager : MonoBehaviour
{
    //Assign a GameObject in the Inspector to rotate around
    [SerializeField] GameObject target;
    [SerializeField] GameObject sphere;
    [SerializeField] float revolutionSpeed;

    void Start()
    {
        target = GameObject.Find("Sun");
        sphere = GameObject.Find("Sphere");
    }

    void Update()
    {
        Vector3 axis = sphere.transform.up;
        // Spin the object around the target at revolutionSpeed/second.
        transform.RotateAround(target.transform.position, axis, revolutionSpeed * Time.deltaTime);
    }
}

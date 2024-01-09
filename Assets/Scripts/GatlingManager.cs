using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GatlingManager : MonoBehaviour
{
    //Assign a GameObject in the Inspector to rotate around
    [SerializeField] GameObject target;
    [SerializeField] float revolutionSpeed;

    private void Start()
    {
        target = GameObject.Find("Gatling Gun");
        revolutionSpeed = 500;
    }

    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.forward, revolutionSpeed * Time.deltaTime);   
    }
}

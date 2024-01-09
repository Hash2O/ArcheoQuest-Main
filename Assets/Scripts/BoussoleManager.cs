using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoussoleManager : MonoBehaviour
{
    [SerializeField] Transform orientation;
    [SerializeField] APIManager apiManager;
    private float yAxis;

    // Start is called before the first frame update
    void Start()
    {
        yAxis = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float yAxis = apiManager.DirectionVent;
        //transform.LookAt(orientation.transform);
        if(yAxis != 0f)
        {
            transform.Rotate(0, yAxis, 0);
        }
    }
}

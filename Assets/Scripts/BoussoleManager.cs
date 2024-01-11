using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoussoleManager : MonoBehaviour
{
    [SerializeField] Transform orientation;
    [SerializeField] APIManager apiManager;
    private static float yAxis;

    // Start is called before the first frame update
    void Start()
    {
        yAxis = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        yAxis = apiManager.DirectionVent;
        //transform.LookAt(orientation.transform);
        if(yAxis != 0f)
        {
            transform.Rotate(0, yAxis, 0);
        }
    }
}

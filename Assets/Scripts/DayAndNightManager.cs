using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNightManager : MonoBehaviour
{
    public bool isNight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("X euler angles : " + transform.localEulerAngles.x);


        if (transform.localEulerAngles.x > 180) { 
            isNight = true;
            Debug.Log("Night falls."); 
        }
        else
        {
            isNight = false;
            Debug.Log("Sun rises.");
        }

    }
}

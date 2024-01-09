using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class BoatLightsManager : MonoBehaviour
{
    [SerializeField]
    private DayAndNightManager _dayAndNightManager;

    [SerializeField] 
    private Light[] _lights;

    // Start is called before the first frame update
    void Start()
    {
        _lights = gameObject.GetComponentsInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_dayAndNightManager.isNight)
        {
            Debug.Log("Night falls on the sea.");

            foreach (Light light in _lights)
            {
                light.GetComponent<Light>().enabled = true;
            }

        }
        else
        {

            Debug.Log("Sun rises over the sea.");

            foreach (Light light in _lights)
            {
                light.GetComponent<Light>().enabled = false;
            }
        }
    }

    
}

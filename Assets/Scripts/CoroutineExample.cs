using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
    // keep a copy of the executing script
    private IEnumerator coroutine;
    private float timer;

    // Use this for initialization
    void Start()
    {
        print("Starting " + Time.time);
        coroutine = WaitAndPrint(2.0f);
        StartCoroutine(coroutine);
        print("Done " + Time.time);
    }

    // print to the console every 3 seconds.
    // yield is causing WaitAndPrint to pause every 3 seconds
    public IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
            yield return new WaitForSeconds(waitTime);
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            yield return new WaitForSeconds(waitTime);
            print("WaitAndPrint " + Time.time);
        }
    }

    void Update()
    {
 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine(coroutine);
            print("Stopped " + Time.time);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            StartCoroutine(coroutine);
            print("Restarting " + Time.time);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingVehicles : MonoBehaviour
{
    private float speed = 10.0f;
    private float rightBound = 150.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (transform.position.x > rightBound)
        {
            Debug.Log("Vehicle destroyed");
            Destroy(gameObject);
        }
    }
}

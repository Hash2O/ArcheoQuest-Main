using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    [SerializeField] private Vector3 newPortal1;
    [SerializeField] private Vector3 newPortal2;

    // Start is called before the first frame update
    void Start()
    {
        newPortal1 = GameObject.Find("Portal 1").transform.position;
        newPortal2 = GameObject.Find("Portal 2").transform.position;

        
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        Vector3 otherPosition = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
        Debug.Log(otherPosition);
        */

        if(other.gameObject.CompareTag("Player"))
        {
            other.transform.position = newPortal2;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] BeatManager beatManager;
    // Start is called before the first frame update
    void Start()
    {
        transform.SetPositionAndRotation(transform.position, Random.rotation);
        GetComponent<Rigidbody>()?.AddForce(Random.insideUnitSphere, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * 2.0f);
        if (transform.position.z < -11)
        {
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{

    public Transform target;

    [Range(0f, 1f)]
    public float positionDamping;

    [Range(0f, 1f)]
    public float rotationDamping;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //slow down when close to the target
        transform.position = Vector3.Lerp(transform.position, target.position, positionDamping);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotationDamping);
    }
}

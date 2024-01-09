using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//URL : https://youtu.be/Ag_KaIQ5_TE
//Author : Justin P Barnett

//Inspirated from : https://youtu.be/9PxHzcWCAtY (Game Dev Chef)

[RequireComponent(typeof(Rigidbody))]
public class Swimmer : MonoBehaviour
{
    [Header("Values")]
    [SerializeField]
    private float swimForce = 2f;

    [SerializeField]
    private float dragForce = 1f;

    [SerializeField]
    private float minForce;

    [SerializeField]
    private float minTimeBetweenStrokes;


    [Header("References")]
    [SerializeField]
    InputActionReference leftControllerSwimReference;

    [SerializeField]
    InputActionReference rightControllerSwimReference;

    [SerializeField]
    InputActionReference leftControllerSwimVelocity;

    [SerializeField]
    InputActionReference rightControllerSwimVelocity;

    [SerializeField]
    private Transform trackingReference; //Head as forward direction

    Rigidbody _rigidbody;
    float _cooldownTimer;


    private void Awake()
    {
       _rigidbody = GetComponent<Rigidbody>();
       _rigidbody.useGravity = false;
       _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;    //to avoid a maximum motion sickness while swimming

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _cooldownTimer += Time.fixedDeltaTime;

        if(_cooldownTimer > minTimeBetweenStrokes && leftControllerSwimReference.action.IsPressed() && rightControllerSwimReference.action.IsPressed())
        {
            var leftHandVelocity = leftControllerSwimVelocity.action.ReadValue<Vector3>();
            var rightHandVelocity = rightControllerSwimVelocity.action.ReadValue<Vector3>();

            Vector3 localVelocity = leftHandVelocity + rightHandVelocity;

            localVelocity *= -1;    //Inverse to move forward

            if(localVelocity.sqrMagnitude > minForce * minForce)
            {
                Vector3 worldVelocity = trackingReference.TransformPoint(localVelocity);
                _rigidbody.AddForce(worldVelocity * swimForce, ForceMode.Acceleration);
                _cooldownTimer = 0;
            }
        }

        if(_rigidbody.velocity.sqrMagnitude > 0.01f)
        {
            _rigidbody.AddForce( - _rigidbody.velocity * dragForce, ForceMode.Acceleration );
        }


    }
}

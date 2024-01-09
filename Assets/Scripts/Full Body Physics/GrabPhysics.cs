using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabPhysics : MonoBehaviour
{
    public InputActionProperty grabInputSource;
    public float radius;
    public LayerMask grabLayer;

    private FixedJoint fixedJoint;
    private bool isGrabbing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool isGrabButtonPressed = grabInputSource.action.ReadValue<float>() > 0.1f;

        if(isGrabButtonPressed && !isGrabbing)
        {
            Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, radius, grabLayer, QueryTriggerInteraction.Ignore);

            if(nearbyColliders.Length > 0 )
            {
                Rigidbody nearbyRb = nearbyColliders[0].attachedRigidbody;

                fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.autoConfigureConnectedAnchor = false;

                if(nearbyRb)
                {
                    fixedJoint.connectedBody = nearbyRb;
                    fixedJoint.connectedAnchor = nearbyRb.transform.InverseTransformPoint(transform.position);  //form world position to local position
                }
                else
                {
                    fixedJoint.connectedAnchor = transform.position;    //To climb on walls, for example
                }

                isGrabbing = true;
            }
        }
        else if (!isGrabButtonPressed && isGrabbing)
        {
            isGrabbing = false;

            if(fixedJoint)
            {
                Destroy(fixedJoint);
            }
        }
    }
}

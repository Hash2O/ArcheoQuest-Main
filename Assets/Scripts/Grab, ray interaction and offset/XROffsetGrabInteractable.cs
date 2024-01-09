using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{

    private Vector3 initialLocalPosition;
    private Quaternion initialLocalRotation;

    // Start is called before the first frame update
    void Start()
    {
        if (!attachTransform)
        {
            GameObject attachPoint = new GameObject("Offset Grab Pivot");
            attachPoint.transform.SetParent(transform, false); // local position and rotation at (0,0,0)
            attachTransform = attachPoint.transform;
        }
        else
        {
            initialLocalPosition = transform.localPosition;
            initialLocalRotation = transform.localRotation;
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if(args.interactorObject is XRDirectInteractor)
        {
            attachTransform.position = args.interactorObject.transform.position;
            attachTransform.rotation = args.interactorObject.transform.rotation;
        }
        else
        {
            attachTransform.position = initialLocalPosition;
            attachTransform.rotation = initialLocalRotation;
        }


        //back to normal flow
        base.OnSelectEntered(args);
    }

}

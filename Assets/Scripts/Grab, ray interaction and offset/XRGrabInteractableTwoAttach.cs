using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//Or simply allow the Use Dynamix Attach on XR Grab Interactable

public class XRGrabInteractableTwoAttach : XRGrabInteractable
{

    public Transform leftAttachTransform;
    public Transform rightAttachTransform;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("Left Hand"))
        {
            attachTransform = leftAttachTransform;
        }
        else if (args.interactableObject.transform.CompareTag("Right Hand"))
        {
            attachTransform = rightAttachTransform;
        }


        //Return to the regular flow of the function's life cycle
        base.OnSelectEntered(args);
    }

}

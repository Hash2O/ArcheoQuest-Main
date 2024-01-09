using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PickupItem : MonoBehaviour
{
    [SerializeField]
    private InputActionProperty grabInputSource;

    [SerializeField]
    private float pickupRange = 10f;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private TextMeshProUGUI interactionText;

    private bool isGrabButtonPressed;

    public Inventory inventory;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, pickupRange, layerMask))
        {
            interactionText.gameObject.SetActive(true);

            if(hit.transform.CompareTag("Item"))
            {
                Debug.Log("Item spotted.");
            }

            if (isGrabButtonPressed)
            {
                Debug.Log("This object has a Item Component ? : " + hit.transform.gameObject.GetComponent<Item>().item);
            }

        }
        else
        {
            interactionText.gameObject.SetActive(false);
        }

    }


    private void FixedUpdate()
    {
        isGrabButtonPressed = grabInputSource.action.ReadValue<float>() > 0.1f;
    }
}

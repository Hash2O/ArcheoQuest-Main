using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    public Image itemImage;

    [HideInInspector]
    public Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag"); ;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);

        //Leave item on top of every object on the screen while dragging it
        transform.SetAsLastSibling();
        //Disabling raycast layer on itemImage to allow direct raycasting on inventory slots hovered while dragging
        itemImage.raycastTarget = false;


    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        transform.SetParent(parentAfterDrag);

        //Enabling raycast layer again to allow interaction when item has been dropped in ew inventory slot
        itemImage.raycastTarget = true;
    }

}

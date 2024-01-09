using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //Checking if slot is empty first
        if(transform.childCount == 0)
        {
            //Item variable initialization
            GameObject currentlyDraggedItem = eventData.pointerDrag;
            //Getting component from currently dragged item 
            DraggableItem draggableItem = currentlyDraggedItem.GetComponent<DraggableItem>();
            //Giving new Transform to item
            draggableItem.parentAfterDrag = transform;
        }

    }


}

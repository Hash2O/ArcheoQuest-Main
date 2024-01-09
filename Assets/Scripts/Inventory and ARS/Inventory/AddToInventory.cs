using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddToInventory : MonoBehaviour
{
    public Inventory inventory;

    private void OnTriggerEnter(Collider other)
    {
        if(inventory.IsFull())
        {
            return;
        }
        else if (other.gameObject.CompareTag("Item"))
        {
            inventory.AddItem(other.gameObject.GetComponent<Item>().item);
            Destroy(other.gameObject);
        }
    }
}

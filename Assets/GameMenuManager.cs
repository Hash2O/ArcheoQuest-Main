using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    public Transform head;
    public float spawnDistance = 1;
    public GameObject menu;
    public InputActionProperty showMenuButton;

    // Update is called once per frame
    void Update()
    {
        //switch Menu On/Off with Left Touch
        if(showMenuButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);

            //Place the menu in front of the player (remains static)
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }

        //Make the menu look at the player when spawning. 
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        //if needed if menu shows his back to the player
        menu.transform.forward *= -1;
    }
}

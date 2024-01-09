using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //Variable attached to the camera
    public GameObject player;
    //Variable used to modify valors of camera's position over time if needed
    private Vector3 offset = new Vector3(0, 9, -10);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // LateUpdate is called after Update, 
    // here making the camera always moving after the vehicle, 
    // for a smoother following sequence
    void LateUpdate()
    {
        //Camera link on player's position (which evolves in time)
        transform.position = player.transform.position + offset;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarbourTriggerZoneManager : MonoBehaviour
{
    [SerializeField] SailingWindSimulationManager player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Quand le navire rentre au port, il utilise son moteur
        if(other.gameObject.tag == "Player")
        {
            if(player.isWindSailing == true)
            {
                player.isWindSailing = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Quand le navire sort du port, il utilise ses voiles
        if (other.gameObject.tag == "Player")
        {
            if (player.isWindSailing == false)
            {
                player.isWindSailing = true;
            }
        }
    }
}

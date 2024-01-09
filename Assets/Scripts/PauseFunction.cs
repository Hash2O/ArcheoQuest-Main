using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//URL : https://medium.com/@jdpetta21/unity-ui-pause-menu-and-volume-brightness-settings-eb8ef51673f9

public class PauseFunction : MonoBehaviour
{

    private void OnEnable()
    {
        Debug.Log("Enabled");
        Time.timeScale = 0.0f;
    }

    private void OnDisable()
    {
        Debug.Log("Disabled");
        //Time is going 5xtimes slower than "normal" time
        Time.timeScale = 1.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

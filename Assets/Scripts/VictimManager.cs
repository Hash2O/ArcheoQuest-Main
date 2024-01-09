using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimManager : MonoBehaviour
{
    private Animator victimAnim;
    // Start is called before the first frame update
    void Start()
    {
        victimAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(victimAnim != null)
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                victimAnim.SetTrigger("TriggerIdle");
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                victimAnim.SetTrigger("TriggerWalk");
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                victimAnim.SetTrigger("TriggerRun");
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                victimAnim.SetTrigger("TriggerRoll");
            }
        }


    }
}

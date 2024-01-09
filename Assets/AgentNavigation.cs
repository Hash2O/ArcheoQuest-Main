using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentNavigation : MonoBehaviour
{

    [SerializeField]
    private Vector3 _desiredDestination;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NavMeshAgent>().destination = _desiredDestination;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

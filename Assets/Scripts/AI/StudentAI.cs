using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StudentAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform player;

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private Animator animator;

    [Header("Stats")]
    [SerializeField]
    private float detectionRange;

    [SerializeField]
    private float interactionRange;

    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float runSpeed;

    [SerializeField]
    private float interactionDelay;

    [SerializeField]
    private float rotationSpeed;

    private float distanceToPlayer;

    private bool hasDestination;
    private bool isInteracting;

    [Header("Wandering Values")]
    [SerializeField]
    private float minWaitingValue;

    [SerializeField]
    private float maxWaitingValue;

    [SerializeField]
    private float minWanderingValue;

    [SerializeField]
    private float maxWanderingValue;


    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer < detectionRange)
        {
            agent.speed = walkSpeed;

            /*
            Quaternion lookAtPlayer = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAtPlayer, rotationSpeed * Time.deltaTime);
            */

            if(!isInteracting) { 

                if (distanceToPlayer < interactionRange)
                {
                    StartCoroutine(InteractWithPlayer());
                }
                else
                {
                    agent.SetDestination(player.position);
                }
            }
        }
        else
        {
            agent.speed = walkSpeed;

            if (agent.remainingDistance < 0.75f && !hasDestination)
            {
                StartCoroutine(GetNewDestination());
            }
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    IEnumerator GetNewDestination()
    {
        hasDestination = true;
        yield return new WaitForSeconds(Random.Range(minWaitingValue, maxWaitingValue));

        Vector3 nextDestination = transform.position;
        nextDestination += Random.Range(minWanderingValue, maxWanderingValue) * new Vector3(Random.Range(-1f, 1), 0, Random.Range(-1f, 1)).normalized;

        NavMeshHit hit;
        if(NavMesh.SamplePosition(nextDestination, out hit, maxWanderingValue, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        hasDestination = false;
    }

    IEnumerator InteractWithPlayer()
    {
        isInteracting = true;
        agent.isStopped = true;

        animator.SetTrigger("Interact");

        yield return new WaitForSeconds(interactionDelay);

        agent.isStopped = false;
        //isInteracting = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}

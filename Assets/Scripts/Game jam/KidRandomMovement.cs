using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KidRandomMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float wanderRadius = 5.0f;
    public GameObject player;

    private Vector3 wanderPoint;
    private bool isPaused = false;
    private float pauseTimer = 5.0f;

    void Start()
    {
        wanderPoint = GetRandomWanderPoint();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= 5.0f)
            {
                if (!isPaused)
                {
                    // Se diriger vers le joueur
                    if (distanceToPlayer > 1.0f)
                    {
                        transform.LookAt(player.transform);
                        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                    }
                    else
                    {
                        // Arrêter devant le joueur
                        isPaused = true;
                    }
                }
                else
                {
                    // En pause devant le joueur
                    pauseTimer -= Time.deltaTime;
                    if (pauseTimer <= 0.0f && distanceToPlayer <= 1.0f)
                    {
                    Debug.Log("Time to go");
                        isPaused = false;
                        wanderPoint = GetRandomWanderPoint();
                    }
                }
            }
            else
            {
                // Se déplacer de façon aléatoire
                float distanceToWanderPoint = Vector3.Distance(transform.position, wanderPoint);

                if (distanceToWanderPoint <= 1.0f)
                {
                    wanderPoint = GetRandomWanderPoint();
                }
                else
                {
                    transform.LookAt(wanderPoint);
                    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                }
            }
    }

    Vector3 GetRandomWanderPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
        return hit.position;
    }
}

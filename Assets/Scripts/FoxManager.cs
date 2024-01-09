using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoxManager : MonoBehaviour
{

    [SerializeField] Transform _destination;

    [SerializeField] List<Transform> _destinations;

    NavMeshAgent _agent;

    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(_destination.position);
        //_animator.SetFloat("vitesse", _agent.speed);

        if (_agent.remainingDistance < 0.1f && _destinations.Count > 0)
        {
            _animator.SetBool("isWalking", true);

            //On définit la nouvelle définition en prenant la première de la liste
            _destination = _destinations[0];

            //Ensuite on la supprime de la liste, pour faire remonter le reste de la liste
            _destinations.RemoveAt(0);

            //On envoie le GameObject à la destination sélectionnée
            _agent.SetDestination(_destination.position);
        }
        else if (_agent.remainingDistance < 0.1f && _destinations.Count <= 0)
        {
            _animator.SetBool("isWalking", false);
        }

    }
}

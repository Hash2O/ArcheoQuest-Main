using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CivilManager : MonoBehaviour
{
    private NavMeshAgent _citizenAgent;

    //[SerializeField] private Transform _destination;

    [SerializeField] private Transform _player;
    [SerializeField] private Transform _zombie;
    [SerializeField] private AudioSource _citizenAudioSource;
    [SerializeField] private bool isIdle;
    [SerializeField] private bool isRunning;

    private Animator _animatorCitizen;

    // Start is called before the first frame update
    void Start()
    {
        _citizenAgent = GetComponent<NavMeshAgent>();
        _citizenAudioSource = GetComponent<AudioSource>();
        _player = GameObject.FindWithTag("Player").transform;
        _zombie = GameObject.FindWithTag("Zombie").transform;
        _animatorCitizen = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        keepYourDistance();
        callForHelp();
    }

    void keepYourDistance()
    {
        var safeDist = Vector3.Distance(transform.position, _player.position);
        var riskyDist = Vector3.Distance(transform.position, _zombie.position);

        if (safeDist < 25.0f)
        {
            _citizenAgent.SetDestination(_player.position);
            _animatorCitizen.SetBool("isRunning", true);
        }
        else
        {
            _animatorCitizen.SetBool("isIdle", true);
        }

        if (riskyDist < 25.0f)
        {
            _citizenAgent.SetDestination(_player.position);
            _animatorCitizen.SetBool("isRunning", true);
            _citizenAudioSource.Play();
        }
        else
        {
            _animatorCitizen.SetBool("isIdle", true);
        }


    }

    void callForHelp()
    {
        var safeDist = Vector3.Distance(transform.position, _player.position);
        if (safeDist < 1.0f)
        {
            _animatorCitizen.SetBool("isIdle", true);
            Debug.Log("HELP !");
            _citizenAudioSource.PlayDelayed(0.1f);
        }
    }
}

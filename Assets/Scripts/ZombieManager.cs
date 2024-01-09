using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{
    private NavMeshAgent _agent;

    //[SerializeField] private Transform _destination;

    [SerializeField] private Transform _player;

    [SerializeField] private PlayerStats _playerStats;

    private Animator _animatorAnim;

    private float _damage = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindWithTag("Player").transform;
        _playerStats = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerStats>();
            //GetComponent<PlayerStats>();

        _animatorAnim = GetComponent<Animator>();

        //_agent.SetDestination(_player.position);
    }

    // Update is called once per frame
    void Update()
    {
        var dist = Vector3.Distance(transform.position, _player.position);

        if(dist > 2.0f)
        {
            //Au delà d'un mètre, le zombie marche vers lui
            _agent.SetDestination(_player.position);
            _animatorAnim.SetBool("isAttacking", false);
        }
        else
        {
            //_agent.isStopped = true;
            //Moins d'un mêtre, le zombie enclenche l'anim d'attaque
            _animatorAnim.SetBool("isAttacking", true);
                       
        }

        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            print("Collision");
            StartCoroutine(MakingDamage());
        }
        
    }

    IEnumerator MakingDamage()
    {
        yield return new WaitForSeconds(1.0f);
        print("Player has been hit !");
        _playerStats.TakeDamage(_damage);
    }
}

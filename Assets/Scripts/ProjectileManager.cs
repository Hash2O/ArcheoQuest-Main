using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionParticle;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Zombie"))
        {
            Debug.Log("That's a hit !");
            Destroy(gameObject);
            Destroy(collision.gameObject);
            Instantiate(_explosionParticle, transform.position, transform.rotation);
            
        }
    }
}

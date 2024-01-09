using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballManager : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleEffects;

    private AudioSource _fireAudiosource;

    [SerializeField] List<AudioClip> _fireClips;


    void Start()
    {
        _fireAudiosource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Zombie")
        {
            print("Hit");
            _fireAudiosource.clip = _fireClips[0];
            _fireAudiosource.Play();
            Instantiate(_particleEffects, transform.position, transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject, 1f);
        }
    }
}

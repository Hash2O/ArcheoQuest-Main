using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickManager : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private List<AudioClip> _kickAudioclips;

    [SerializeField] float _kickThrust;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zombie")
        {
            print("This Is Sparta !");
            _audioSource.clip = _kickAudioclips[1];
            _audioSource.Play();
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            enemyRb.AddForce(transform.up * _kickThrust, ForceMode.Impulse);
            Destroy(other.gameObject, 2.0f);
        }
    }
}

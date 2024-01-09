using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private List<AudioClip> _swordAudioclips;

    [SerializeField] private GameObject _ashesPrefab;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
   
    private void OnTriggerEnter(Collider other)
    {
        /*
        //print(other.gameObject.name + " est entré en contact avec " + gameObject.name);
        if(other.gameObject.name != "Orange Fireball(Clone)")
        {
            _audioSource.clip = _swordAudioclips[0];
            _audioSource.Play();
        }
        */

        if (other.gameObject.tag == "Zombie")
        {
            _audioSource.clip = _swordAudioclips[1];
            _audioSource.Play();
            Instantiate(_ashesPrefab, transform.position, transform.rotation);
            Destroy(other.gameObject);
        }
    }
    
}

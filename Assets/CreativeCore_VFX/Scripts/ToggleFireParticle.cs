using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]

public class ToggleFireParticle : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.Space;

    private ParticleSystem fireParticle;
    public ParticleSystem igniteParticle;
    public ParticleSystem extinguishParticle;
    public GameObject pointLight;

    public AudioSource fireplaceAudiosource;
    public List<AudioClip> fireplaceSounds;

    bool isPlaying = true;

    private void Start()
    {
        fireParticle = GetComponent<ParticleSystem>();
        fireplaceAudiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if(isPlaying)
            {
                fireParticle.Stop();
                Debug.Log("Fire Stop");

                fireplaceAudiosource.clip = fireplaceSounds[0];
                fireplaceAudiosource.Play();

                pointLight.SetActive(false);
                if (extinguishParticle != null)
                    extinguishParticle.Play();
                
                isPlaying = false;
            } 
            else
            {
                fireplaceAudiosource.clip = fireplaceSounds[1];
                fireplaceAudiosource.Play();

                StartCoroutine(IgniteFire(1));

                isPlaying = true;
            }
        }
    }

    IEnumerator IgniteFire(float second)
    {
        yield return new WaitForSeconds(second);
        fireParticle.Play();
        Debug.Log("Fire Play");

        pointLight.SetActive(true);
        if (igniteParticle != null)
            igniteParticle.Play();
    }
}

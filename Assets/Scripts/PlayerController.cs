using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private InputActionReference _actionSwordStrike;
    [SerializeField] private InputActionReference _actionKickStrike;
    [SerializeField] private InputActionReference _actionMagicStrike;

    [SerializeField] private float _force;

    [SerializeField] private GameObject _fireballPrefab;
    [SerializeField] Transform firePoint;

    private AudioSource _audioSource;

    [SerializeField] private List<AudioClip> _kickAudioclips;
    [SerializeField] private List<AudioClip> _swordAudioclips;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        _actionSwordStrike.action.Enable(); //On active la fonction
        _actionSwordStrike.action.performed += OnSwordStrike;

        _actionKickStrike.action.Enable(); //On active la fonction
        _actionKickStrike.action.performed += OnKickStrike;

        _actionMagicStrike.action.Enable(); //On active la fonction
        _actionMagicStrike.action.performed += OnMagicStrike;

        _audioSource = GetComponent<AudioSource>();
 
    }


    void OnSwordStrike(InputAction.CallbackContext obj)
    {
        //print("Strike");
        anim.SetInteger("attackIndex", Random.Range(0, 5));
        anim.SetTrigger("Attack");
        StartCoroutine(PlaySwordSound());
    }

    IEnumerator PlaySwordSound()
    {
        yield return new WaitForSeconds(0.5f);
        _audioSource.clip = _swordAudioclips[0];
        _audioSource.Play();
    }

    void OnKickStrike(InputAction.CallbackContext obj)
    {
        print("Kick");
        anim.SetInteger("kickIndex", Random.Range(0, 2));
        anim.SetTrigger("Kick");
        StartCoroutine(PlayKickSound());

    }

    IEnumerator PlayKickSound()
    {
        yield return new WaitForSeconds(0.5f);
        _audioSource.clip = _kickAudioclips[0];
        _audioSource.Play();
    }

    void OnMagicStrike(InputAction.CallbackContext obj)
    {
        //print("Kamehameha");
        anim.SetInteger("magicIndex", Random.Range(0, 2));
        anim.SetTrigger("Magic");
        //timeToFire = Time.time + 1 / fireRate;
        castFireball();
    }

    void castFireball()
    {
        InstantiateFireball(firePoint); 
    }

    void InstantiateFireball(Transform firePoint)
    {
        GameObject Fireball = Instantiate(_fireballPrefab, firePoint.position, firePoint.rotation);
        Fireball.GetComponent<Rigidbody>().AddForce(firePoint.forward * _force);
         Destroy(Fireball, 3.0f);
    }
}

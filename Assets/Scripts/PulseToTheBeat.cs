using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseToTheBeat : MonoBehaviour
{

    [SerializeField] bool _useTestBeat;
    [SerializeField] float _pulseSize = 1.15f;
    [SerializeField] float _returnSpeed = 5f;

    //Spawn de cubes
    [SerializeField] GameObject prefab;

    private Vector3 _startSize;
    // Start is called before the first frame update
    void Start()
    {
        _startSize = transform.localScale;
        if(_useTestBeat)
        {
            StartCoroutine(TestBeat());
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, _startSize, Time.deltaTime * _returnSpeed);
    }

    public void Pulse()
    {
        transform.localScale = _startSize * _pulseSize;

        //Spawn de cubes
        Instantiate(prefab, transform.position, Quaternion.identity);
    }

    IEnumerator TestBeat()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Pulse();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessLoop : MonoBehaviour
{

    [SerializeField] GameObject item;
    [SerializeField] Transform _spawnPoint;
    Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
         _rb = item.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (item.transform.position.y <= 0.25f)
        {
            Debug.Log("Flying high again");
            item.transform.position = _spawnPoint.position;
            _rb.velocity.Set(0, 0, 0);
        }
    }
}

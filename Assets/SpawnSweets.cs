using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSweets : MonoBehaviour
{

    [SerializeField] private GameObject sweet;

    [SerializeField] private float _thrust;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            int random = Random.Range(2, 6);
            for(int i = 0; i < random; i++)
            {
                Spawn(sweet);
            }
            
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("ToothPaste"))
        {
            int random = Random.Range(2, 6);
            for(int i = 0; i < random; i++)
            {
                Spawn(sweet);
            }
            collision.gameObject.SetActive(false);
        }
        
    }
    

    protected void Spawn(GameObject newGo)
    {
        if (newGo == null)
            return;
        Instantiate(newGo);
        newGo.transform.SetPositionAndRotation(transform.position, Random.rotation);
        newGo.GetComponent<Rigidbody>()?.AddForce(Random.insideUnitSphere * _thrust, ForceMode.Impulse);
    }
}

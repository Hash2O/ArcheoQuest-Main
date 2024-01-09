using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 'client'
public class ElementSpawner : MonoBehaviour
{
    private ElementFactory _currentFactory = null;

    public void SetFactory(ElementFactory factory)
    {
        _currentFactory = factory;
    }

    public void CreateCube()
    {
        GameObject go = _currentFactory?.CreateCube();
        Spawn(go);
    }
    
    public void CreateCylinder()
    {
        GameObject go = _currentFactory?.CreateCylinder();
        Spawn(go);
    }

    public void CreateSphere()
    {
        GameObject go = _currentFactory?.CreateSphere();
        Spawn(go);
    }

    protected void Spawn(GameObject newGo)
    {
        if (newGo == null)
            return;
        
        newGo.transform.SetPositionAndRotation(transform.position, Random.rotation);
        newGo.GetComponent<Rigidbody>()?.AddForce(Random.insideUnitSphere, ForceMode.Impulse);
    }
}

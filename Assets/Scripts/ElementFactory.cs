using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementFactory : MonoBehaviour
{
    [SerializeField] 
    private GameObject _cubePrefab;
    [SerializeField] 
    private GameObject _cylinderPrefab;
    [SerializeField] 
    private GameObject _spherePrefab;
   
    
    public GameObject CreateCube()
    {
        return CreateElement(_cubePrefab);
    }

    public GameObject CreateCylinder()
    {
        return CreateElement(_cylinderPrefab);
    }
    
    public GameObject CreateSphere()
    {
        return CreateElement(_spherePrefab);
    }

    protected GameObject CreateElement(GameObject prefab)
    {
        if (prefab == null)
            return null;
        
        return Instantiate(prefab);
    }
    
 
}

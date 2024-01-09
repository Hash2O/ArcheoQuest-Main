using UnityEngine;
using System.Collections;

public class ObjectBuilderScript : MonoBehaviour
{
    public GameObject prefab;
    public Vector3 spawnPoint;


    public void BuildObject()
    {
        Instantiate(prefab, spawnPoint, Quaternion.identity);
    }
}

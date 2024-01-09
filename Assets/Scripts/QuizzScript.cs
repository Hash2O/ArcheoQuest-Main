using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizzScript : MonoBehaviour
{
    /*
    List<GameObject> objectList = new List<GameObject>();
    GameObject[] objectArray = new GameObject[]; //Mal déclarée
    Dictionary<string, GameObject> objectDict = new Dictionary<string, GameObject>();
    bool isTrue = true;
    */

    string playerName = "Frank";
    void Start()
    {
        Debug.Log("Hello: " + gameObject.name + playerName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

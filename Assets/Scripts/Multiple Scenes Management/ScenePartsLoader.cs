using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CheckMethod
{
    Distance,
    Trigger
}

public class ScenePartsLoader : MonoBehaviour
{

    public Transform player;
    public CheckMethod checkMethod;
    public float loadRange;

    //Scene state
    private bool isLoaded;
    private bool shouldLoad;

    // Start is called before the first frame update
    void Start()
    {
        //To avoid a scene to open twice in a row
        if(SceneManager.sceneCount > 0)
        {
            Debug.Log("SceneManager.sceneCount > 0");
            for(int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if(scene.name == gameObject.name)
                {
                    isLoaded = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(checkMethod == CheckMethod.Distance)
        {
            DistanceCheck();
        }
        else if(checkMethod == CheckMethod.Trigger)
        {
            TriggerCheck();
        }
    }

    private void DistanceCheck()
    {
        if (Vector3.Distance(player.position, transform.position) < loadRange)
        {
            Debug.Log("Within loading range");
            LoadScene();
        }
        else
        {
            Debug.Log("Leaving loading range");
            UnLoadScene();
        }
    }

    private void LoadScene()
    {
        if(!isLoaded)
        {
            Debug.Log("LoadScene " + gameObject.name + " : Done");
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            isLoaded = true;
        }
    }

    private void UnLoadScene()
    {
        if(isLoaded)
        {
            Debug.Log("UnLoadScene " + gameObject.name + " : Done");
            SceneManager.LoadSceneAsync(gameObject.name);
            isLoaded = false;
        }
    }


    private void TriggerCheck()
    {
        if(shouldLoad)
        {
            LoadScene();
        }
        else
        {
            UnLoadScene();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Entering trigger...");
            shouldLoad = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Leaving trigger...");
            shouldLoad = false;
        }
    }
}

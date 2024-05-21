using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        HideMenu();
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Scene Management Test 2", LoadSceneMode.Additive));
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Scene Management Test 3", LoadSceneMode.Additive));
    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    [SerializeField]
    private Animator transition;

    [SerializeField]
    private float transitionTime = 1.5f;

    [SerializeField]
    private int requiredLevel;

    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();
        }
    }
    

    public void LoadNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex <= 0)
        {
            Debug.Log("level : " + requiredLevel);
            StartCoroutine(LoadLevel(requiredLevel));
        }
        else
        {
            StartCoroutine(LoadLevel(0));
        }
        
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        transition.SetTrigger("Start");

        //Wait for smoothness
        yield return new WaitForSeconds(transitionTime);

        //Load Scene effectively
        SceneManager.LoadScene(levelIndex);
    }
}

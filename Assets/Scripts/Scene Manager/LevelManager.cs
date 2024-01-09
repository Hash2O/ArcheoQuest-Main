using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;

    private float _target;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
            
    }
    
    public async void LoadScene(string sceneName)
    {
        //Resetting variables
        _target = 0;
        _progressBar.fillAmount = 0;

        //Initializing scene name
        var scene = SceneManager.LoadSceneAsync(sceneName);

        //Preventing the scene to activate automatically
        scene.allowSceneActivation = false;

        //Activating the canvas to show the progress bar
        _loaderCanvas.SetActive(true);

        do
        {
            //artificially delay the process, here one second or so
            await Task.Delay(1000);

            _target = scene.progress;

        } while (scene.progress < 0.9f);    //In Unity, the scene is ready when 90% is reached

        //Return to normal life cycle
        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        //Making the progress bar being filling a little slower/softer
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, 3 * Time.deltaTime);  
    }
}

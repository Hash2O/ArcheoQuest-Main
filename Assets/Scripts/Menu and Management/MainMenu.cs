using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject optionsPanel;
    
    [SerializeField]
    private Button loadGameButton;

    [SerializeField]
    private Button clearSavedDataButton;

    [SerializeField]
    private Dropdown resolutionsDropdown;

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private Slider soundSlider;

    [HideInInspector]
    public static bool loadSavedData;

    // Start is called before the first frame update
    void Start()
    {
        //Initialisation du slider gérant le volume
        audioMixer.GetFloat("Volume", out float soundValueForSlider);
        soundSlider.value = soundValueForSlider;

        bool savedFileExist = System.IO.File.Exists(Application.persistentDataPath + "/SavedData.json");
        loadGameButton.interactable = savedFileExist;
        clearSavedDataButton.interactable = savedFileExist;

        Resolution[] resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();

        List<string> resolutionOptions = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x "  + resolutions[i].height + " (" + resolutions[i].refreshRate + " Hz)";
            resolutionOptions.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }

        }

        resolutionsDropdown.AddOptions(resolutionOptions);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();

    }

    // Update is called once per frame
    public void NewGameButton()
    {
        loadSavedData = false;
        SceneManager.LoadScene("The Trapped Labyrinth");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void LoadGameButton()
    {
        loadSavedData = true;
        SceneManager.LoadScene("The Trapped Labyrinth");
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = Screen.resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void ClearSavedData()
    {
        System.IO.File.Delete(Application.persistentDataPath + "/SavedData.json");
        loadGameButton.interactable = false;
        clearSavedDataButton.interactable = false;
    }

    public void EnableDisableOptionsPanel()
    {
        optionsPanel.SetActive(!optionsPanel.activeSelf);
    }
}

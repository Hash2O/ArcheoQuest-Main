using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Slider _brightnessSlider;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private AudioMixer _volumeMixer;
    [SerializeField] private Image _overLay;

    private bool _isPauseMenuActive = false;

    // Start is called before the first frame update
    void Start()
    {
        _pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseMenuSwitch();
        }

        DarkOverlay();
        AdjustVolume();
    }

    private void PauseMenuSwitch()
    {
        if(!_isPauseMenuActive)
        {
            _pauseMenu.SetActive(true);
            _isPauseMenuActive = true;
        }
        else if (_isPauseMenuActive)
        {
                DisableMainMenu();
            _isPauseMenuActive = false;
        }
    }

    private void DisableMainMenu()
    {
        _pauseMenu.SetActive(false);
    }

    private void AdjustVolume()
    {
        Debug.Log("Adjusting Volume...");
        _volumeMixer.SetFloat("BG_Music", _volumeSlider.value);
    }

    private void DarkOverlay()
    {
        var tempColor = _overLay.color;
        tempColor.a = _brightnessSlider.value;
        _overLay.color = tempColor;
    }
}

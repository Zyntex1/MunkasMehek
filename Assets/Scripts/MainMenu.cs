using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject main;
    [SerializeField] GameObject settings;

    [SerializeField] Slider sensitivity;
    [SerializeField] Slider volume;

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Main()
    {
        main.SetActive(true);
        settings.SetActive(false);
    }

    public void SettingsMenu()
    {
        main.SetActive(false);
        settings.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetSensitivity()
    {
        Settings.sensitivity = 1.0f + sensitivity.value * 19.0f;
    }

    public void SetVolume()
    {
        Settings.volume = volume.value;
    }
}

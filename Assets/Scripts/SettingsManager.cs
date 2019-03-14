using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public Toggle highSettingsToggle;
    public Toggle lowSettingsToggle;

    void Start()
    {
        settingsPanel.SetActive(false);
    }

    void OnEnable()
    {
        highSettingsToggle.isOn = PlayerPrefManager.Quality == 1;
        lowSettingsToggle.isOn = PlayerPrefManager.Quality == 0;
    }

    public void SetQualitySettings(int qualityLevel)
    {
        PlayerPrefManager.Quality = qualityLevel;
        ToggleQualityPanel();
    }

    public void ToggleQualityPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
    }
}

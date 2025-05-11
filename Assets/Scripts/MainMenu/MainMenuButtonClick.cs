using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class MainMenuButtonClick : MonoBehaviour
{
    [SerializeField] private GameObject _settingsWindow;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _soundVolume;

    [Inject] private SettingStorage _settingStorage;
    [Inject] private GlobalSettings _globalSettings;
    private ShouldLoadFlag _shouldLoadFlag;
    
    [Inject]
    public void Construct(ShouldLoadFlag shouldLoadFlag)
    {
        _shouldLoadFlag = shouldLoadFlag;
    }

    public void OnContinueButtonClicked()
    {
        if (_shouldLoadFlag == null)
        {
            Debug.LogError("ShouldLoadFlag не инжектирован!");
            return;
        }
        
        _shouldLoadFlag.IsNeedToLoad(true);
        SceneManager.LoadScene("SampleScene");
    }

    public void OnNewGameButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    public void OnSettingsButtonClick()
    {
        _settingsWindow.SetActive(true);
        _slider.value = _globalSettings.SoundValue;
        UpdateSoundVolume();
    }

    public void OnAcceptButtonClick()
    {
        _globalSettings.SoundValue = _slider.value;
        _settingStorage.SaveSetting();
        _settingsWindow.SetActive(false);
    }

    public void OnCloseSettingsButtonClick()
    {
        _settingsWindow.SetActive(false);
    }

    public void UpdateSoundVolume()
    {
        _soundVolume.text = $"{(_slider.value * 100).ToString("00.0")}%";
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class BoostrapEntryPoint : MonoBehaviour
{
    [Inject] SettingStorage _settingStorage;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (_settingStorage.CheckForSetting())
        {
            _settingStorage.LoadSetting();
        }
        else
        {
            _settingStorage.SaveSetting();
        }
        Debug.Log("Load complete");
    }
}
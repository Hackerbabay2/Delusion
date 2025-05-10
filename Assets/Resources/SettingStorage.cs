using Storage.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

public class SettingStorage
{
    [Inject] private GlobalSettings _globalSettings;
    private JsonToFileStorageService _jsonToFileStorageService;
    private string _path = "UserSetting";

    public SettingStorage()
    {
        _jsonToFileStorageService = new JsonToFileStorageService();
    }

    public void SaveSetting()
    {
        _jsonToFileStorageService.Save(_path, _globalSettings);
    }

    public void LoadSetting()
    {
        _jsonToFileStorageService.Load<GlobalSettings>(_path, data => 
        {
            if (data != null)
            {
                _globalSettings.Load(data.SoundValue);
            }
        });
    }

    public bool CheckForSetting()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, _path)))
        {
            return true;
        }
        return false;
    }
}
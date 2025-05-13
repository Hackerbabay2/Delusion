using System;
using UnityEngine;

[Serializable]
public class GlobalSettings
{
    [Range(0,1)]
    public float SoundValue = 1f;
    public bool VSyncEnable = false;

    public GlobalSettings(){}

    public void Load(float soundValue, bool vSyncEnable)
    {
        SoundValue = soundValue;
        VSyncEnable = vSyncEnable;

        UpdateVSync();
    }

    public void UpdateVSync()
    {
        if (VSyncEnable)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }
}
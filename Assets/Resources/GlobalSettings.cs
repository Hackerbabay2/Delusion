using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Storage.Scripts;
using System.IO;

[Serializable]
public class GlobalSettings
{
    [Range(0,1)]
    public float SoundValue = 1f;

    public GlobalSettings(){}

    public void Load(float soundValue)
    {
        SoundValue = soundValue;
    }
}

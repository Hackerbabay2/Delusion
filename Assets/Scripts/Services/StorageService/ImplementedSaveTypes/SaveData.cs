using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public abstract class SaveData
{
    public bool IsDynamic = false;

    protected SaveData(){}

    public void SetDynamicValue(bool value)
    {
        IsDynamic = value;
    }
}

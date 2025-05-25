using System;
using UnityEngine;

[RequireComponent(typeof(LightTumbler))]
public class SaveableTumbler : BaseStorage
{
    private LightTumbler _lightTumbler;
    private TumblerData _tumblerData;

    private void Awake()
    {
        _lightTumbler = GetComponent<LightTumbler>();
        _tumblerData = new TumblerData();   
    }

    private void OnEnable()
    {
        SetSaveData(_tumblerData);
        OnStorageEnable();
    }

    private void OnDisable()
    {
        OnStorageDisable();
    }

    public override void Load(SaveData saveData)
    {
        if (saveData.GetType() != typeof(TumblerData))
        {
            Debug.LogError($"{GetSaveKey()} - Getted wrong SaveData. Needed - {nameof(TumblerData)}, Getted - {saveData.GetType().Name}");
            return;
        }

        _tumblerData = saveData as TumblerData;
        _lightTumbler.SetState(_tumblerData.IsOn);
    }

    public override void Save()
    {
        _tumblerData.IsOn = _lightTumbler.LightEnabled;
    }
}

[Serializable]
public class TumblerData : SaveData
{
    public bool IsOn;

    public TumblerData()
    {
        
    }
}
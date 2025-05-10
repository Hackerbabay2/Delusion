using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DayCycle))]
public class SaveableDayCycleService : BaseStorage
{
    private DayCycle _dayCycle;
    private DayCycleData _dayCycleData;

    private void Awake()
    {
        _dayCycle = GetComponent<DayCycle>();
        _dayCycleData = new DayCycleData();
    }

    private void OnEnable()
    {
        SetSaveData(_dayCycleData);
        OnStorageEnable();
    }

    private void OnDisable()
    {
        OnStorageDisable();
    }

    public override void Load(SaveData saveData)
    {
        if (saveData.GetType() != typeof(DayCycleData))
        {
            Debug.LogError($"{GetSaveKey()} - Getted wrong SaveData. Needed - {nameof(DayCycleData)}, Getted - {saveData.GetType().Name}");
            return;
        }
        _dayCycleData = saveData as DayCycleData;
        _dayCycle.SetTimeOfDay(_dayCycleData.TimeOfDay);
    }

    public override void Save()
    {
        _dayCycleData.TimeOfDay = _dayCycle.TimeOfDay;
    }
}

[Serializable]
public class DayCycleData : SaveData
{
    public float TimeOfDay;

    public DayCycleData() { }
}

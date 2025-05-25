using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class SaveableTimer : BaseStorage
{
    private Timer _timer;
    private TimerData _timerData;

    private void Awake()
    {
        _timer = GetComponent<Timer>();
        _timerData = new TimerData();
    }

    private void OnEnable()
    {
        SetSaveData(_timerData);
        OnStorageEnable();
    }

    private void OnDisable()
    {
        OnStorageDisable();
    }

    public override void Load(SaveData saveData)
    {
        if (saveData.GetType() != typeof(TimerData))
        {
            Debug.LogError($"{GetSaveKey()} - Getted wrong SaveData. Needed - {nameof(TimerData)}, Getted - {saveData.GetType().Name}");
            return;
        }

        _timerData = saveData as TimerData;
        _timer.SetState(_timerData.isEnabled);
    }

    public override void Save()
    {
        _timerData.isEnabled = _timer.IsTimerActive;
    }
}

[Serializable]
public class TimerData : SaveData
{
    public bool isEnabled;

    public TimerData()
    {
        
    }
}
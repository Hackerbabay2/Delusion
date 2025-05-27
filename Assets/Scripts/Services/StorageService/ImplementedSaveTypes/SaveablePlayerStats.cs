using System;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class SaveablePlayerStats : BaseStorage
{
    private PlayerStatsData _playerStatsData;
    private PlayerStats _playerStats;

    private void Awake()
    {
        _playerStatsData = new PlayerStatsData();
        _playerStats = GetComponent<PlayerStats>(); 
    }

    private void OnEnable()
    {
        SetSaveData(_playerStatsData);
        OnStorageEnable();
    }

    private void OnDisable()
    {
        OnStorageDisable();
    }

    public override void Load(SaveData saveData)
    {
        if (saveData.GetType() != typeof(PlayerStatsData))
        {
            Debug.LogError($"{GetSaveKey()} - Getted wrong SaveData. Needed - {nameof(PlayerStatsData)}, Getted - {saveData.GetType().Name}");
            return;
        }

        _playerStatsData = saveData as PlayerStatsData;
        _playerStats.SetStats(_playerStatsData.Fatigue, _playerStatsData.FlashLightPower);
    }

    public override void Save()
    {
        _playerStatsData.Fatigue = _playerStats.Fatigue;
        _playerStatsData.FlashLightPower = _playerStats.FlashLightPower;
    }
}

[Serializable]
public class PlayerStatsData : SaveData 
{
    public float FlashLightPower;
    public float Fatigue;

    public PlayerStatsData()
    {
        
    }
}
using Storage.Scripts;
using System;
using UnityEngine;
using Zenject;

public abstract class BaseStorage : MonoBehaviour
{
    [Inject] protected StorageService _storageService;

    private SaveData _saveData;

    public SaveData SaveDataValue => _saveData;

    protected void SetSaveData(SaveData saveData)
    {
        _saveData = saveData;
    }

    protected void OnStorageEnable()
    {
        _storageService?.RegisterSaveable(this);
    }

    protected void OnStorageDisable()
    {
        _storageService?.UnregisterSaveable(this);
    }

    public virtual void InitData(SaveData saveData)
    {
        Save();
        _storageService.GameData.AddData(GetSaveKey(), saveData);
    }

    public abstract void Save();

    public abstract void Load(SaveData saveData);

    public string GetSaveKey()
    {
        return $"{gameObject.scene.name}_{gameObject.name}";
    }
}
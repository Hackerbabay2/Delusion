using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ElectricalPanel))]
public class SaveableElectricalPanel : BaseStorage
{
    private ElectricalPanel _electricalPanel;
    private ElectricalPanelData _electricalPanelData;

    private void Awake()
    {
        _electricalPanel = GetComponent<ElectricalPanel>();
        _electricalPanelData = new ElectricalPanelData();
    }

    private void OnEnable()
    {
        SetSaveData(_electricalPanelData);
        OnStorageEnable();
    }

    private void OnDisable()
    {
        OnStorageDisable();
    }

    public override void Load(SaveData saveData)
    {
        if (saveData.GetType() != typeof(ElectricalPanelData))
        {
            Debug.LogError($"{GetSaveKey()} - Getted wrong SaveData. Needed - {nameof(ElectricalPanelData)}, Getted - {saveData.GetType().Name}");
            return;
        }

        _electricalPanelData = saveData as ElectricalPanelData;
        _electricalPanel.SetRepaired(_electricalPanelData.IsRepaired);
        _electricalPanel.SetState(_electricalPanelData.IsTurn);
        _electricalPanel.SetEnabledSlots(_electricalPanelData.EnabledSlotsCount);
    }

    public override void Save()
    {
        _electricalPanelData.IsRepaired = _electricalPanel.IsRepaired;
        _electricalPanelData.IsTurn = _electricalPanel.IsTurn;
        _electricalPanelData.EnabledSlotsCount = _electricalPanel.EnabledSlotsCount;
    }
}

[Serializable]
public class ElectricalPanelData : SaveData
{
    public bool IsRepaired;
    public bool IsTurn;
    public int EnabledSlotsCount;

    public ElectricalPanelData()
    {

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Toggle))]
public class VSyncToggle : MonoBehaviour
{
    [Inject] private SettingStorage _settingStorage;
    [Inject] private GlobalSettings _globalSettings;

    private Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        _settingStorage.OnSettingsUpdate += UpdateToggle;
    }

    private void OnDisable()
    {
        _settingStorage.OnSettingsUpdate -= UpdateToggle;
    }

    public void UpdateValue()
    {
        _globalSettings.VSyncEnable = _toggle.isOn;
    }

    public void UpdateToggle()
    {
        _toggle.isOn = _globalSettings.VSyncEnable;
    }

    public void SetToggle(bool enable)
    {
        _toggle.isOn = enable;
    }
}

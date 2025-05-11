using System;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _flashLightPower;
    [SerializeField] private float _fatigue;
    [SerializeField] private UnityEvent _onValueUpdateFlashlightPower;
    [SerializeField] private UnityEvent _onFatigueValueUpdate;

    private float _maxFlashLightPower = 100;
    private float _maxFatigue = 100;

    public float FlashLightPower => _flashLightPower;
    public float Fatigue => _fatigue;

    public float MaxFlashLightPower => _maxFlashLightPower;
    public float MaxFatigue => _maxFatigue;

    public void DecreaseFlashLightPower(float value)
    {
        _flashLightPower -= value;

        if (_flashLightPower <= 0)
        {
            _flashLightPower = 0;
        }

        _onValueUpdateFlashlightPower?.Invoke();
    }

    public void ChargeFlashLighPower()
    {
        _flashLightPower = _maxFlashLightPower;
        _onValueUpdateFlashlightPower?.Invoke();
    }

    public void DecreaseFatigue(float value)
    {
        _fatigue -= value;

        if (_fatigue <= 0)
        {
            _fatigue = 0;
        }

        _onFatigueValueUpdate?.Invoke();
    }

    internal void IncreaseFatigue(float value)
    {
        _fatigue += value;

        if (_fatigue >= _maxFatigue)
        {
            _fatigue = _maxFatigue;
        }

        _onFatigueValueUpdate?.Invoke();
    }
}
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _flashLightPower;
    [SerializeField] private float _fatigue;
    [SerializeField] private UnityEvent _onDecreaseFlashlightPower;

    public float FlashLightPower => _flashLightPower;
    public float Fatigue => _fatigue;

    public void DecreaseFlashLightPower(float value)
    {
        _flashLightPower -= value;

        if (_flashLightPower <= 0)
        {
            _flashLightPower = 0;
        }
        _onDecreaseFlashlightPower?.Invoke();
    }
}

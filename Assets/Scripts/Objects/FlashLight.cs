using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Light), typeof(AudioSource))]
public class FlashLight : SoundEffector
{
    [SerializeField] private float _dischargeSpeed;
    [SerializeField] private float _dischargeValue;

    [Inject] private PlayerStats _playerStats;

    private KeyInputService _keyInputService;
    private bool _enabled = false;
    private Light _light;
    private Coroutine _flashlightDecreasePower;

    private void OnDisable()
    {
        _keyInputService.Dispose();
    }

    private void Start()
    {
        _light = GetComponent<Light>();
        _keyInputService = new KeyInputService();
        _light.enabled = _enabled;
    }

    private void Update()
    {
        if (_keyInputService.IsFlashLightPressed())
        {
            AudioSource.Play();
            _enabled = !_enabled;
            _light.enabled = _enabled;

            if (_enabled == false && _flashlightDecreasePower != null)
            {
                StopCoroutine(_flashlightDecreasePower);
            }

            if (_enabled && _playerStats.FlashLightPower > 0)
            {
                _flashlightDecreasePower = StartCoroutine(Discharge());
            }
        }
    }

    private IEnumerator Discharge()
    {
        while (_playerStats.FlashLightPower > 0)
        {
            yield return new WaitForSeconds(_dischargeSpeed);
            _playerStats.DecreaseFlashLightPower(_dischargeValue);
        }
        _enabled = false;
        _light.enabled = _enabled;
        AudioSource.Play();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using Zenject;

[RequireComponent(typeof(Light), typeof(AudioSource))]
public class FlashLight : MonoBehaviour
{
    [SerializeField] private AudioClip _buttonSound;
    [SerializeField] private float _dischargeSpeed;
    [SerializeField] private float _dischargeValue;

    [Inject] private PlayerStats _playerStats;

    private KeyInputService _keyInputService;
    private bool _enabled = false;
    private Light _light;
    private AudioSource _audioSource;
    private Coroutine _flashlightDecreasePower;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _light = GetComponent<Light>();
        _keyInputService = new KeyInputService();
        _audioSource.clip = _buttonSound;
        _audioSource.playOnAwake = false;
        _light.enabled = _enabled;
    }

    private void Update()
    {
        if (_keyInputService.IsFlashLightPressed())
        {
            _audioSource.Play();
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
        _audioSource?.Play();
    }
}

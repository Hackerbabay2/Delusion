using KinematicCharacterController.Examples;
using System.Collections;
using UnityEngine;
using Zenject;

public class Bed : MonoBehaviour, IInteractable
{
    [Header("Links")]
    [SerializeField] private ExamplePlayer _examplePlayer;
    [SerializeField] private Fatigue _fatigue;
    [SerializeField] private DayCycle _dayCycle;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _cameraPoint;

    [Header("Values")]
    [SerializeField] private float _canSleepValue = 50f;
    [SerializeField] private float _dayDuration = 30f;
    [SerializeField] private float _fatigueIncreaseValue;
    [SerializeField] private float _fatigueIncreaseSpeed;

    [Inject] private PlayerStats _playerStats;

    private float _savedDayDuratoin;
    private Light _light;
    private FlashLight _flashLight;

    private void Awake()
    {
        _savedDayDuratoin = _dayCycle.DayDuration;
        _light = _camera.GetComponentInChildren<Light>();
        _flashLight = _light.GetComponent<FlashLight>();
    }

    public void Interact()
    {
        if (_playerStats.Fatigue <= _canSleepValue)
        {
            if (_light.enabled)
            {
                _light.enabled = false;
                _flashLight.enabled = false;
            }

            _examplePlayer.enabled = false;
            _camera.transform.position = _cameraPoint.position;
            _camera.transform.rotation = _cameraPoint.rotation;
            _dayCycle.SetDuration(_dayDuration);
            StartCoroutine(_fatigue.IncreaseFatugue(_fatigueIncreaseSpeed, _fatigueIncreaseValue));
            StartCoroutine(WaitForEnd());
        }
    }

    private IEnumerator WaitForEnd()
    {
        while (_playerStats.Fatigue < _playerStats.MaxFatigue)
        {
            yield return null;
        }

        _examplePlayer.enabled = true;
        _flashLight.enabled = true;
        _dayCycle.SetDuration(_savedDayDuratoin);
    }
}

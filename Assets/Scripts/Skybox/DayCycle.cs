using UnityEngine;
using UnityEngine.Events;

public class DayCycle : MonoBehaviour
{
    [Header("DayCycle Events")]
    [SerializeField] public UnityEvent OnDayStart;

    [Range(0, 1)]
    [SerializeField] private float _timeOfDay;
    [SerializeField] private float _dayDuration = 30f;

    [SerializeField] private AnimationCurve _sunCurve;
    [SerializeField] private AnimationCurve _moonCurve;
    [SerializeField] private AnimationCurve _skyboxCurve;
    [SerializeField] private AnimationCurve _fogDensityCurve;
    [SerializeField] private float _maxFogDensity = 0.05f;

    [SerializeField] private Material _daySkybox;
    [SerializeField] private Material _nightSkybox;

    [SerializeField] private ParticleSystem _stars;

    [SerializeField] private Light _sun;
    [SerializeField] private Light _moon;

    [Header("Music Settings")]
    [SerializeField] private AudioSource _dayMusic;
    [SerializeField] private AudioSource _nightMusic;
    [SerializeField] private float _musicTransitionSpeed = 1f;
    [SerializeField] private float _musicSwitchThreshold = 0.3f;

    private float sunIntensity;
    private float moonIntensity;
    private bool _dayStartTrigger;
    private bool _isDayMusicPlaying = true;

    public float TimeOfDay => _timeOfDay;

    private void Start()
    {
        sunIntensity = _sun.intensity;
        moonIntensity = _moon.intensity;

        if (_dayMusic != null && _nightMusic != null)
        {
            _dayMusic.volume = 0.1f;
            _nightMusic.volume = 0f;
            _dayMusic.Play();
            _nightMusic.Play();
        }
    }

    private void Update()
    {
        _timeOfDay += Time.deltaTime / _dayDuration;

        if (_timeOfDay >= 1)
        {
            _timeOfDay = 0;
            _dayStartTrigger = false;
        }

        var (currentHour, currentMinute) = GetCurrentTime();

        if (!_dayStartTrigger && currentHour == 0 && currentMinute == 0)
        {
            OnDayStart?.Invoke();
            _dayStartTrigger = true;
        }

        RenderSettings.skybox.Lerp(_nightSkybox, _daySkybox, _skyboxCurve.Evaluate(_timeOfDay));
        RenderSettings.sun = _skyboxCurve.Evaluate(_timeOfDay) > 0.1f ? _sun : _moon;

        float fogIntensity = _fogDensityCurve.Evaluate(_timeOfDay);
        RenderSettings.fogDensity = fogIntensity * _maxFogDensity;
        RenderSettings.fog = fogIntensity > 0.01f;

        DynamicGI.UpdateEnvironment();

        var mainModule = _stars.main;

        _sun.transform.localRotation = Quaternion.Euler(_timeOfDay * 360f, 180, 0);
        _moon.transform.localRotation = Quaternion.Euler(_timeOfDay * 360f + 180f, 180, 0);

        _sun.intensity = sunIntensity * _sunCurve.Evaluate(_timeOfDay);
        _moon.intensity = moonIntensity * _moonCurve.Evaluate(_timeOfDay);

        UpdateMusic();
    }

    private void UpdateMusic()
    {
        if (_dayMusic == null || _nightMusic == null) return;

        float dayFactor = _skyboxCurve.Evaluate(_timeOfDay);
        bool shouldPlayDayMusic = dayFactor > _musicSwitchThreshold;

        if (shouldPlayDayMusic != _isDayMusicPlaying)
        {
            _isDayMusicPlaying = shouldPlayDayMusic;
        }

        float targetDayVolume = _isDayMusicPlaying ? 0.1f : 0f;
        float targetNightVolume = _isDayMusicPlaying ? 0f : 0.1f;

        _dayMusic.volume = Mathf.Lerp(_dayMusic.volume, targetDayVolume, Time.deltaTime * _musicTransitionSpeed);
        _nightMusic.volume = Mathf.Lerp(_nightMusic.volume, targetNightVolume, Time.deltaTime * _musicTransitionSpeed);
    }

    public (int hour, int minute) GetCurrentTime()
    {
        float countMinetsInDay = 1440;
        float totalMinutes = _timeOfDay * countMinetsInDay;
        int hour = Mathf.FloorToInt(totalMinutes / 60);
        int minute = Mathf.FloorToInt(totalMinutes % 60);

        return (hour, minute);
    }

    public string GetCurrentTimeFormatted()
    {
        var (hour, minute) = GetCurrentTime();
        return $"{hour:D2}:{minute:D2}";
    }

    public void SetTimeOfDay(float timeOfDay)
    {
        _timeOfDay = timeOfDay;
    }
}
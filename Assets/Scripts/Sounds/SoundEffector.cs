using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public abstract class SoundEffector : MonoBehaviour
{
    [Inject] private GlobalSettings _settings;
    [Inject] private SettingStorage _storage;

    [Header("Sound Settings")]
    [SerializeField] protected AudioClip Sound;
    [SerializeField] protected float MinPitch = 0.9f;
    [SerializeField] protected float MaxPitch = 1.1f;
    [SerializeField] protected float MinVolume = 0.1f;
    [SerializeField] protected float MaxVolume = 1f;

    protected AudioSource AudioSource;

    protected virtual void Awake()
    {
        if (Sound == null)
        {
            Sound = Resources.Load<AudioClip>("Padenie");
        }

        AudioSource = GetComponent<AudioSource>();
        AudioSource.volume = _settings.SoundValue;
        AudioSource.playOnAwake = false;
        AudioSource.spatialBlend = 1f;
        AudioSource.minDistance = 0.5f;
        AudioSource.maxDistance = 15f;
        AudioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        AudioSource.clip = Sound;
    }

    private void OnEnable()
    {
        _storage.OnSettingsUpdate += UpdateVolume;
    }

    private void OnDisable()
    {
        _storage.OnSettingsUpdate -= UpdateVolume;
    }

    private void UpdateVolume()
    {
        AudioSource.volume = _settings.SoundValue;
    }
}
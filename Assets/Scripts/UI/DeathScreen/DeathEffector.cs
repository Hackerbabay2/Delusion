using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class DeathEffector : MonoBehaviour
{
    [SerializeField] private AudioClip _audio;
    [SerializeField] private float _durtaion;

    [Inject] private GlobalSettings _globalSettings;

    private AudioSource _audioSource;

    [Inject]
    private void Construct(GlobalSettings settings)
    {
        Debug.Log(settings != null ? "Injected successfully" : "Injection failed");
        _globalSettings = settings;
    }

    private void Awake()
    {
        Construct(_globalSettings);
    }

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audio;

        if (_globalSettings != null)
        {
            _audioSource.volume = _globalSettings.SoundValue;
        }
        else
        {
            Debug.Log("GlobalSettings not loaded");
            _audioSource.volume = 0.5f;
        }
        _audioSource.Play();
        StartCoroutine(StartMainMenuForDuration());
    }

    private IEnumerator StartMainMenuForDuration()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_durtaion);
        yield return waitForSeconds;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }
}
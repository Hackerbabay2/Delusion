using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoadingWindow : MonoBehaviour
{
    [Inject] private StorageService _storageService;

    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _loadingWindow;

    private Coroutine _loadCoroutine;

    public void ShowLoadWindow()
    {
        _loadingWindow.SetActive(true);
        _loadCoroutine = StartCoroutine(ShowLoadValue());
    }

    private IEnumerator ShowLoadValue()
    {
        while(_slider.value != 1)
        {
            _slider.value = _storageService.LoadProgress;
            yield return null;
        }
    }

    public void HideLoadWindow()
    {
        StopCoroutine(_loadCoroutine);
        _slider.value = 0;
        _loadingWindow?.SetActive(false);
    }
}
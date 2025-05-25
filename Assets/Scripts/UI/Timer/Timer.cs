using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Timer : MonoBehaviour
{
    [SerializeField] private UnityEvent _onTimerEnd;
    [SerializeField] private float _duration = 120f;
    [SerializeField] private float _timerSpeed = 1f;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private GameObject _timerObject;

    [Inject] private StorageService _storageService;

    private int minutes;
    private int seconds;

    public bool IsTimerActive => _timerObject.activeSelf;

    public void SetState(bool enabled)
    {
        StopAllCoroutines();
        _timerObject.SetActive(enabled);
    }

    public void TimerStart()
    {
        _storageService.SetCanSave(false);
        _timerObject.SetActive(true);
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        for (int i = 0; i < _duration+1; i++)
        {
            minutes = (int)(_duration - i) / 60;
            seconds = (int)(_duration - i) % 60;
            _timerText.text = $"Помощь прибудет через - {minutes:D2}:{seconds:D2}";
            WaitForSeconds waitForSeconds = new WaitForSeconds(_timerSpeed);
            yield return waitForSeconds;
        }
        _onTimerEnd?.Invoke();
    }
}
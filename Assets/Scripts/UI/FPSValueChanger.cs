using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class FPSValueChanger : ValueChanger
{
    [SerializeField] private float _updateInterval = 1f;

    private float _accum = 0f;
    private int _frames = 0;
    private float _fps = 0;

    private Coroutine _updateFPSForInterval;

    private void OnEnable()
    {
        _updateFPSForInterval = StartCoroutine(UpdateFPSForInterval());
    }

    private void OnDisable()
    {
        if (_updateFPSForInterval != null)
        {
            StopCoroutine(_updateFPSForInterval);
        }
        _updateFPSForInterval= null;
    }

    private void Update()
    {
        _accum += Time.timeScale / Time.deltaTime;
        _frames++;
    }

    public IEnumerator UpdateFPSForInterval()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_updateInterval);

        while (true)
        {
            yield return waitForSeconds;
            _fps = _accum / _frames;
            _text.text = $"FPS:{_fps.ToString("0")}";
            _accum = 0f;
            _frames = 0;
        }
    }
}
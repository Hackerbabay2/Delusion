using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Fatigue : MonoBehaviour
{
    [SerializeField] private float _decreaseSpeed = 1f;
    [SerializeField] private float _decreaseValue = 0.1f;
    [SerializeField] private float _madOffset = 2f;
    [SerializeField] private List<GameObject> _madObjects = new List<GameObject>();

    [Inject] private PlayerStats _playerStats;

    private Coroutine _fatigueDecrease;
    private bool _isDecreasing = false;
    private List<MadEvent> _madEvents = new List<MadEvent>();

    private void Awake()
    {
        for (int i = 0; i < _madObjects.Count; i++)
        {
            _madEvents.Add(new MadEvent(_madObjects[i], _playerStats.MaxFatigue / (i + _madOffset)));
        }
    }

    private void Update()
    {
        if (_playerStats.Fatigue > 0 && _isDecreasing == false)
        {
            _fatigueDecrease = StartCoroutine(DecreaseFatigue());
        }

        if (_playerStats.Fatigue <= 0)
        {
            StopCoroutine(_fatigueDecrease);
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        SceneManager.LoadScene("DeathScreen");
    }

    private IEnumerator DecreaseFatigue()
    {
        _isDecreasing = true;
        WaitForSeconds waitForSeconds = new WaitForSeconds(_decreaseSpeed);

        while (_playerStats.Fatigue > 0)
        {
            yield return waitForSeconds;
            _playerStats.DecreaseFatigue(_decreaseValue);
            CheckForMad();
        }

        _isDecreasing = false;
    }
    public IEnumerator IncreaseFatugue(float duration, float increaseValue)
    {
        _isDecreasing = true;

        StopCoroutine(_fatigueDecrease);

        WaitForSeconds waitForSeconds = new WaitForSeconds(duration);

        while (_playerStats.Fatigue < _playerStats.MaxFatigue)
        {
            yield return waitForSeconds;
            _playerStats.IncreaseFatigue(increaseValue);
            CheckForMad();
        }
        _isDecreasing = false;
    }

    private void CheckForMad()
    {
        foreach (MadEvent madEvent in _madEvents)
        {
            if (madEvent.GameObject.activeSelf == false && madEvent.OnValueStart >= _playerStats.Fatigue)
            {
                madEvent.SetActiveObject(true);
            }
            else if(madEvent.GameObject.activeSelf == true && madEvent.OnValueStart < _playerStats.Fatigue)
            {
                madEvent.SetActiveObject(false);
            }
        }
    }
}

public class MadEvent
{
    private GameObject _gameObject;
    private float _onValueStart;

    public float OnValueStart => _onValueStart;
    public GameObject GameObject => _gameObject;

    public MadEvent(GameObject gameObject, float onValueStart)
    {
        _gameObject = gameObject;
        _onValueStart = onValueStart;
    }

    public void SetActiveObject(bool isActive)
    {
        _gameObject.SetActive(isActive);
    }
}
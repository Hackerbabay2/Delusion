using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class DesktopButtonClicker : MonoBehaviour
{
    [SerializeField] private DayCycle _dayCycle;
    [SerializeField] private TMP_Text _timeText;

    [Inject] private ExamplePlayer _examplePlayer;

    [SerializeField] private GameObject _sosWindow;
    [SerializeField] private InGameMenuClick _inGameMenuClick;

    private KeyInputService _keyInputService;

    private void Awake()
    {
        _keyInputService = new KeyInputService();
    }

    private void OnEnable()
    {
        _examplePlayer.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        _examplePlayer.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if(_keyInputService.IsMenuPressed())
        {
            StartCoroutine(DisableComputer());
        }

        _timeText.text = _dayCycle.GetCurrentTimeFormatted();
    }

    private IEnumerator DisableComputer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yield return new WaitForEndOfFrame();
        _inGameMenuClick.enabled = true;
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }

    public void OnSOSButtonClick()
    {
        _sosWindow.SetActive(true);
    }
}
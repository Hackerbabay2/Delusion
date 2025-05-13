using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DesktopButtonClicker : MonoBehaviour
{
    [Inject] private ExamplePlayer _examplePlayer;

    [SerializeField] private GameObject _sosWindow;

    private KeyInputService _inputService;

    private void Awake()
    {
        _inputService = new KeyInputService();
    }

    private void OnEnable()
    {
        _examplePlayer.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnDisable()
    {
        _examplePlayer.enabled = true;
    }

    private void Update()
    {
        if(_inputService.IsMenuPressed())
        {
            gameObject.SetActive(false);
        }
    }

    public void OnSOSButtonClick()
    {
        _sosWindow.SetActive(true);
    }
}
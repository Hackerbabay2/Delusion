using KinematicCharacterController.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class InGameMenuClick : MonoBehaviour
{
    [Inject] StorageService _storageService;

    [SerializeField] private GameObject _inGameMenu;
    [SerializeField] private ExamplePlayer _examplePlayer;
    private KeyInputService _keyInputService;

    private void Awake()
    {
        _keyInputService = new KeyInputService();
    }

    private void Update()
    {
        if (_keyInputService.IsMenuPressed())
        {
            _inGameMenu.SetActive(!_inGameMenu.activeSelf);
            _examplePlayer.enabled = !_inGameMenu.activeSelf;
            UpdateCursorState();
        }
    }

    private void UpdateCursorState()
    {
        if (_inGameMenu.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        _examplePlayer.enabled = !_inGameMenu.activeSelf;
    }

    public void OnContinueButtonClick()
    {
        _inGameMenu.SetActive(false);
        UpdateCursorState();
    }

    public void OnSaveButtonClick()
    {
        StartCoroutine(_storageService.SaveGame());
        _inGameMenu.SetActive(false);
        UpdateCursorState();
    }

    public void OnLoadButtonClick()
    {
        StartCoroutine(_storageService.LoadGame());
        _inGameMenu.SetActive(false);
        UpdateCursorState();
    }

    public void OnExitInMainMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

using KinematicCharacterController.Examples;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class LetterTextChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text _letterText;
    [SerializeField] protected InGameMenuClick _inGameMenuClick;

    [Inject] private ExamplePlayer _player;

    private KeyInputService _keyInputService;

    private void Awake()
    {
        _keyInputService = new KeyInputService();
    }

    private void OnDisable()
    {
        _keyInputService.Dispose();
    }

    public void ChangeText(string text)
    {
        _letterText.text = text;
    }

    private void Update()
    {
        if (_keyInputService.IsMenuPressed())
        {
            StartCoroutine(CloseLetter());
        }
    }

    private IEnumerator CloseLetter()
    {
        _player.enabled = true;
        yield return new WaitForEndOfFrame();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yield return new WaitForEndOfFrame();
        _inGameMenuClick.enabled = true;
        gameObject.SetActive(false);
    }

    public void OnCloseButtonClick()
    {
        StartCoroutine(CloseLetter());
    }
}

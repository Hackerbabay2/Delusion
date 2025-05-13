using KinematicCharacterController.Examples;
using TMPro;
using UnityEngine;
using Zenject;

public class LetterTextChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text _letterText;

    [Inject] private ExamplePlayer _player;

    private KeyInputService _keyInputService;

    private void Awake()
    {
        _keyInputService = new KeyInputService();
    }

    public void ChangeText(string text)
    {
        _letterText.text = text;
    }

    private void Update()
    {
        if (_keyInputService.IsMenuPressed())
        {
            _player.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            gameObject.SetActive(false);
        }
    }

    public void OnCloseButtonClick()
    {
        _player.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }
}

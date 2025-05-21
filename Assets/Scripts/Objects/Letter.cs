using KinematicCharacterController.Examples;
using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(MoveableObject))]
public class Letter : MonoBehaviour, IInteractable
{
    [SerializeField] [TextArea(10,20)] private string _text;
    [SerializeField] private GameObject _letterPanel;
    [SerializeField] private LetterTextChanger _textChanger;
    [SerializeField] private InGameMenuClick _inGameMenuClick;

    [Inject] private ExamplePlayer _player;

    private MoveableObject _moveableObject;

    private void Awake()
    {
        _moveableObject = GetComponent<MoveableObject>();
    }

    public void Interact()
    {
        _moveableObject.Interact();
    }

    public virtual void Use()
    {
        _player.enabled = false;
        _inGameMenuClick.enabled = false;
        _letterPanel.SetActive(true);
        _textChanger.ChangeText(_text);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _computerWindow;
    [SerializeField] private InGameMenuClick _inGameClick;

    public void Interact()
    {

    }

    public virtual void Use()
    {
        _computerWindow.SetActive(true);
        _inGameClick.enabled = false;
    }
}
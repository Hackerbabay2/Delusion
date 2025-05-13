using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _computerWindow;

    public void Interact()
    {

    }

    public virtual void Use()
    {
        _computerWindow.SetActive(true);
    }
}
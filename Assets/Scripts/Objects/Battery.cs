using UnityEngine;
using Zenject;

[RequireComponent(typeof(SaveableDynamicObject), typeof(MoveableObject))]
public class Battery : SoundEffector, IInteractable
{
    [Inject] private PlayerStats _stats;

    private SaveableDynamicObject _dynamicObject;
    private MoveableObject _moveableObject;

    private void Start()
    {
        _dynamicObject = GetComponent<SaveableDynamicObject>();
        _moveableObject = GetComponent<MoveableObject>();
    }

    public void Interact()
    {
        _moveableObject.Interact();
    }

    public virtual void Use()
    {
        if (_stats.FlashLightPower == _stats.MaxFlashLightPower)
        {
            return;
        }
        AudioSource.Play();
        _stats.ChargeFlashLighPower();
        _dynamicObject.SetDestroyed();
        gameObject.SetActive(false);
    }
}
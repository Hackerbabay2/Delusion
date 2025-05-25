using DG.Tweening;
using UnityEngine;

public class LightTumbler : SoundEffector, IInteractable
{
    [SerializeField] private Light _light;
    [SerializeField] private Transform _switchButton;
    [SerializeField] private float _rotationAngle = 10f;
    [SerializeField] private float _rotationDuration = 0.3f;

    public bool LightEnabled => _light.enabled;

    private void Start()
    {
        RotateButton();
    }

    public void Interact()
    {
        _light.enabled = !_light.enabled;
        RotateButton();
        AudioSource.Play();
    }

    public void SetState(bool lightEnabled)
    {
        _light.enabled = lightEnabled;
        RotateButton();
    }

    private void RotateButton()
    {
        float targetRotation = _light.enabled ? _rotationAngle : -_rotationAngle;
        _switchButton.DOLocalRotate(new Vector3(targetRotation, 0, 0), _rotationDuration)
            .SetEase(Ease.OutBack);
    }
}
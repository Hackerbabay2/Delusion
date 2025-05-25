using DG.Tweening;
using UnityEngine;

public class TowerLeverTumbler : SoundEffector, IInteractable
{
    [SerializeField] private ElectricalPanel _electriaclPanel;
    [SerializeField] private Transform _leverSwitch;
    [SerializeField] private float _rotationAngle = 60f;
    [SerializeField] private float _rotationDuration = 0.3f;

    private void Start()
    {
        RotateLever();
    }

    public void Interact()
    {
        if (_electriaclPanel.IsRepaired)
        {
            _electriaclPanel.TurnTower();
            RotateLever();
            AudioSource.Play();
        }
    }

    private void RotateLever()
    {
        float targetRotation = _electriaclPanel.IsTurn ? _rotationAngle : -_rotationAngle;
        _leverSwitch.DOLocalRotate(new Vector3(targetRotation, 0, 0), _rotationDuration)
            .SetEase(Ease.OutBack);
    }
}
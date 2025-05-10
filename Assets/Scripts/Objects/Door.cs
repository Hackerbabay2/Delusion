using DG.Tweening;
using Storage.Scripts;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveableDoor))]
public class Door : SoundEffector, IInteractable
{
    [SerializeField] private float _openAngle = 90f;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private List<AudioClip> _closeDoorClips = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _openDoorClips = new List<AudioClip>();

    private bool _isOpen = false;
    private Quaternion _originalRotation;

    public bool IsOpen => _isOpen;

    private void Start()
    {
        _originalRotation = transform.rotation;
    }

    public void SetIsOpen(bool isOpen)
    {
        _isOpen = isOpen;
    }

    public void Interact()
    {
        if (_isOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
        _isOpen = !_isOpen;
    }

    private void OpenDoor()
    {
        transform.DORotate(_originalRotation.eulerAngles + Vector3.up * _openAngle, _duration);

        if (_openDoorClips.Count > 0)
        {
            AudioSource.clip = _openDoorClips[Random.Range(0, _openDoorClips.Count)];
            AudioSource.pitch = Random.Range(MinPitch, MaxPitch);
            AudioSource.Play();
        }
    }

    private void CloseDoor()
    {
        transform.DORotate(_originalRotation.eulerAngles, _duration);

        if (_closeDoorClips.Count > 0)
        {
            AudioSource.clip = _closeDoorClips[Random.Range(0, _closeDoorClips.Count)];
            AudioSource.pitch = Random.Range(MinPitch, MaxPitch);
            AudioSource.Play();
        }
    }
}
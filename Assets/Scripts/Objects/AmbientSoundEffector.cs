using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundEffector : SoundEffector
{
    [SerializeField] private float _minDistance = 0.5f;
    [SerializeField] private float _maxDistance = 15f;

    void Start()
    {
        AudioSource.playOnAwake = true;
        AudioSource.minDistance = _minDistance;
        AudioSource.maxDistance = _maxDistance;
        AudioSource.Play();
    }
}

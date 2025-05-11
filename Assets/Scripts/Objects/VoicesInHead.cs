using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class VoicesInHead : SoundEffector
{
    [Inject] GlobalSettings settings;

    private void Start()
    {
        AudioSource.playOnAwake = true;
        AudioSource.loop = true;
        AudioSource.Play();
    }
}

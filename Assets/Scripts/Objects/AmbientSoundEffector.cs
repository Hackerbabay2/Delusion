using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundEffector : SoundEffector
{
    void Start()
    {
        AudioSource.playOnAwake = true;
    }
}

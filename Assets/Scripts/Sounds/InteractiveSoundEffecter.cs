using UnityEngine;

public class InteractiveSoundEffecter : SoundEffector
{
    private void OnCollisionEnter(Collision collision)
    {
        if (Sound == null) return;

        float collisionForce = collision.impulse.magnitude;
        float volume = Mathf.Clamp(collisionForce * 0.1f, MinVolume, MaxVolume);

        if (volume < MinVolume) return;

        AudioSource.pitch = Random.Range(MinPitch, MaxPitch);
        AudioSource.PlayOneShot(Sound, volume);
    }
}

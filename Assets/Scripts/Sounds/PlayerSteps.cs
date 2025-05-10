using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(KinematicCharacterMotor))]
public class PlayerSteps : SoundEffector
{
    [SerializeField] private List<AudioClip> _stepsSounds = new List<AudioClip>();
    [SerializeField] private float _stepInterval = 0.5f;

    private KinematicCharacterMotor _characterMotor;
    private Coroutine _footstepCoroutine;
    private bool isGrounded;
    private bool isMoving;

    private void Start()
    {
        _characterMotor = GetComponent<KinematicCharacterMotor>();
    }

    private void Update()
    {
        isGrounded = _characterMotor.GroundingStatus.IsStableOnGround;
        isMoving = _characterMotor.Velocity.magnitude > 0.1f;

        if (isGrounded && isMoving)
        {
            if (_footstepCoroutine == null)
            {
                _footstepCoroutine = StartCoroutine(PlayFootsteps());
            }
        }
        else
        {
            if (_footstepCoroutine != null)
            {
                StopCoroutine(_footstepCoroutine);
                _footstepCoroutine = null;
            }
        }
    }

    private IEnumerator PlayFootsteps()
    {
        while (true)
        {
            PlayFootstepSound();
            yield return new WaitForSeconds(_stepInterval);
        }
    }

    private void PlayFootstepSound()
    {
        if (_stepsSounds.Count == 0 || AudioSource == null)
            return;

        int randomIndex = Random.Range(0, _stepsSounds.Count);
        AudioSource.clip = _stepsSounds[randomIndex];
        AudioSource.Play();
    }

    public void UpdateCharacterMotor()
    {
        _characterMotor = GetComponent<KinematicCharacterMotor>();
    }
}
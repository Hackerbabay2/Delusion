using KinematicCharacterController;
using KinematicCharacterController.Examples;
using UnityEngine;

public class PlayerCheatController : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private KinematicCharacterMotor _motor;
    [SerializeField] private ExampleCharacterController _characterController;

    [Header("Settings")]
    [SerializeField] private float _speedMultiplier = 10f;
    [SerializeField] private float _teleportDistance = 10f;

    private KeyInputService _keyInputService;
    private bool _isSpeedHackEnabled = false;

    private float _originalMaxStableMoveSpeed;
    private float _originalMaxAirMoveSpeed;

    private void Awake()
    {
        _keyInputService = new KeyInputService();
        _originalMaxStableMoveSpeed = _characterController.MaxStableMoveSpeed;
        _originalMaxAirMoveSpeed = _characterController.MaxAirMoveSpeed;
    }


    private void Update()
    {
        if (_keyInputService.IsF1Pressed())
        {
            ToggleSpeedBoost();
        }

        if (_keyInputService.IsF8Pressed())
        {
            Teleport(_motor.TransientPosition + _motor.CharacterUp * _teleportDistance);
        }

        if (_keyInputService.IsKPressed())
        {
            Teleport(_motor.TransientPosition + _motor.CharacterForward * _teleportDistance);
        }

        if (_keyInputService.IsMinusPressed())
        {
            _teleportDistance -= 1;
        }

        if (_keyInputService.IsPlusPressed())
        {
            _teleportDistance += 1;
        }
    }

    private void Teleport(Vector3 targetPosition)
    {
        _motor.SetPosition(targetPosition);
        _motor.BaseVelocity = Vector3.zero;
    }

    private void ToggleSpeedBoost()
    {
        _isSpeedHackEnabled = !_isSpeedHackEnabled;

        if (_isSpeedHackEnabled)
        {
            _characterController.MaxStableMoveSpeed *= _speedMultiplier;
            _characterController.MaxAirMoveSpeed *= _speedMultiplier;
        }
        else
        {
            ResetSpeed();
        }
    }

    private void ResetSpeed()
    {
        _characterController.MaxStableMoveSpeed = _originalMaxStableMoveSpeed;
        _characterController.MaxAirMoveSpeed = _originalMaxAirMoveSpeed;
    }
}
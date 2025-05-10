using KinematicCharacterController.Examples;
using KinematicCharacterController;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class SaveablePlayer : BaseStorage
{
    [SerializeField] private KinematicCharacterMotor _kinematicCharacterMotor;
    [SerializeField] private ExamplePlayer _examplePlayer;

    private Rigidbody _rigidbody;
    private PlayerData _playerData;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerData = new PlayerData();
    }

    private void OnEnable()
    {
        SetSaveData(_playerData);
        OnStorageEnable();
    }

    private void OnDisable()
    {
        OnStorageDisable();
    }

    public override void Load(SaveData saveData)
    {
        if (saveData.GetType() != typeof(PlayerData))
        {
            Debug.LogError($"{GetSaveKey()} - Getted wrong SaveData. Needed - {nameof(PlayerData)}, Getted - {saveData.GetType().Name}");
            return;
        }

        _playerData = saveData as PlayerData;
        _kinematicCharacterMotor.SetPosition(_playerData.Position.ApplyToVector3());
        _kinematicCharacterMotor.SetRotation(_playerData.Rotation.ApplyToQuaterniuon());
        _rigidbody.velocity = _playerData.Velocity.ApplyToVector3();
        _rigidbody.angularVelocity = _playerData.AngularVelocity.ApplyToVector3();
        _rigidbody.isKinematic = _playerData.IsKinematic;
        _examplePlayer.SetCameraRotation(_playerData.CameraRotation.ApplyToQuaterniuon());
    }

    public override void Save()
    {
        _playerData.Position = new VectorConvertor(transform.position);
        _playerData.Rotation = new QuaternionConvertor(transform.rotation);
        _playerData.Velocity = new VectorConvertor(_rigidbody.velocity);
        _playerData.AngularVelocity = new VectorConvertor(_rigidbody.angularVelocity);
        _playerData.IsKinematic = _rigidbody.isKinematic;
        _playerData.CameraRotation = new QuaternionConvertor(_examplePlayer.CharacterCamera.gameObject.transform.rotation);
    }
}

[Serializable]
public class PlayerData : SaveData
{
    public VectorConvertor Position;
    public QuaternionConvertor Rotation;
    public QuaternionConvertor CameraRotation;
    public VectorConvertor Velocity;
    public VectorConvertor AngularVelocity;
    public bool IsKinematic;

    public PlayerData(){}
}
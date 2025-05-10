using Storage.Scripts;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SaveableDoor : BaseStorage
{
    private Door _door;
    private Rigidbody _rigidbody;
    private DoorData _doorData;

    private void Awake()
    {
        _door = GetComponent<Door>();
        _rigidbody = GetComponent<Rigidbody>();
        _doorData = new DoorData(_door);
    }

    private void OnEnable()
    {
        SetSaveData(_doorData);
        OnStorageEnable();
    }

    private void OnDisable()
    {
        OnStorageDisable();
    }

    public override void Load(SaveData saveData)
    {
        if (saveData.GetType() != typeof(DoorData))
        {
            Debug.LogError($"{GetSaveKey()} - Getted wrong SaveData. Needed - {nameof(DoorData)}, Getted - {saveData.GetType().Name}");
            return;
        }

        _doorData = saveData as DoorData;
        transform.position = _doorData.Position.ApplyToVector3();
        transform.rotation = _doorData.Rotation.ApplyToQuaterniuon();
        _rigidbody.velocity = _doorData.Velocity.ApplyToVector3();
        _rigidbody.angularVelocity = _doorData.AngularVelocity.ApplyToVector3();
        _rigidbody.isKinematic = _doorData.IsKinematic;
        _door.SetIsOpen(_doorData.IsOpen);
    }

    public override void Save()
    {
        _doorData.Position = new VectorConvertor(transform.position);
        _doorData.Rotation = new QuaternionConvertor(transform.rotation);
        _doorData.Velocity = new VectorConvertor(_rigidbody.velocity);
        _doorData.AngularVelocity = new VectorConvertor(_rigidbody.angularVelocity);
        _doorData.IsKinematic = _rigidbody.isKinematic;
    }
}

[Serializable]
public class DoorData : SaveData
{
    public VectorConvertor Position;
    public QuaternionConvertor Rotation;
    public VectorConvertor Velocity;
    public VectorConvertor AngularVelocity;
    public bool IsKinematic;
    public bool IsOpen;

    public DoorData(){}

    public DoorData(Door door)
    {
        IsOpen = door.IsOpen;
    }
}
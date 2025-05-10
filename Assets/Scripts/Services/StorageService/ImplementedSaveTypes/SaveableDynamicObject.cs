using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SaveableDynamicObject : BaseStorage
{
    [SerializeField] private string _prefabId;  

    private Rigidbody _rigidbody;
    private DynamicData _dynamicData;

    public DynamicData DynamicData => _dynamicData;
    public string PrefabId => _prefabId;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _dynamicData = new DynamicData(true);
    }

    private void OnEnable()
    {
        SetSaveData(_dynamicData);
        OnStorageEnable();
    }

    private void OnDisable()
    {
        OnStorageDisable();
    }

    public override void Load(SaveData saveData)
    {
        if (saveData.GetType() != typeof(DynamicData))
        {
            Debug.LogError($"{GetSaveKey()} - Getted wrong SaveData. Needed - {nameof(DynamicData)}, Getted - {saveData.GetType().Name}");
            return;
        }

        _dynamicData = saveData as DynamicData;
        transform.position = _dynamicData.Position.ApplyToVector3();
        transform.rotation = _dynamicData.Rotation.ApplyToQuaterniuon();
        _rigidbody.velocity = _dynamicData.Velocity.ApplyToVector3();
        _rigidbody.angularVelocity = _dynamicData.AngularVelocity.ApplyToVector3();
        _rigidbody.isKinematic = _dynamicData.IsKinematic;
        _prefabId = _dynamicData.PrefabID;
        gameObject.name = _dynamicData.Name;
    }

    public override void Save()
    {
        Debug.Log("Save start");
        _dynamicData.Position = new VectorConvertor(transform.position);
        _dynamicData.Rotation = new QuaternionConvertor(transform.rotation);
        _dynamicData.Velocity = new VectorConvertor(_rigidbody.velocity);
        _dynamicData.AngularVelocity = new VectorConvertor(_rigidbody.angularVelocity);
        _dynamicData.IsKinematic = _rigidbody.isKinematic;
        _dynamicData.PrefabID = _prefabId;
        _dynamicData.Name = gameObject.name;
        Debug.Log("Save end");
    }
}

[Serializable]
public class DynamicData : SaveData
{
    public VectorConvertor Position;
    public QuaternionConvertor Rotation;
    public VectorConvertor Velocity;
    public VectorConvertor AngularVelocity;
    public bool IsKinematic;
    public string PrefabID;
    public string Name;

    public DynamicData() { }

    public DynamicData(bool dynamic)
    {
        IsDynamic = dynamic;
    }
}

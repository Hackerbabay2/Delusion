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

    public override void Load(SaveData saveData)
    {
        if (saveData.GetType() != typeof(DynamicData))
        {
            Debug.LogError($"{GetSaveKey()} - Getted wrong SaveData. Needed - {nameof(DynamicData)}, Getted - {saveData.GetType().Name}");
            return;
        }

        _dynamicData = saveData as DynamicData;

        if (_dynamicData.IsDestroyed)
        {
            gameObject.SetActive(false);
            return;
        }
        else
        {
            gameObject.SetActive(true);
        }

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
        _dynamicData.Position = new VectorConvertor(transform.position);
        _dynamicData.Rotation = new QuaternionConvertor(transform.rotation);
        _dynamicData.Velocity = new VectorConvertor(_rigidbody.velocity);
        _dynamicData.AngularVelocity = new VectorConvertor(_rigidbody.angularVelocity);
        _dynamicData.IsKinematic = _rigidbody.isKinematic;
        _dynamicData.PrefabID = _prefabId;
        _dynamicData.Name = gameObject.name;
    }

    public void SetDestroyed(bool isDestroyed = true)
    {
        _dynamicData.IsDestroyed = isDestroyed;
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
    public bool IsDestroyed;

    public DynamicData() { }

    public DynamicData(bool dynamic)
    {
        IsDynamic = dynamic;
    }
}

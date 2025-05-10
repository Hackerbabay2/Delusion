using System;
using UnityEngine;

namespace Storage.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class SaveableStaticObject : BaseStorage
    {
        private Rigidbody _rigidbody;
        private TransformData _transformData;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transformData = new TransformData();
        }

        private void OnEnable()
        {
            SetSaveData(_transformData);
            OnStorageEnable();
        }

        private void OnDisable()
        {
            OnStorageDisable();
        }

        public override void Load(SaveData saveData)
        {
            if (saveData.GetType() != typeof(TransformData))
            {
                Debug.LogError($"{GetSaveKey()} - Getted wrong SaveData. Needed - {nameof(TransformData)}, Getted - {saveData.GetType().Name}");
                return;
            }

            _transformData = saveData as TransformData;
            transform.position = _transformData.Position.ApplyToVector3();
            transform.rotation = _transformData.Rotation.ApplyToQuaterniuon();
            _rigidbody.velocity = _transformData.Velocity.ApplyToVector3();
            _rigidbody.angularVelocity = _transformData.AngularVelocity.ApplyToVector3();
            _rigidbody.isKinematic= _transformData.IsKinematic;
        }

        public override void Save()
        {
            _transformData.Position = new VectorConvertor(transform.position);
            _transformData.Rotation = new QuaternionConvertor(transform.rotation);
            _transformData.Velocity = new VectorConvertor(_rigidbody.velocity);
            _transformData.AngularVelocity = new VectorConvertor(_rigidbody.angularVelocity);
            _transformData.IsKinematic = _rigidbody.isKinematic;
        }
    }
    
    [Serializable]
    public class TransformData : SaveData
    {
        public VectorConvertor Position;
        public QuaternionConvertor Rotation;
        public VectorConvertor Velocity;
        public VectorConvertor AngularVelocity;
        public bool IsKinematic;

        public TransformData(){}
    }
}
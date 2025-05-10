using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Storage.Scripts.SaveableStaticObject))]
public class MoveableObject : MonoBehaviour, IInteractable
{
    public event Action<MoveableObject> OnDropped;

    [Header("Physics Settings")]
    [SerializeField] private float _maxDragDistance = 5f;
    [SerializeField] private float _minDragDistance = 1f;
    [SerializeField] private float _distanceChangeSpeed = 2f;
    [SerializeField] private float _rotationLerpSpeed = 5f;
    [SerializeField] private float _throwForce = 15f;
    [SerializeField] private float _springForce = 300f;
    [SerializeField] private float _damping = 1f;

    private Rigidbody _rigidbody;
    private bool _isDragging = false;
    private float _currentDragDistance;
    private Camera _mainCamera;
    private float _originalDrag;
    private float _originalAngularDrag;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
        _currentDragDistance = (_minDragDistance + _maxDragDistance) / 2f;
        _originalDrag = _rigidbody.drag;
        _originalAngularDrag = _rigidbody.angularDrag;
    }

    public void Interact()
    {
        if (_isDragging)
        {
            DropObject();
        }
        else
        {
            GrabObject();
        }
    }

    private void Update()
    {
        if (!_isDragging) return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        _currentDragDistance += scroll * _distanceChangeSpeed;
        _currentDragDistance = Mathf.Clamp(_currentDragDistance, _minDragDistance, _maxDragDistance);

        if (Input.GetMouseButtonDown(0))
        {
            ThrowObject();
        }
    }

    private void GrabObject()
    {
        _isDragging = true;
        _rigidbody.useGravity = false;
        _rigidbody.drag = 10f;
        _rigidbody.angularDrag = 10f;

        _currentDragDistance = Vector3.Distance(transform.position, _mainCamera.transform.position);
        _currentDragDistance = Mathf.Clamp(_currentDragDistance, _minDragDistance, _maxDragDistance);
    }

    public void DropObject()
    {
        _isDragging = false;
        ResetPhysics();
        OnDropped?.Invoke(this);
    }

    private void ThrowObject()
    {
        _isDragging = false;
        ResetPhysics();

        Vector3 throwDirection = _mainCamera.transform.forward;
        _rigidbody.AddForce(throwDirection * _throwForce, ForceMode.Impulse);
    }

    private void ResetPhysics()
    {
        _rigidbody.useGravity = true;
        _rigidbody.drag = _originalDrag;
        _rigidbody.angularDrag = _originalAngularDrag;
    }

    private void FixedUpdate()
    {
        if (!_isDragging) return;

        Vector3 targetPosition = _mainCamera.transform.position + _mainCamera.transform.forward * _currentDragDistance;
        Vector3 direction = targetPosition - transform.position;
        Vector3 desiredVelocity = direction * _springForce;
        Vector3 dampingForce = -_rigidbody.velocity * _damping;

        _rigidbody.AddForce(desiredVelocity + dampingForce);

        Quaternion targetRotation = Quaternion.LookRotation(transform.position - _mainCamera.transform.position);
        _rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, _rotationLerpSpeed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isDragging)
        {
            _currentDragDistance = Vector3.Distance(transform.position, _mainCamera.transform.position);

            if (_currentDragDistance > _maxDragDistance)
            {
                DropObject();
            }
        }
    }
}
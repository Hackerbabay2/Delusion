using KinematicCharacterController.Examples;
using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class MoveableObject : MonoBehaviour, IInteractable
{
    public event Action<MoveableObject> OnDropped;

    [Header("Physics Settings")]
    [SerializeField] private float _maxDragDistance = 5f;
    [SerializeField] private float _minDragDistance = 1f;
    [SerializeField] private float _distanceChangeSpeed = 2f;
    [SerializeField] private float _rotationLerpSpeed = 5f;
    [SerializeField] private float _throwForce = 15f;
    [SerializeField] private float _springForce = 50f;
    [SerializeField] private float _damping = 1f;
    [SerializeField] private float _rotationSpeed = 90f;

    [Inject] private ExamplePlayer _examplePlayer;

    private Vector2 _rotationInput;
    private bool _shouldUnlockCursorOnRelease = false;
    private Rigidbody _rigidbody;
    private bool _isDragging = false;
    private float _currentDragDistance;
    private Camera _mainCamera;
    private float _originalDrag;
    private float _originalAngularDrag;
    private bool _isRotating = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
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

        if (Input.GetMouseButtonDown(1))
        {
            _examplePlayer.enabled = false;
            StartRotating();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _examplePlayer.enabled = true;
            StopRotating();
        }

        if (_isRotating)
        {
            _rotationInput.x = Input.GetAxis("Mouse X");
            _rotationInput.y = Input.GetAxis("Mouse Y");
        }
        else
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            _currentDragDistance += scroll * _distanceChangeSpeed;
            _currentDragDistance = Mathf.Clamp(_currentDragDistance, _minDragDistance, _maxDragDistance);

            if (Input.GetMouseButtonDown(0))
            {
                ThrowObject();
            }
        }
    }

    private void StartRotating()
    {
        _isRotating = true;
        _rotationInput = Vector2.zero;

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            _shouldUnlockCursorOnRelease = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            _shouldUnlockCursorOnRelease = true;
        }
    }

    private void StopRotating()
    {
        _isRotating = false;

        if (_shouldUnlockCursorOnRelease)
        {
            Cursor.lockState = CursorLockMode.None;
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
        _isRotating = false;
        ResetPhysics();
        OnDropped?.Invoke(this);
    }

    private void ThrowObject()
    {
        _isDragging = false;
        _isRotating = false;
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

        if (_isRotating && _rotationInput != Vector2.zero)
        {
            Vector3 rotation = new Vector3(
                -_rotationInput.y * _rotationSpeed,
                _rotationInput.x * _rotationSpeed,
                0
            ) * Time.fixedDeltaTime;

            Quaternion deltaRotation = Quaternion.Euler(rotation);
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);

            _rotationInput = Vector2.zero;
        }
        else if (!_isRotating)
        {
            Vector3 targetPosition = _mainCamera.transform.position + _mainCamera.transform.forward * _currentDragDistance;
            Vector3 direction = targetPosition - transform.position;
            Vector3 desiredVelocity = direction * _springForce;
            Vector3 dampingForce = -_rigidbody.velocity * _damping;
            _rigidbody.AddForce(desiredVelocity + dampingForce);
        }
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
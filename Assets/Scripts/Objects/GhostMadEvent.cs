using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMadEvent : MonoBehaviour
{
    [Header("Cast Settings")]
    [SerializeField] private float _castRadius = 15f;
    [SerializeField] private float _eventDuration = 30f;
    [SerializeField] private float _minForceKick = 5f;
    [SerializeField] private float _maxForceKick = 15f;

    private Coroutine _interactForDuration;

    private void OnEnable()
    {
        _interactForDuration = StartCoroutine(InteractForDuration());
    }

    private void OnDisable()
    {
        if (_interactForDuration != null)
        {
            StopCoroutine(InteractForDuration());
            _interactForDuration = null;
        }
    }

    private IEnumerator InteractForDuration()
    {
        while (true)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _castRadius);
            ProcessAffectedObjects(hitColliders);
            yield return new WaitForSeconds(_eventDuration);
        }
    }

    private void ProcessAffectedObjects(Collider[] colliders)
    {
        foreach (Collider collider in colliders)
        {
            if (collider.transform.TryGetComponent(out MoveableObject moveableObject))
            {
                if (Random.Range(0f, 1f) <= 0.15f)
                {
                    Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
                    Vector3 randomDirection = Random.onUnitSphere;
                    rigidbody.AddForce(randomDirection * Random.Range(_minForceKick, _maxForceKick), ForceMode.Impulse);
                }
                continue;
            }

            if (collider.transform.TryGetComponent(out Door door))
            {
                if (Random.Range(0f,1f) <= 0.15f)
                {
                    door.Interact();
                }
                continue;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _castRadius);
    }
}

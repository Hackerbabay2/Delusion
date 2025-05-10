using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody), typeof(InteractiveSoundEffecter))]
public class Randomkick : MonoBehaviour
{
    [SerializeField] private float _minTimeKick = 5f;
    [SerializeField] private float _maxTimeKick = 30f;
    [SerializeField] private float _minForceKick = 5f;
    [SerializeField] private float _maxForceKick = 15f;
    
    private bool isWaitingForKick = false;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isWaitingForKick == false)
        {
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        isWaitingForKick = true;
        yield return new WaitForSeconds(Random.Range(_minTimeKick, _maxTimeKick));
        isWaitingForKick = false;
        Kick();
    }

    public void Kick()
    {
        Vector3 randomDirection = Random.onUnitSphere;

        _rigidbody.AddForce(randomDirection * Random.Range(_minForceKick, _maxForceKick), ForceMode.Impulse);
    }
}
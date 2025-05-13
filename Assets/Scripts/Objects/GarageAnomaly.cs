using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageAnomaly : MonoBehaviour
{
    [SerializeField] private float _interval = 5f;
    [SerializeField] private float _castRadius = 15f;

    private Coroutine _turnOffLight;

    private IEnumerator TurnOffLightForInterval()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_interval);

        while (true)
        {
            yield return waitForSeconds;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _castRadius);

            foreach (Collider collider in hitColliders)
            {
                Light light = collider.GetComponentInChildren<Light>();

                if (light != null)
                {
                    if (Random.Range(0f, 1f) <= 0.5f)
                    {
                        light.enabled = false;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out ExamplePlayer player))
        {
            _turnOffLight = StartCoroutine(TurnOffLightForInterval());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent(out ExamplePlayer player))
        {
            if (_turnOffLight != null)
            {
                StopCoroutine(_turnOffLight);
                _turnOffLight = null;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _castRadius);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class HelicopterPathMover : MonoBehaviour
{
    [SerializeField] private List<Transform> _pathPoints;
    [SerializeField] private GameObject _helicopterPrefab;
    [SerializeField] private ElectricalPanel _electricalPanel;
    [SerializeField] private float _helicopterSpeed = 10f;
    [SerializeField] private float _loadDuration = 5f;
    [SerializeField] private float _forcePower = 10f;
    [SerializeField] private float _torquePower = 10f;

    private GameObject _helicopter;

    public void LaunchHelicopter()
    {
        if (_pathPoints.Count == 0)
        {
            Debug.LogWarning("No path points set for the helicopter.");
            return;
        }
        _helicopter = Instantiate(_helicopterPrefab, _pathPoints[0].position, Quaternion.identity);
        StartCoroutine(MoveHelicopter(_helicopter, _helicopterSpeed));
    }

    private IEnumerator MoveHelicopter(GameObject helicopter, float duration)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(duration);

        for (int i = 0; i < _pathPoints.Count; i++)
        {
            Transform targetPoint = _pathPoints[i];

            helicopter.transform.DOMove(targetPoint.position, duration)
                .SetEase(Ease.Linear);
            helicopter.transform.DORotate(targetPoint.rotation.eulerAngles, duration)
                .SetEase(Ease.Linear);
            yield return waitForSeconds;
        }
        CheckForEnding();
        yield return null;
    }

    private IEnumerator LoadSceneForDuration(float duration, string sceneName)
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(sceneName);
    }

    private void CheckForEnding()
    {
        if (_electricalPanel.IsTurn)
        {
            Rigidbody rigidbody = _helicopter.AddComponent<Rigidbody>();
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rigidbody.AddForce(Vector3.right * _forcePower, ForceMode.Impulse);
            rigidbody.AddTorque(Vector3.up * _torquePower, ForceMode.Impulse);
            StartCoroutine(LoadSceneForDuration(_loadDuration, "BadEnding"));
        }
        else
        {
            StartCoroutine(LoadSceneForDuration(_loadDuration, "GoodEnding"));
        }
    }
}
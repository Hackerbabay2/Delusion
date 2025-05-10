using System.Collections;
using UnityEngine;

public class ShowNotification : MonoBehaviour
{
    [SerializeField] private GameObject _notification;

    public void Show()
    {
        _notification.SetActive(true);
        StartCoroutine(HideForDuration(1));
    }

    public void Hide()
    {
        _notification?.SetActive(false);
    }

    private IEnumerator HideForDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        Hide();
    }
}
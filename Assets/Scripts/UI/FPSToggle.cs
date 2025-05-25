using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class FPSToggle : MonoBehaviour
{
    [SerializeField] private GameObject _fpsLabel;

    private Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    public void Toggle()
    {
        _fpsLabel.SetActive(_toggle.isOn);
    }
}

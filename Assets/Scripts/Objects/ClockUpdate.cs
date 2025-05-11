using TMPro;
using UnityEngine;

public class ClockUpdate : MonoBehaviour
{
    [SerializeField] private DayCycle _dayCycle;
    [SerializeField] private TMP_Text _timeText;

    private void Update()
    {
        _timeText.text = _dayCycle.GetCurrentTimeFormatted();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DayEventService : MonoBehaviour
{
    [SerializeField] private List<DayEvent> _dayEvents;

    private int _currentDay = 0;

    public void RegisterEvent(DayEvent dayEvent)
    {
        if (_dayEvents.Contains(dayEvent) == false)
        {
            _dayEvents.Add(dayEvent);
        }
    }

    public void UnregisterEvenet(DayEvent dayEvent)
    {
        if (_dayEvents.Contains(dayEvent))
        {
            _dayEvents.Remove(dayEvent);
        }
    }

    public void DayStated()
    {
        _currentDay++;

        foreach (DayEvent dayEvent in _dayEvents)
        {
            dayEvent.CheckNeededDay(_currentDay);
        }
    }
}
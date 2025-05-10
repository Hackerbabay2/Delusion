using UnityEngine;
using Zenject;

public abstract class DayEvent : MonoBehaviour
{
    [SerializeField] protected int NeededDay;
    
    [Inject] protected DayEventService DayEventService;

    public virtual void OnEnable()
    {
        DayEventService.RegisterEvent(this);
    }

    public virtual void OnDisable()
    {
        DayEventService.UnregisterEvenet(this);
    }


    public virtual void CheckNeededDay(int currentDay)
    {
        if (currentDay == NeededDay)
        {
            EventStart();
        }
    }

    public abstract void EventStart();
}

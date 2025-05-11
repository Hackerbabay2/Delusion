using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FatigueValueChanger : ValueChanger
{
    [Inject] private PlayerStats _playerStats;

    public void UpdateValue()
    {
        ChangeText($"Усталость: {_playerStats.Fatigue.ToString("00.0")}");
    }
}

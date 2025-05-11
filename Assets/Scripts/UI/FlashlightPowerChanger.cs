using TMPro;
using Zenject;

public class FlashlightPowerChanger : ValueChanger
{
    [Inject] private PlayerStats _playerStats;

    public void UpdateValue()
    {
        ChangeText($"����� ��������: {_playerStats.FlashLightPower.ToString("00.0")}");
    }
}
using TMPro;
using Zenject;

public class FlashlightPowerChanger : ValueChanger
{
    [Inject] private PlayerStats _playerStats;

    private TMP_Text _flashlightText;

    private void Awake()
    {
        _flashlightText = GetComponent<TMP_Text>();
    }

    public void UpdateValue()
    {
        ChangeText($"Flashlight: {_playerStats.FlashLightPower.ToString("#.##")}");
    }

    public override void ChangeText(string text)
    {
        _flashlightText.text = text;
    }
}
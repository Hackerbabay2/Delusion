using System;
using UnityEngine;

public class KeyInputService
{
    private Keybinds _keybinds;

    public KeyInputService()
    {
        _keybinds = new Keybinds();
        _keybinds.Enable();
    }

    public bool IsMinusPressed()
    {
        return _keybinds.CharacterMovement.Minus.triggered;
    }

    public bool IsPlusPressed()
    {
        return _keybinds.CharacterMovement.Plus.triggered;
    }

    public bool IsKPressed()
    {
        return _keybinds.CharacterMovement.K.triggered;
    }

    public bool IsF8Pressed()
    {
        return _keybinds.CharacterMovement.F8.triggered;
    }

    public bool IsUsePressed()
    {
        return _keybinds.CharacterMovement.Use.triggered;
    }

    public bool IsFlashLightPressed()
    {
        return _keybinds.Interface.Flashlight.triggered;
    }

    public bool IsSavePressed()
    {
        return _keybinds.Interface.Save.triggered;
    }

    public bool IsLoadPressed()
    {
        return _keybinds.Interface.Load.triggered;
    }

    public bool IsInterectivePressed()
    {
        return _keybinds.CharacterMovement.Interactive.triggered;
    }

    public Vector2 GetMovementVector()
    {
        return _keybinds.CharacterMovement.Movement.ReadValue<Vector2>();
    }

    public bool IsF1Pressed()
    {
        return _keybinds.CharacterMovement.FirstPerson.triggered;
    }

    public bool IsF2Pressed()
    {
        return _keybinds.CharacterMovement.SecondPerson.triggered;
    }

    public bool IsF3Pressed()
    {
        return _keybinds.CharacterMovement.ThridPerson.triggered;
    }

    public bool IsMenuPressed()
    {
        return _keybinds.Interface.Menu.triggered;
    }
}

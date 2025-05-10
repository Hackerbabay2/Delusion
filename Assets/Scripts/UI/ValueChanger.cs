using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public abstract class ValueChanger : MonoBehaviour
{
    public abstract void ChangeText(string text);
}

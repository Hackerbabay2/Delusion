using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public abstract class ValueChanger : MonoBehaviour
{
    protected TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public virtual void ChangeText(string text)
    {
        _text.text = text;
    }
}

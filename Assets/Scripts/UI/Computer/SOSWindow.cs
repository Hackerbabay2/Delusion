using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SOSWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _sosField;
    [SerializeField] private ElectricalPanel _electricalPanel;
    [SerializeField] private UnityEvent _onSosSend;
    [SerializeField] private ScrollRect _scrollRect;

    private bool _isSignalSent = false;

    public bool IsSignalSent => _isSignalSent;

    private void OnEnable()
    {
        if (_electricalPanel.IsTurn)
        {
            AddLog("Подключено!\n");
        }
        else
        {
            AddLog("Нет соединения...\n");
        }
    }

    private void OnDisable()
    {
        _isSignalSent = false;
        _sosField.text = string.Empty;
    }

    public void SetSignalSet(bool signalSet)
    {
        _sosField.text = string.Empty;
        _isSignalSent = signalSet;
    }

    public void OnSendSOSSignalButtonClick()
    {
        if (_electricalPanel.IsTurn && _isSignalSent == false)
        {
            StartCoroutine(SendSignal());
            _isSignalSent = true;
            _onSosSend?.Invoke();
        }
        else
        {
            AddLog("Не возможно отправить сигнал\n");
        }
    }

    public void OnCloseSOSWindowClick()
    {
        gameObject.SetActive(false);    
    }

    private IEnumerator SendSignal()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);
        yield return waitForSeconds;
        AddLog("Координаты получены..\n");
        yield return waitForSeconds;
        AddLog("SOS. Это потерпевший в кратере. \r\nТребуется эвакуация. \r\nВнимание: в зоне сильные электромагнитные аномалии. \r\nБудьте готовы к аварийному ручному управлению.\n");
        yield return waitForSeconds;
        AddLog("SOS сигнал отправлен!\n");
    }

    public void CheckLineCount()
    {
        if (_sosField.textInfo.lineCount > 100)
        {
            _sosField.text = string.Join("\n", _sosField.text.Split('\n').Skip(50));
        }
    }

    public void AddLog(string message)
    {
        _sosField.text = message + "\n" + _sosField.text;
        CheckLineCount();
        Canvas.ForceUpdateCanvases();
        _scrollRect.verticalNormalizedPosition = 0;
    }
}
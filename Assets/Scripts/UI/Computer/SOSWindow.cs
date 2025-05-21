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

    private void OnEnable()
    {
        if (_electricalPanel.IsRepaired)
        {
            AddLog("����������!\n");
        }
        else
        {
            AddLog("��� ����������...\n");
        }
    }

    public void OnSendSOSSignalButtonClick()
    {
        if (_electricalPanel.IsRepaired && _isSignalSent == false)
        {
            StartCoroutine(SendSignal());
            _isSignalSent = true;
            _onSosSend?.Invoke();
        }
        else
        {
            AddLog("�� �������� ��������� ������\n");
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
        AddLog("���������� ��������..\n");
        yield return waitForSeconds;
        AddLog("SOS. ��� ����������� � �������. \r\n��������� ���������. \r\n��������: � ���� ������� ���������������� ��������. \r\n������ ������ � ���������� ������� ����������.\n");
        yield return waitForSeconds;
        AddLog("SOS ������ ���������!\n");
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
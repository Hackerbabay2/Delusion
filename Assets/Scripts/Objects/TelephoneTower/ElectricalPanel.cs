using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ElectricalPanel : MonoBehaviour
{
    [SerializeField] private List<Transform> _slots = new List<Transform>();
    [SerializeField] protected UnityEvent _onSlotsComplete;

    private bool _isRepaired = false;
    private bool _turn = false;
    private int _enabledSlotsCount = 0;

    public bool IsRepaired => _isRepaired;
    public int EnabledSlotsCount => _enabledSlotsCount; 
    public bool IsTurn => _turn;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Fuse fuse))
        {
            MoveableObject moveableObject = fuse.gameObject.GetComponent<MoveableObject>();

            if (moveableObject != null)
            {
                SaveableDynamicObject saveableDynamicObject = moveableObject.GetComponent<SaveableDynamicObject>();
                moveableObject.DropObject();
                Transform slotForFuse = _slots.Find(slot => slot.gameObject.transform.childCount > 0 && slot.GetChild(0).gameObject.activeSelf == false);

                if (slotForFuse != null)
                {
                    slotForFuse.GetChild(0).gameObject.SetActive(true);
                    moveableObject.gameObject.SetActive(false);
                    saveableDynamicObject.SetDestroyed();
                    _enabledSlotsCount++;
                }
            }

            if (_slots.All(slot => slot.childCount > 0 && slot.GetChild(0).gameObject.activeSelf))
            {
                _onSlotsComplete?.Invoke();
                _isRepaired = true;
            }
        }
    }

    public void TurnTower()
    {
        _turn = !_turn;
    }

    public void SetEnabledSlots(int enabledSlotsCount)
    {
        _enabledSlotsCount = 0;

        foreach (Transform slot in _slots)
        {
            slot.GetChild(0).gameObject.SetActive(false);
        }

        for (int i = 0; i < enabledSlotsCount; i++)
        {
            Transform slotForFuse = _slots.Find(slot => slot.gameObject.transform.childCount > 0 && slot.GetChild(0).gameObject.activeSelf == false);

            if (slotForFuse != null)
            {
                slotForFuse.GetChild(0).gameObject.SetActive(true);
                _enabledSlotsCount++;
            }
        }
    }

    public void SetState(bool enable)
    {
        _turn = enable;
    }

    internal void SetRepaired(bool isRepaired)
    {
        _isRepaired = isRepaired;

        if (_isRepaired)
        {
            _onSlotsComplete?.Invoke();
        }
    }
}
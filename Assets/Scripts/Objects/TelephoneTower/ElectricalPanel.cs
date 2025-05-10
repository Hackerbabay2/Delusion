using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ElectricalPanel : MonoBehaviour
{
    [SerializeField] private List<Transform> _slots = new List<Transform>();
    [SerializeField] protected UnityEvent _onSlotsComplete;

    private const int NeedBattery = 3;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Fuse fuse))
        {
            MoveableObject moveableObject = fuse.gameObject.GetComponent<MoveableObject>();
            Rigidbody rigidbody = moveableObject.GetComponent<Rigidbody>();

            if (moveableObject != null)
            {
                moveableObject.DropObject();
                Transform slotForFuse = _slots.Find(slot => slot.gameObject.transform.childCount == 0);
                fuse.transform.SetParent(slotForFuse, true);
                fuse.transform.localPosition = Vector3.zero;
                fuse.transform.localRotation = Quaternion.identity;
                rigidbody.isKinematic = true;
            }

            if (_slots.Find(slot => slot.gameObject.transform.childCount == 0) == null)
            {
                _onSlotsComplete?.Invoke();
            }
        }
    }
}
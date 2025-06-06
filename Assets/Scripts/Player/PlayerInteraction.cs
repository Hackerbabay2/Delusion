using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private GameObject _prefab;

    private KeyInputService _keyInputService;
    private MoveableObject _currentHeldObject;

    private Outline _outline;

    private void Awake()
    {
        _keyInputService = new KeyInputService();
    }

    private void Update()
    {
        if (_keyInputService.IsInterectivePressed())
        {
            TryInteract();
        }

        if (_keyInputService.IsUsePressed())
        {
            TryUse();
        }

        OutlineInterectiveObject();
    }

    private void OutlineInterectiveObject()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        bool isHit = Physics.Raycast(ray, out hit, interactionDistance);

        if (isHit)
        {
            if (hit.transform.TryGetComponent(out Outline outline))
            {
                if (_outline != null && _outline != outline)
                {
                    _outline.enabled = false;
                }
                outline.enabled = true;
                _outline = outline;
            }
        }
        else
        {
            if (_outline != null)
            {
                _outline.enabled = false;
            }
        }
    }

    private void TryUse()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Use();
            }
        }
    }

    private void TryInteract()
    {
        if (_currentHeldObject != null)
        {
            _currentHeldObject.DropObject();
            _currentHeldObject = null;
            return;
        }

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();

                if (interactable is MoveableObject moveable)
                {
                    _currentHeldObject = moveable;
                    moveable.OnDropped += HandleObjectDropped;
                }
            }
        }
    }

    private void HandleObjectDropped(MoveableObject droppedObject)
    {
        if (_currentHeldObject == droppedObject)
        {
            _currentHeldObject.OnDropped -= HandleObjectDropped;
            _currentHeldObject = null;
        }
    }
}
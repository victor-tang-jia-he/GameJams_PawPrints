using UnityEngine;
using UnityEngine.Events;


public interface IInteractable
{
    public UnityAction OnInteractAction { get; set; }
    protected bool CanInteract();

    protected void OnInteract();
    public bool TryInteract()
    {
        bool canInteract = CanInteract();
        if (canInteract)
        {
            OnInteract();
            OnInteractAction.Invoke();
        }
        return canInteract;
    }
}

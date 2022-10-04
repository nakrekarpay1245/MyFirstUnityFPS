using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AmmoBoxInteractable : Interactable
{
    [SerializeField]
    private UnityEvent Focus;
    [SerializeField]
    private UnityEvent Interact;
    [SerializeField]
    private UnityEvent LoseFocus;

    public override void OnFocus()
    {
        Focus.Invoke();
    }

    public override void OnInteract()
    {
        Interact.Invoke();
        Destroy(gameObject, 1);
        OnLoseFocus();
    }

    public override void OnLoseFocus()
    {
        LoseFocus.Invoke();
    }
}

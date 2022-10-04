using UnityEngine;
using System.Collections;

public class TestInteractable : Interactable
{
    public override void OnInteract()
    {
        Debug.Log("Looking At Object: " + gameObject.name);
    }
    public override void OnFocus()
    {
        Debug.Log("Interacted With: " + gameObject.name);
    }
    public override void OnLoseFocus()
    {
        Debug.Log("Stopping Looking At Object: " + gameObject.name);
    }
}
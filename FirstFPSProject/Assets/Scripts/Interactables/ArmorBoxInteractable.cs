using UnityEngine;
using UnityEngine.Events;

public class ArmorBoxInteractable : Interactable
{
    [SerializeField]
    private float armor;
    public override void OnFocus()
    {
        Focus.Invoke();
        UI.instance.DisplayInteractText(description);
    }

    public override void OnInteract()
    {
        Interact.Invoke();
        Destroy(gameObject, 1);
        collider.enabled = false;
        model.SetActive(false);
        particle.gameObject.SetActive(false);
        audioSource.Play();
        PlayerHealth.instance.IncreaseArmor(armor);
        OnLoseFocus();
    }

    public override void OnLoseFocus()
    {
        LoseFocus.Invoke();
        UI.instance.HideInteractText();
    }
}

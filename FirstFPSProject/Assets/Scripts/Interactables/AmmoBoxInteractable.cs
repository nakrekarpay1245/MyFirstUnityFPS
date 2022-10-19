using UnityEngine;
public class AmmoBoxInteractable : Interactable
{
    [SerializeField]
    private int ammoCount;
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
        Weapon.instance.GetAmmo(ammoCount);
        OnLoseFocus();
    }

    public override void OnLoseFocus()
    {
        LoseFocus.Invoke();
        UI.instance.HideInteractText();
    }
}

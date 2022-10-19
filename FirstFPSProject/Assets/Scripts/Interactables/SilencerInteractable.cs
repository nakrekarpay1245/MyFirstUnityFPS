public class SilencerInteractable : Interactable
{
    public override void OnFocus()
    {
        Focus.Invoke();
        UI.instance.DisplayInteractText(description);
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
        UI.instance.HideInteractText();
    }
}

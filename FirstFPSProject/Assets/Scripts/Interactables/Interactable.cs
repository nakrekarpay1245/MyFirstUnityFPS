using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    protected UnityEvent Focus;
    [SerializeField]
    protected UnityEvent Interact;
    [SerializeField]
    protected UnityEvent LoseFocus;

    [SerializeField]
    protected string description;

    [SerializeField]
    protected GameObject model;

    [SerializeField]
    protected ParticleSystem particle;

    protected Collider collider;
    protected AudioSource audioSource;
    public virtual void Awake()
    {
        collider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        gameObject.layer = 9;
    }

    public abstract void OnInteract();
    public abstract void OnFocus();
    public abstract void OnLoseFocus();
}
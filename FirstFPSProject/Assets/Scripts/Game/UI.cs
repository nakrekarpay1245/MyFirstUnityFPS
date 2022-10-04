using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI healthText;

    [SerializeField]
    private TextMeshProUGUI interactText;

    private void OnEnable()
    {
        CC_FirstPersonController.OnDamage += UpdateHealth;
        CC_FirstPersonController.OnHeal += UpdateHealth;
    }

    private void OnDisable()
    {
        CC_FirstPersonController.OnDamage -= UpdateHealth;
        CC_FirstPersonController.OnHeal -= UpdateHealth;
    }

    private void Start()
    {
        UpdateHealth(100);
        HideInteractText();
    }

    private void UpdateHealth(float currentHealth)
    {
        healthText.text = currentHealth.ToString("00");
    }

    public void DisplayInteractText(string _interactName)
    {
        interactText.text = _interactName;
    }

    public void HideInteractText()
    {
        interactText.text = "";
    }
}
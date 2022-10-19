using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI healthText;
    [SerializeField]
    private Image healthBarFill;

    [SerializeField]
    private TextMeshProUGUI armorText;
    [SerializeField]
    private Image armorBarFill;

    [SerializeField]
    private TextMeshProUGUI interactText;

    private void OnEnable()
    {
        PlayerHealth.OnDamage += UpdateHealth;
        PlayerHealth.OnHeal += UpdateHealth;
        PlayerHealth.OnIncreaseArmor += UpdateArmor;
    }

    private void OnDisable()
    {
        PlayerHealth.OnDamage -= UpdateHealth;
        PlayerHealth.OnHeal -= UpdateHealth;
        PlayerHealth.OnIncreaseArmor -= UpdateArmor;
    }

    public static UI instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        UpdateHealth(100);
        UpdateArmor(0);
    }

    private void Start()
    {
        UpdateHealth(100);
        HideInteractText();
    }

    private void UpdateHealth(float currentHealth)
    {
        healthText.text = currentHealth.ToString("00");
        healthBarFill.fillAmount = currentHealth / 100;
    }

    private void UpdateHealth(float currentHealth, float maxHealth)
    {
        healthText.text = currentHealth.ToString("00");
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    private void UpdateArmor(float currentArmor)
    {
        armorText.text = currentArmor.ToString("00");
        armorBarFill.fillAmount = currentArmor / 100;
    }

    private void UpdateArmor(float currentArmor, float maxArmor)
    {
        armorText.text = currentArmor.ToString("00");
        armorBarFill.fillAmount = currentArmor / maxArmor;
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
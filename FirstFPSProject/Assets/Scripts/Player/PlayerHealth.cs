using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Paremeters")]
    [SerializeField]
    private float maxHealth = 100;
    private float currentHealth;
    public static Action<float> OnTakeDamage;
    public static Action<float> OnDamage;
    public static Action<float> OnHeal;

    [Header("Health Paremeters")]
    [SerializeField]
    private float maxArmor = 100;
    public float currentArmor; 
    public static Action<float> OnIncreaseArmor;

    [SerializeField]
    private AudioSource healthAudioSource;
    [SerializeField]
    private BloodScreenEffect bloodScreenEffect;

    public static PlayerHealth instance;
    private void OnEnable()
    {
        if (!instance)
        {
            instance = this;
        }

        OnTakeDamage += ApplyDamage;
    }

    private void OnDisable()
    {
        OnTakeDamage -= ApplyDamage;
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void IncreaseHealth(float value)
    {
        currentHealth += value;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        OnHeal?.Invoke(currentHealth);
    }

    private void ApplyDamage(float _damage)
    {
        if (!healthAudioSource.isPlaying)
        {
            healthAudioSource.PlayOneShot(healthAudioSource.clip);
        }

        bloodScreenEffect.BloodEffect(currentHealth);
        float damage = _damage - (_damage * (currentArmor / (2 * maxArmor)));
        currentHealth -= damage;
        OnDamage?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            KillPlayer();
        }
    }

    public void IncreaseArmor(float value)
    {
        currentArmor += value;

        if (currentArmor > maxArmor)
        {
            currentArmor = maxArmor;
        }

        OnIncreaseArmor?.Invoke(currentArmor);
    }

    private void KillPlayer()
    {
        currentHealth = 0;
        Debug.Log("The Player Is Just Dead");
    }
}

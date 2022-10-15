using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [Header("Health Paremeters")]
    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private float timeBeforeRegenStart = 3;
    [SerializeField]
    private float healthValueIncrement = 1;
    [SerializeField]
    private float healthTimeIncrement = 0.1f;
    private float currentHealth;
    public float storageHealth;
    private Coroutine regeneratingHealth;
    public static Action<float> OnTakeDamage;
    public static Action<float> OnDamage;
    public static Action<float> OnHeal;
    [SerializeField]
    private AudioSource healthAudioSource;
    [SerializeField]
    private BloodScreenEffect bloodScreenEffect;

    public static PlayerHealth playerHealth;
    private void OnEnable()
    {
        if (!playerHealth)
        {
            playerHealth = this;
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

    private void ApplyDamage(float _damage)
    {
        if (!healthAudioSource.isPlaying)
        {
            healthAudioSource.PlayOneShot(healthAudioSource.clip);
        }
        bloodScreenEffect.BloodEffect();
        currentHealth -= _damage;
        OnDamage?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            KillPlayer();
        }
        else if (regeneratingHealth != null)
        {
            StopCoroutine(regeneratingHealth);
        }

        regeneratingHealth = StartCoroutine(RegenerateHealth());
    }

    private void KillPlayer()
    {
        currentHealth = 0;

        if (regeneratingHealth != null)
        {
            StopCoroutine(regeneratingHealth);
        }

        Debug.Log("The Player Is Just Dead");
    }

    public void IncreaseStorageHealth(int _health)
    {
        if (storageHealth < maxHealth)
        {
            storageHealth += _health;
        }

        if (storageHealth > maxHealth)
        {
            storageHealth = maxHealth;
        }
    }
    private IEnumerator RegenerateHealth()
    {
        yield return new WaitForSeconds(timeBeforeRegenStart);

        WaitForSeconds timeToWait = new WaitForSeconds(healthTimeIncrement);

        while (currentHealth < maxHealth && storageHealth > 0)
        {
            currentHealth += healthValueIncrement;
            storageHealth -= healthValueIncrement;

            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }

            OnHeal?.Invoke(currentHealth);
            yield return timeToWait;
        }

        regeneratingHealth = null;
    }
}

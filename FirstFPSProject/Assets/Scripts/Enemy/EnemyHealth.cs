using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Saðlýk Deðiþkenleri")]
    [SerializeField]
    private float maxHealth;
    private float health;

    private EnemyAI enemyAI;
    private void Awake()
    {
        health = maxHealth;
        enemyAI = GetComponent<EnemyAI>();
    }

    public void TakeDamage(float value)
    {
        Debug.Log("The Enemy Is Just Taked Damage");

        health -= value;
        if (health <= 0)
        {
            Dead();
        }
        Debug.Log("Health: " + health);
    }


    private void Dead()
    {
        Debug.Log("The Enemy Is Just Dead");
        health = 0;
        enemyAI.Dead();
    }

}

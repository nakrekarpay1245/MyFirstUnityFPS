using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTest : MonoBehaviour
{
    [SerializeField]
    private DamageTest[] damageTests;

    [SerializeField]
    private AudioSource audioSource;

    public void AttackActivate()
    {
        for (int i = 0; i < damageTests.Length; i++)
        {
            damageTests[i].gameObject.SetActive(true);
        }
        audioSource.Play();
    }

    public void AttackDeactivate()
    {
        for (int i = 0; i < damageTests.Length; i++)
        {
            damageTests[i].gameObject.SetActive(false);
        }
    }
}

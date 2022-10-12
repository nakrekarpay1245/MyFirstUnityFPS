using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTest : MonoBehaviour
{
    [SerializeField]
    private DamageTest[] damageTests;

    public void AttackActivate()
    {
        for (int i = 0; i < damageTests.Length; i++)
        {
            damageTests[i].gameObject.SetActive(true);
        }
    }

    public void AttackDeactivate()
    {
        for (int i = 0; i < damageTests.Length; i++)
        {
            damageTests[i].gameObject.SetActive(false);
        }
    }
}

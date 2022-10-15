using UnityEngine;

public class DamageTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth.OnTakeDamage(2.5f);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootStep : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField]
    private AudioSource auidoSource;

    public void PlayFootStep()
    {
        auidoSource.pitch = Random.Range(0.95f, 1f);
        auidoSource.PlayOneShot(auidoSource.clip);
    }
}

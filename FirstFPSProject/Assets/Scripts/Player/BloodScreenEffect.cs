using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class BloodScreenEffect : MonoBehaviour
{
    [Header("Sound")]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip breathClip;

    [Header("BloodScreen")]
    [SerializeField]
    private CanvasGroup[] bloodScreens;

    public static BloodScreenEffect instance;

    private void OnEnable()
    {
        PlayerHealth.OnDamage += BloodEffect;
        PlayerHealth.OnHeal += BloodEffect;
    }
    private void OnDisable()
    {
        PlayerHealth.OnDamage -= BloodEffect;
        PlayerHealth.OnHeal -= BloodEffect;
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void BloodEffect(float value)
    {
        BloodScreenActive(1 - (value / 100));
        BloodSoundsPlay();
    }

    private void BloodScreenActive(float value)
    {
        for (int i = 0; i < bloodScreens.Length; i++)
        {
            bloodScreens[i].DOFade(value, 1);
        }
    }


    private void BloodSoundsPlay()
    {
        audioSource.PlayOneShot(breathClip);
    }
}

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

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void BloodEffect()
    {
        StartCoroutine(BloodScreenActive());
        BloodSoundsPlay();
    }

    private IEnumerator BloodScreenActive()
    {
        for (int i = 0; i < bloodScreens.Length; i++)
        {
            bloodScreens[i].DOFade(0.75f, 1);
        }

        yield return new WaitForSeconds(1);

        for (int i = 0; i < bloodScreens.Length; i++)
        {
            bloodScreens[i].DOFade(0, 14);
        }
    }


    private void BloodSoundsPlay()
    {
        audioSource.PlayOneShot(breathClip);
    }
}

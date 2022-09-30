using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("Magazine")]
    [SerializeField]
    private int totalBulletCount = 6;
    private int bulletCount = 6;
    [SerializeField]
    private List<Image> bullets;

    [Header("Shoot")]
    [SerializeField]
    private float shootTime = 0.5f;
    private float nextShootTime = 0.5f;
    [SerializeField]
    private DynamicCrosshair dynamicCrosshair;

    [Header("References")]
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private ParticleSystem muzzleFlash;

    [SerializeField]
    private ParticleSystem hitImpactPrefab;

    [SerializeField]
    private Animator weaponAnimator;

    [SerializeField]
    private AudioSource weaponAudioSource;
    [SerializeField]
    private AudioClip weaponShootClip;
    [SerializeField]
    private AudioClip weaponMagazineClip;

    private bool isReloading;
    private void Awake()
    {
        camera = Camera.main;
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        weaponAnimator = GetComponentInChildren<Animator>();
        weaponAudioSource = GetComponentInChildren<AudioSource>();
        bulletCount = totalBulletCount;
    }

    void Update()
    {
        if (bulletCount > 0 && !isReloading)
        {
            Shoot();
        }

        if (!isReloading && (Input.GetKeyDown(KeyCode.R) || bulletCount <= 0))
        {
            // Debug.Log("Reload Update");
            isReloading = true;
            StartCoroutine(ReloadRoutine());
        }
    }

    private void Shoot()
    {
        RaycastHit raycastHit;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out raycastHit, 100))
        {
            if (raycastHit.collider.gameObject)
            {
                // Debug.Log(raycastHit.collider.tag);
                if (raycastHit.collider.CompareTag("Enemy"))
                {
                    if (Time.time > nextShootTime)
                    {
                        nextShootTime = Time.time + shootTime;

                        weaponAnimator.CrossFade("Shoot", 0.15f);

                        dynamicCrosshair.RecoilCrosshair();

                        muzzleFlash.Play();

                        weaponAudioSource.clip = weaponShootClip;
                        weaponAudioSource.pitch = Random.Range(0.95f, 1.05f);
                        weaponAudioSource.Play();

                        Destroy(Instantiate(hitImpactPrefab, raycastHit.point,
                            Quaternion.LookRotation(raycastHit.normal)), 5);

                        DecreaseBulletCount();
                    }
                }
                // Debug.DrawRay(muzzle.transform.position, raycastHit.point, Color.red, 1);
            }
        }
    }

    private void DecreaseBulletCount()
    {
        bulletCount--;
        bullets[bulletCount].gameObject.SetActive(false);
    }

    private void IncreaseBulletCount()
    {
        bullets[bulletCount].gameObject.SetActive(true);
        bulletCount++;
    }

    private IEnumerator ReloadRoutine()
    {
        // Debug.Log("Reload Routine");
        isReloading = true;
        yield return new WaitForSeconds(0.85f);

        weaponAnimator.CrossFade("Reload", 0.15f);
        yield return new WaitForSeconds(0.15f);

        while (bulletCount < totalBulletCount)
        {
            IncreaseBulletCount();
            weaponAudioSource.clip = weaponMagazineClip;
            weaponAudioSource.pitch = Random.Range(1.15f, 1.25f);
            weaponAudioSource.Play();
            yield return new WaitForSeconds(0.2f);
        }

        isReloading = false;
    }
}

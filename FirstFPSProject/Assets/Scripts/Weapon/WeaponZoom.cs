using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [Header("Zoom")]
    [SerializeField]
    private KeyCode zoomKey = KeyCode.Mouse1;
    [SerializeField]
    private float timeToZoom = 0.3f;

    [SerializeField]
    private float zoomFOV = 30;
    private float defaultFOV = 60;

    [SerializeField]
    private Vector3 zoomPosition = new Vector3(0, -0.16f, -0.25f);
    private Vector3 defaultPosition = new Vector3(0.25f, -0.25f, 0.5f);

    private Coroutine zoomRoutine;

    [Header("References")]
    [SerializeField]
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        defaultFOV = camera.fieldOfView;
        defaultPosition = transform.localPosition;
    }

    private void Update()
    {
        HandleZoom();
    }

    private void HandleZoom()
    {
        if (Input.GetKeyDown(zoomKey))
        {
            if (zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }

            zoomRoutine = StartCoroutine(ToggleZoom(true));
        }

        if (Input.GetKeyUp(zoomKey))
        {
            if (zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }

            zoomRoutine = StartCoroutine(ToggleZoom(false));
        }
    }

    private IEnumerator ToggleZoom(bool isEnter)
    {
        float targetFOV = isEnter ? zoomFOV : defaultFOV;
        Vector3 targetPosition = isEnter ? zoomPosition : defaultPosition;

        float startingFOV = camera.fieldOfView;
        Vector3 startingPosition = transform.localPosition;

        float timeElapsed = 0;

        while (timeElapsed < timeToZoom)
        {
            camera.fieldOfView = Mathf.Lerp(startingFOV, targetFOV, timeElapsed / timeToZoom);
            transform.localPosition = Vector3.Lerp(startingPosition, targetPosition, timeElapsed / timeToZoom);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        camera.fieldOfView = targetFOV;
        transform.localPosition = targetPosition;
        DynamicCrosshair.instance.Zoom(isEnter);
        zoomRoutine = null;
    }
}

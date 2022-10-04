using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicCrosshair : MonoBehaviour
{
    //[SerializeField]
    //private Image crosshair; 

    [SerializeField]
    private RectTransform crosshair;

    [SerializeField]
    private float smoothness = 2;
    private float crosshairMultiplier;

    private Vector2 currentSize = new Vector2(100, 100);

    private void Awake()
    {
        currentSize = crosshair.sizeDelta;
    }

    public void Update()
    {
        crosshairMultiplier = Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical")) +
            Mathf.Abs(Input.GetAxis("Mouse X")) + Mathf.Abs(Input.GetAxis("Mouse Y"));

        crosshairMultiplier = Mathf.Clamp(crosshairMultiplier, 0, 1);

        if (crosshairMultiplier > 0.1f)
        {
            crosshair.sizeDelta = Vector3.Slerp(crosshair.sizeDelta, currentSize * 2, Time.deltaTime * smoothness);
        }
        else
        {
            crosshair.sizeDelta = Vector3.Slerp(crosshair.sizeDelta, currentSize, Time.deltaTime * smoothness);
        }
    }

    public void RecoilCrosshair()
    {
        crosshair.sizeDelta = currentSize * 2;
    }
}

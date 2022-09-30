using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("SWAY")]
    [SerializeField]
    private float smoothness = 8;
    [SerializeField]
    private float swayMultiplier = 2;

    private float mouseX;
    private float mouseY;

    private Quaternion rotationX;
    private Quaternion rotationY;
    private Quaternion targetRotation;


    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * swayMultiplier;
        mouseY = Input.GetAxis("Mouse Y") * swayMultiplier;

        rotationX = Quaternion.AngleAxis(mouseY, Vector3.right);
        rotationY = Quaternion.AngleAxis(-mouseX, Vector3.up);

        targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation,
            targetRotation, smoothness * Time.deltaTime);
    }
}

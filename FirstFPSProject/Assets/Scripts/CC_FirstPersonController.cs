using UnityEngine;
using System.Collections;

public class CC_FirstPersonController : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
    private bool ShouldJump => characterController.isGrounded && Input.GetKeyDown(jumpKey);
    private bool ShouldCrouch => characterController.isGrounded && !duringCrouchAnimation &&
    Input.GetKeyDown(crouchKey);

    [Header("Functional Options")]
    [SerializeField]
    private bool canSprint = true;
    [SerializeField]
    private bool canJump = true;
    [SerializeField]
    private bool canCrouch = true;
    [SerializeField]
    private bool canUseHeadbob = true;
    [SerializeField]
    private bool willSlideOnSlopes = true;

    [Header("Control Inputs")]
    [SerializeField]
    private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField]
    private KeyCode jumpKey = KeyCode.Space;
    [SerializeField]
    private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Movement Paremeters")]
    [SerializeField]
    private float walkSpeed = 3;
    [SerializeField]
    private float sprintSpeed = 7;
    [SerializeField]
    private float crouchSpeed = 1.5f;
    [SerializeField]
    private float slopeSpeed = 8;

    [Header("Look Paremeters")]
    [SerializeField, Range(1, 10)]
    private float lookSpeedX = 2;
    [SerializeField, Range(1, 10)]
    private float lookSpeedY = 2;
    [SerializeField, Range(1, 180)]
    private float upperLookLimit = 80;
    [SerializeField, Range(1, 180)]
    private float lowerLookLimit = 80;

    [Header("Jump Paremeters")]
    [SerializeField]
    private float jumpForce = 8;
    [SerializeField]
    private float gravity = 30;
    [SerializeField]
    private DynamicCrosshair dynamicCrosshair;

    [Header("Crouch Paremeters")]
    [SerializeField]
    private float crouchingHeight = 0.5f;
    [SerializeField]
    private float standingHeight = 2;
    [SerializeField]
    private float timeToCrouch = 0.25f;
    [SerializeField]
    private Vector3 crouchingCenter = new Vector3(0, 0.75f, 0);
    [SerializeField]
    private Vector3 standingCenter = new Vector3(0, 0.25f, 0);
    private bool isCoruching;
    private bool duringCrouchAnimation;

    [Header("Headbob Paremeters")]
    [SerializeField]
    private float walkBobSpeed = 14;
    [SerializeField]
    private float walkBobAmount = 0.05f;
    [SerializeField]
    private float sprintBobSpeed = 18;
    [SerializeField]
    private float sprintBobAmount = 0.1f;
    [SerializeField]
    private float crouchBobSpeed = 8;
    [SerializeField]
    private float crouchBobAmount = 0.025f;
    private float defaultYPosition = 0;
    private float timer;

    // SLIDING PARAMETERS
    private Vector3 hitPointNormal;

    private bool IsSliding
    {
        get
        {
            if(characterController.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 2))
            {
                hitPointNormal = slopeHit.normal;
                return Vector3.Angle(hitPointNormal, Vector3.up) > characterController.slopeLimit;
            }
            else
            {
                return false;
            }
        }
    }

    private Camera playerCamera;
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotationX;
    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        defaultYPosition = playerCamera.transform.localPosition.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();

            if (canJump)
            {
                HandleJump();
            }

            if (canJump)
            {
                HandleCrouch();
            }

            if (canUseHeadbob)
            {
                HandleHeadbob();
            }

            ApplyFinalMovements();
        }
    }

    private void HandleMovementInput()
    {
        currentInput = new Vector2((isCoruching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"),
        (isCoruching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));

        float moveDirectionY = moveDirection.y;

        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) +
        (transform.TransformDirection(Vector3.right) * currentInput.y);

        moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void HandleJump()
    {
        if (ShouldJump)
        {
            moveDirection.y = jumpForce;
            dynamicCrosshair.RecoilCrosshair();
        }
    }


    private void HandleCrouch()
    {
        if (ShouldCrouch)
        {
            StartCoroutine(CrouchStand());
        }
    }

    private void HandleHeadbob()
    {
        if (!characterController.isGrounded)
        {
            return;
        }

        if(Mathf.Abs(moveDirection.x)>0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (isCoruching ? crouchBobSpeed : IsSprinting ? sprintBobSpeed : walkBobSpeed);
            
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultYPosition + Mathf.Sin(timer) * (isCoruching ? crouchBobAmount : IsSprinting ? sprintBobAmount : walkBobAmount),
                playerCamera.transform.localPosition.z
            );
        }
    }

    private void ApplyFinalMovements()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if(willSlideOnSlopes && IsSliding)
        {
            moveDirection += new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * slopeSpeed;

        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private IEnumerator CrouchStand()
    {
        if (isCoruching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1))
        {
            yield break;
        }

        duringCrouchAnimation = true;

        float timeElapsed = 0;

        float targetHeight = isCoruching ? standingHeight : crouchingHeight;
        float currentHeight = characterController.height;

        Vector3 targetCenter = isCoruching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        isCoruching = !isCoruching;

        duringCrouchAnimation = false;
    }
}
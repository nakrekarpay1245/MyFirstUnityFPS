using System;
using UnityEngine;
using System.Collections;

public class CC_FirstPersonController : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
    private bool ShouldJump => characterController.isGrounded && Input.GetKeyDown(jumpKey);
    private bool ShouldCrouch => characterController.isGrounded && !duringCrouchAnimation &&
    Input.GetKeyDown(crouchKey);

    [Header("ERKAN YAPRAK CHARACTER CONTROLLER - V - 0.1")]

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
    [SerializeField]
    private bool canZoom = true;
    [SerializeField]
    private bool canInteract = true;
    [SerializeField]
    private bool useFootsteps = true;

    [Header("Control Inputs")]
    [SerializeField]
    private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField]
    private KeyCode jumpKey = KeyCode.Space;
    [SerializeField]
    private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField]
    private KeyCode zoomKey = KeyCode.Mouse1;
    [SerializeField]
    private KeyCode interactKey = KeyCode.E;

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

    [Header("Zoom Paremeters")]
    [SerializeField]
    private float timeToZoom = 0.3f;
    [SerializeField]
    private float zoomFOV = 30;
    private float defaultFOV = 60;
    private Coroutine zoomRoutine;

    [Header("Footstep Paremeters")]
    [SerializeField]
    private float baseStepSpeed = 0.5f;
    [SerializeField]
    private float crouchStepMultiplier = 1.5f;
    [SerializeField]
    private float sprintStepMultiplier = 0.6f;
    [SerializeField]
    private AudioSource footStepAudioSource;
    [SerializeField]
    private AudioClip[] woodClips;
    [SerializeField]
    private AudioClip[] sandClips;
    [SerializeField]
    private AudioClip[] grassClips;
    private float footStepTimer;
    private float GetCurrentOffset => isCoruching ? baseStepSpeed * crouchStepMultiplier : IsSprinting ? baseStepSpeed * sprintStepMultiplier : baseStepSpeed;

    // SLIDING PARAMETERS
    private Vector3 hitPointNormal;

    private bool IsSliding
    {
        get
        {
            if (characterController.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 2))
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

    [Header("Interaction Paremeters")]
    [SerializeField]
    private Vector3 interactionRayPoint = new Vector3(0.5f, 0.5f, 0);
    [SerializeField]
    private float interactionDistance = 3;
    [SerializeField]
    private LayerMask interactionLayer = 9;
    private Interactable currentInteractable;

    [SerializeField]
    private Camera playerCamera;
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotationX;

    public static CC_FirstPersonController controller;

    private void OnEnable()
    {
        if (!controller)
        {
            controller = this;
        }
    }

    //private void OnDisable()
    //{
    //}

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        defaultYPosition = playerCamera.transform.localPosition.y;
        defaultFOV = playerCamera.fieldOfView;
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

            if (canZoom)
            {
                HandleZoom();
            }

            if (useFootsteps)
            {
                HandleFootStep();
            }

            if (canInteract)
            {
                HandleInteractionCheck();
                HandleInteractionInput();
            }

            ApplyFinalMovements();
        }
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

    private void HandleInteractionCheck()
    {
        if (Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance))
        {
            if (hit.collider.gameObject.layer == 9 && !currentInteractable)
            {
                hit.collider.TryGetComponent(out currentInteractable);

                if (currentInteractable)
                {
                    currentInteractable.OnFocus();
                }
            }
        }
        else if (currentInteractable)
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }

    private void HandleInteractionInput()
    {
        if (Input.GetKeyDown(interactKey) && currentInteractable && Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance, interactionLayer))
        {
            currentInteractable.OnInteract();
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

        if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (isCoruching ? crouchBobSpeed : IsSprinting ? sprintBobSpeed : walkBobSpeed);

            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultYPosition + Mathf.Sin(timer) * (isCoruching ? crouchBobAmount : IsSprinting ? sprintBobAmount : walkBobAmount),
                playerCamera.transform.localPosition.z
            );
        }
    }

    private void HandleFootStep()
    {
        if (!characterController.isGrounded)
        {
            return;
        }
        if (currentInput == Vector2.zero)
        {
            return;
        }

        footStepTimer -= Time.deltaTime;

        if (footStepTimer <= 0)
        {
            if (Physics.Raycast(playerCamera.transform.position, Vector3.down, out RaycastHit hit, 5))
            {
                switch (hit.collider.tag)
                {
                    case "Footsteps/Wood":
                        footStepAudioSource.PlayOneShot(woodClips[UnityEngine.Random.Range(0, woodClips.Length)]);
                        break;
                    case "Footsteps/Sand":
                        footStepAudioSource.PlayOneShot(sandClips[UnityEngine.Random.Range(0, sandClips.Length)]);
                        break;
                    case "Footsteps/Grass":
                        footStepAudioSource.PlayOneShot(grassClips[UnityEngine.Random.Range(0, grassClips.Length)]);
                        break;
                    default:
                        footStepAudioSource.PlayOneShot(sandClips[UnityEngine.Random.Range(0, sandClips.Length)]);
                        break;
                }
            }

            footStepTimer = GetCurrentOffset;
        }
    }

    private void ApplyFinalMovements()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (willSlideOnSlopes && IsSliding)
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

    private IEnumerator ToggleZoom(bool isEnter)
    {
        float targetFOV = isEnter ? zoomFOV : defaultFOV;
        float startingFOV = playerCamera.fieldOfView;
        float timeElapsed = 0;

        while (timeElapsed < timeToZoom)
        {
            playerCamera.fieldOfView = Mathf.Lerp(startingFOV, targetFOV, timeElapsed / timeToZoom);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        playerCamera.fieldOfView = targetFOV;
        zoomRoutine = null;
    }   
}
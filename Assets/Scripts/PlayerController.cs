using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))] // Automatically ensures a Rigidbody is on the object
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Camera")]
    [SerializeField] private float mouseSensitivity = 180f;
    [SerializeField] private float followDistance = 6f;
    [SerializeField] private float followHeight = 2f;
    [SerializeField] private float minPitch = -35f;
    [SerializeField] private float maxPitch = 75f;

    private Camera mainCamera;
    private Rigidbody rb;
    private PlayerAnimationController animationController;
    private float yaw;
    private float pitch;

    // We store the calculated movement velocity here to use in FixedUpdate
    private Vector3 movementTarget;
    private bool shouldRotate;
    private Vector3 lookDirection;

    private bool inputActive = true;

    private void SetInputActive(bool inputActive)
    {
        this.inputActive = inputActive;
        Cursor.lockState = inputActive ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !inputActive; 

        if (!inputActive)
        {
            movementTarget = Vector3.zero;
            shouldRotate = false;
            lookDirection = Vector3.zero;
            animationController?.StopAndReturnToIdle();
        }
    }

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        animationController = GetComponent<PlayerAnimationController>();
        if (animationController == null)
        {
            animationController = PlayerAnimationController.Instance;
        }

        // Set up Rigidbody settings so physics behaves correctly for a player character
        rb.interpolation = RigidbodyInterpolation.Interpolate; // Prevents camera/movement jitter
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Helps stop high-speed clipping

        // Freeze X and Z rotations so the player doesn't tip over when hitting walls
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        if (mainCamera != null)
        {
            mainCamera.orthographic = false;
        }

        Vector3 initialEuler = mainCamera != null ? mainCamera.transform.eulerAngles : transform.eulerAngles;
        yaw = initialEuler.y;
        pitch = initialEuler.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (mainCamera == null || Keyboard.current == null || Mouse.current == null)
        {
            return;
        }

        if (!inputActive)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                SetInputActive(true);
            }

            return;
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SetInputActive(false);
            return;
        }

        // --- 1. MOUSE LOOK & CAMERA ROTATION ---
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        yaw += mouseDelta.x * mouseSensitivity * Time.deltaTime;
        pitch -= mouseDelta.y * mouseSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion cameraRotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 cameraForward = cameraRotation * Vector3.forward;
        Vector3 cameraRight = cameraRotation * Vector3.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // --- 2. GATHER KEYBOARD INPUT ---
        Vector3 moveDirection = Vector3.zero;
        if (Keyboard.current.wKey.isPressed) moveDirection += cameraForward;
        if (Keyboard.current.sKey.isPressed) moveDirection -= cameraForward;
        if (Keyboard.current.aKey.isPressed) moveDirection -= cameraRight;
        if (Keyboard.current.dKey.isPressed) moveDirection += cameraRight;

        if (moveDirection.sqrMagnitude > 0f)
        {
            moveDirection.Normalize();
            // Calculate how much we want to move this frame
            movementTarget = moveDirection * moveSpeed;
            lookDirection = moveDirection;
            shouldRotate = true;
            animationController?.runWalking();
        }
        else
        {
            movementTarget = Vector3.zero;
            shouldRotate = false;
            animationController?.StopAndReturnToIdle();
        }

    }

    void LateUpdate()
    {
        if (mainCamera == null) return;

        // Camera position updates ONLY after physics has completely settled the player
        Quaternion cameraRotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 cameraOffset = cameraRotation * new Vector3(0f, followHeight, -followDistance);

        mainCamera.transform.position = transform.position + cameraOffset;
        mainCamera.transform.rotation = cameraRotation;
    }

    // All physics-based movement changes MUST happen inside FixedUpdate
    void FixedUpdate()
    {
        // 1. Move the player using Rigidbody. MovePosition accurately tests for collisions along the path.
        Vector3 nextPosition = rb.position + movementTarget * Time.fixedDeltaTime;
        rb.MovePosition(nextPosition);

        // 2. Rotate the player to face movement direction smoothly via physics
        if (shouldRotate && lookDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            rb.MoveRotation(targetRotation);
        }
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;

    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float gravity = 20;

    [SerializeField] private float lookSensitivity = 0.2f;
    [SerializeField] private float lookAngleLimit = 90f;

    private Camera mainCamera;
    private CharacterController characterController;

    private InputAction moveInput;

    private InputAction jumpInput;
    private bool jumped = false;

    private Vector3 moveDirection = Vector3.zero;
    private float lookAngle = 0f;

    void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();

        moveInput = InputSystem.actions.FindAction("Move");

        jumpInput = InputSystem.actions.FindAction("Jump");
        jumpInput.started += Jump;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 moveVector = moveInput.ReadValue<Vector2>();
        Vector2 mouseDelta = new Vector2(Mouse.current.delta.x.ReadValue(), Mouse.current.delta.y.ReadValue());

        if (!characterController.isGrounded)
            jumped = false;

        Movement(moveVector);
        Look(mouseDelta);
    }

    void Movement(Vector2 moveVector)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float oldY = moveDirection.y;

        Vector2 speed = new Vector2(moveVector.y * movementSpeed, moveVector.x * movementSpeed);

        moveDirection = (forward * speed.x) + (right * speed.y);

        if (jumped && characterController.isGrounded)
        {
            moveDirection.y = jumpForce;
        }
        else
        {
            moveDirection.y = oldY;
        }

        if (!characterController.isGrounded)
             moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void Jump(InputAction.CallbackContext _)
    {
        jumped = true;
    }

    void Look(Vector2 mouseDelta)
    {
        lookAngle += -mouseDelta.y * lookSensitivity;
        lookAngle = Mathf.Clamp(lookAngle, -lookAngleLimit, lookAngleLimit);

        mainCamera.transform.localRotation = Quaternion.Euler(lookAngle, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseDelta.x * lookSensitivity, 0);
    }
}

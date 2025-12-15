// Created by MrRandomYT on 2025-12-10
// Script: PlayerMovement.cs

using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] [Tooltip("Transform of the Player Camera Object.")] private Transform cameraTransform;
    [SerializeField] [Tooltip("Reference to the InputManager for the player.")] private InputManager inputManager;

    [Header("Movement Parameters")]
    [SerializeField] [Tooltip("Default Movement Speed.")] private float moveSpeed = 5f;
    [SerializeField] [Tooltip("Movement speed when sprinting.")] private float sprintSpeed = 10f;
    [SerializeField] [Tooltip("In Script Absolute value is Used.")] private float jumpHeight = 1f;
    [SerializeField] [Tooltip("In Script Absolute value is Used.")] private float gravity = 10f;
    
    public bool canWallJump = false;

    [Header("Sliding Parameters")]
    [SerializeField] private float slideSpeed = 5f;
    [SerializeField] private float maxSlideDuration = 1f;

    [Header("Settings Parameters")]
    [SerializeField] private float sensitivity = 1f;

    private CharacterController characterController;
    private Vector3 movement;
    private Vector3 hitNormal;
    private bool isSliding = false;
    private bool alreadySliding = false;
    private Vector3 downforce = Vector3.zero;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        inputManager = inputManager ?? ScriptableObject.CreateInstance<InputManager>();

        if (cameraTransform) return;
        if (Camera.main) cameraTransform = Camera.main.transform;
        else Debug.LogWarning("Missing Camera Transform!");
    }

    private void Update()
    {
        HandleRotation();
        HandleMovement();
        HandleJump();
        HandleGravity();
        HandleSliding();
        ApplyMovement();
    }

    private void HandleRotation()
    {
        transform.Rotate(0f, inputManager.CameraInputX * sensitivity / 10, 0f);
    }

    private void HandleMovement()
    {
        Vector3 move = (transform.forward * inputManager.MovementInputY + transform.right * inputManager.MovementInputX).normalized;
        move *= inputManager.SprintInput ? sprintSpeed : moveSpeed;

        movement.x = move.x;
        movement.z = move.z;
    }

    private void HandleJump()
    {
        if (characterController.isGrounded && inputManager.JumpInput && !isSliding)
        {
            movement.y = Mathf.Sqrt(Mathf.Abs(jumpHeight) * 2f * Mathf.Abs(gravity));
        }
    }

    private void HandleGravity()
    {
        if (characterController.isGrounded && movement.y < -0.1f && !isSliding)
            movement.y = -0.1f;

        movement.y -= Mathf.Abs(gravity) * Time.deltaTime;
    }

    private void HandleSliding()
    {
        if (!canWallJump)
        {
            if (isSliding)
            {
                Vector3 slideDirection = Vector3.ProjectOnPlane(Vector3.down, hitNormal).normalized;
                float slideDuration = slideSpeed * Time.deltaTime;
                if (slideDuration > maxSlideDuration) slideDuration = maxSlideDuration;

                movement += slideDirection * slideDuration;
                alreadySliding = true;
            }
            else if (characterController.isGrounded && alreadySliding)
            {
                StopSliding();
            }
        }
        else isSliding = false;
    }

    private void StopSliding()
    {
        movement.x *= Mathf.Pow(1f - slideSpeed * Time.deltaTime, 3f);
        movement.z *= Mathf.Pow(1f - slideSpeed * Time.deltaTime, 3f);

        if (Mathf.Abs(movement.x) < 0.01f && Mathf.Abs(movement.z) < 0.01f)
        {
            alreadySliding = false;
            movement.x = 0f;
            movement.z = 0f;
        }
    }

    private void ApplyMovement()
    {
        characterController.Move(movement * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
        float angle = Vector3.Angle(hit.normal, Vector3.up);
    
        if(!canWallJump)
            isSliding = angle > characterController.slopeLimit + 10f; // Slide on steep slopes
    }
}

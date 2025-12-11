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
    
    [Header("Settings Parameters")]
    [SerializeField] private float sensitivity = 1f;

    private CharacterController characterController;
    private Vector3 movement;

    private Transform MoveDirection => (cameraTransform ? cameraTransform : transform);

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
        HandleGravity();
        HandleRotation();
        HandleMovement();
        HandleJump();
        ApplyMovement();
    }
    
    private float yaw;

    private void HandleRotation()
    {
        transform.Rotate(0f, inputManager.CameraInputX * sensitivity/10, 0f);
    }

    
    private void HandleMovement()
    {
        Vector3 move = (
            MoveDirection.forward * inputManager.MovementInputY
            + MoveDirection.right * inputManager.MovementInputX).normalized;

        move *= inputManager.SprintInput ? sprintSpeed : moveSpeed;

        movement.x = move.x;
        movement.z = move.z;
    }

    private void HandleJump()
    {
        if (characterController.isGrounded && inputManager.JumpInput)
        {
            movement.y = Mathf.Sqrt(Mathf.Abs(jumpHeight) * 2f * Mathf.Abs(gravity));
        }
    }

    private void HandleGravity()
    {
        if (characterController.isGrounded && movement.y < -0.1f)
            movement.y = -0.1f;
        
        movement.y -= Mathf.Abs(gravity) * Time.deltaTime;
    }

    private void ApplyMovement()
    {
        characterController.Move(movement * Time.deltaTime);
    }
}

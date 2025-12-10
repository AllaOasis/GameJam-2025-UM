// Created by MrRandomYT on 2025-12-10
// Script: InputManager.cs

using UnityEngine;
[CreateAssetMenu(menuName = "Input/InputManager")]
public class InputManager : ScriptableObject
{
    #region Methods
    void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.Movement.Movement.performed += i => movement = i.ReadValue<Vector2>();
            playerControls.Movement.Camera.performed += i => camera = i.ReadValue<Vector2>();
            
            playerControls.Movement.Sprint.performed += i => sprint = i.ReadValue<float>();
            playerControls.Movement.Sprint.canceled += i => sprint = i.ReadValue<float>();
            
            playerControls.Movement.Jump.performed += i => jump = i.ReadValue<float>();
            playerControls.Movement.Jump.canceled += i => jump = i.ReadValue<float>();
            
            playerControls.Actions.ItemPickUp.performed += i => pickUp = i.ReadValue<float>();
            playerControls.Actions.ItemPickUp.canceled += i => pickUp = i.ReadValue<float>();
        }
        
        playerControls.Enable();
    }
    void OnDisable()
    {
        playerControls.Disable();
    }
    
    #endregion
    #region Local Variables
    
    private PlayerControls playerControls;
    private Vector2 movement;
    private Vector2 camera;
    private float sprint;
    private float jump;
    private float pickUp;
    
    #endregion
    #region Public Properties
    
    public Vector2 Movement => movement;
    public float MovementInputY => movement.y;
    public float MovementInputX => movement.x;
    
    public Vector2 Camera => camera;
    public float CameraInputY => camera.y;
    public float CameraInputX => camera.x;
    
    public bool SprintInput => sprint > 0;
    
    public bool JumpInput => jump > 0;
    
    public bool PickUpInput => pickUp > 0;

    public bool OpenCloseInventory => playerControls.Actions.OpenCloseInventory.triggered;

    #endregion
}

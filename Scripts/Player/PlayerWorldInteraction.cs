using UnityEngine;

public class PlayerWorldInteraction : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] [Tooltip("Transform of the Player Camera Object.")] private Transform cameraTransform;
    [SerializeField] [Tooltip("Reference to the InputManager for the player.")] private InputManager inputManager;
    [SerializeField] [Tooltip("The press E popup.")] private Transform pressE;

    [Header("Interaction Parameters")]
    [SerializeField] [Tooltip("Interaction Range")] [Min(0)] private float range = 5f;
    [SerializeField] [Tooltip("Interactable Layers")] private LayerMask interactableLayerMask;

    
    private void Start()
    {
        inputManager = inputManager ?? ScriptableObject.CreateInstance<InputManager>();
        
        if (cameraTransform) return;
        if (Camera.main) cameraTransform = Camera.main.transform;
        else Debug.LogWarning("Missing Camera Transform!");
    }
    
    private void Update() 
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        
        if (Physics.Raycast(ray, out RaycastHit hit, range, interactableLayerMask))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                if(pressE) pressE.gameObject.SetActive(true);

                if (inputManager.InteractClicked && hit.collider.TryGetComponent(out Interactable interactable))
                {
                    interactable.Interact();
                }
            }
            else
            {
                if(pressE) pressE.gameObject.SetActive(false);
            }
        }
        else
        {
            if(pressE) pressE.gameObject.SetActive(false);
        }
    }

}

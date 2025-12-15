using Unity.VisualScripting;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] [Tooltip("Reference to the InputManager for the player.")] private InputManager inputManager;
    [SerializeField] private GameObject menu;
    void Update()
    {
        if (inputManager && inputManager.OpenCloseMenu) ToggleMenu();
    }

    public void ToggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }
}

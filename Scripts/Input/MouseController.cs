using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] List<Transform> controllableUI;
    
    private void Update()
    {
        bool lockMouse = true;
        foreach (Transform child in controllableUI)
        {
            if (child.gameObject.activeInHierarchy) lockMouse = false;
        }
        Cursor.lockState = lockMouse ? CursorLockMode.Locked : CursorLockMode.None;
    }
}

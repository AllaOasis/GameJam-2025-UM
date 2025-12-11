using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        return false;
    }
}

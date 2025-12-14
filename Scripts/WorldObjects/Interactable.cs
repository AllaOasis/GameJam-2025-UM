using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual bool Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        return false;
    }
}

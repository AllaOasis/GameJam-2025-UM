using System;
using UnityEngine;

public class InteractableGift : Interactable
{
    [SerializeField] private Vector3 finalPosition;
    [SerializeField] private PlayerChallange playerChallange;

    private void Start()
    {
        if (!playerChallange)
        {
            Debug.LogWarning("PlayerChallange not assigned to " + gameObject.name);
        }
    }

    public override bool Interact()
    {
        playerChallange?.GiftCollected();
        Debug.Log("Gift collected");
        transform.position = finalPosition;
        tag = "Untagged";
        Destroy(this);
        return true;
    }
}

using System;
using UnityEngine;

public class InteractableGift : Interactable
{
    [SerializeField] private Vector3 finalPosition;
    [SerializeField] private PlayerChallange playerChallange;
    [SerializeField] private GameObject pickupParticles;

    private void Start()
    {
        if (!playerChallange)
        {
            Debug.LogWarning("PlayerChallange not assigned to " + gameObject.name);
        }
    }

    public override bool Interact()
    {
        if(pickupParticles) Instantiate(pickupParticles, transform.position, Quaternion.identity);
        playerChallange?.GiftCollected();
        Debug.Log("Gift collected");
        transform.position = finalPosition;
        if(pickupParticles) Instantiate(pickupParticles, transform);
        tag = "Untagged";
        Destroy(this);
        return true;
    }
}

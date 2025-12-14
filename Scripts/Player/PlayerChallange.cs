using System;
using UnityEngine;

public class PlayerChallange : MonoBehaviour
{
    [SerializeField] private int giftAmount;
    [SerializeField] private int giftsRequired;
        
    public void GiftCollected()
    {
        giftAmount++;
        if (giftAmount >= giftsRequired) Debug.Log("All the gifts have been collected!");
    }
}
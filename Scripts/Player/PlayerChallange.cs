using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ToggleReward
{
    public Transform Transform;
    public bool SetActive;
}

public class PlayerChallange : MonoBehaviour
{
    [SerializeField] private int giftAmount;
    [SerializeField] private int giftsRequired;
    [SerializeField] private float winFog = 0.01f;
    [SerializeField] List<ToggleReward> rewards;

    private void Start()
    {
        foreach (ToggleReward reward in rewards)
        {
            reward.Transform.gameObject.SetActive(!reward.SetActive);
        }
        if (TryGetComponent<PlayerHazards>(out PlayerHazards playerHazards))
        {
            playerHazards.enabled = true;
        }

        if (TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            playerMovement.canWallJump = true;
        }
    }

    public void GiftCollected()
    {
        giftAmount++;
        if (giftAmount >= giftsRequired)
        {
            Debug.Log("All the gifts have been collected!");
            foreach (ToggleReward reward in rewards)
            {
                reward.Transform.gameObject.SetActive(reward.SetActive);
            }
            if (TryGetComponent<PlayerHazards>(out PlayerHazards playerHazards))
            {
                playerHazards.enabled = false;
            }
            
        }
    }

    private void Update()
    {
        if (giftAmount >= giftsRequired)
            RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, winFog, Time.deltaTime * 2.5f);
    }
}
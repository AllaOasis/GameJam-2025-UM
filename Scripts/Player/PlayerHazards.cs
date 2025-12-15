using System;
using QFSW.QC;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHazards : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] [Tooltip("Reference to the InputManager for the player.")] private InputManager inputManager;
    [SerializeField] private Rigidbody cameraRigidBody;
    [SerializeField] private Transform deathScreen;
    [SerializeField] private Transform menuScreen;
    [SerializeField] private Transform sliderParent;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider coldSlider;
    [SerializeField] private Slider heatSlider;
    
    [Header("Player Stats")]
    [SerializeField] private float health = 100f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float cold = 0;
    [SerializeField] private float maxCold = 25;
    [SerializeField] private float heat = 0;
    [SerializeField] private float maxHeat = 25;

    [Header("Hazard Settings")]
    [SerializeField] [Tooltip("Amount of Cold hazard per second.")] private float snowHazardRate = 5f;
    [SerializeField] [Tooltip("Amount of Cold hazard per second.")] private float waterHazardRate = 10f;
    [SerializeField] [Tooltip("Amount of Heat hazard per second.")] private float fireHazardRate = 5f;
    
    [SerializeField] [Tooltip("Amount of Cold hazard removed per second.")] private float coldHealRate = 5f;
    [SerializeField] [Tooltip("Amount of Heat hazard removed per second.")] private float heatHealRate = 5f;
    [SerializeField] [Tooltip("Amount of Health healed per second.")] private float healthHealRate = 5f;
    
    [SerializeField] [Tooltip("Threshold where hazards start to damage the player.")] private float hazardDamageThreshold = 15f;
    [SerializeField] [Tooltip("Damage dealt to the Player when hazards reach certain level.")] private float hazardDamageRate = 10f;

    private bool inSnow;
    private bool inWater;
    private bool inFire;
    private bool nearFire;

    private void Start()
    {
        inputManager.Enable();
        if (cameraRigidBody) cameraRigidBody.useGravity = false;
        if (deathScreen) deathScreen.gameObject.SetActive(false);
        if (sliderParent) sliderParent.gameObject.SetActive(true);
        if (menuScreen) menuScreen.gameObject.SetActive(false);
    }

    [Command]
    private void KillPlayer()
    {
        health = 0;
    }

    private void Update()
    {
        if (health <= 0)
        {
            inputManager.Enable(false);
            inputManager.DisableUI();
            if (cameraRigidBody) cameraRigidBody.useGravity = true;
            if (deathScreen) deathScreen.gameObject.SetActive(true);
            if (sliderParent) sliderParent.gameObject.SetActive(false);
            if (menuScreen) menuScreen.gameObject.SetActive(false);
            return;
        } 
        
        if (inWater)
        {
            if (cold < maxCold) cold += waterHazardRate * Time.deltaTime;
            if (cold > maxCold) cold = maxCold;
            if (heat > 0) heat -= heatHealRate  * Time.deltaTime;
            if (heat < 0) heat = 0;
        }

        if (inSnow)
        {
            if (cold < maxCold) cold += snowHazardRate * Time.deltaTime;
            if (cold > maxCold) cold = maxCold;
            if (heat > 0) heat -= heatHealRate  * Time.deltaTime;
            if (heat < 0) heat = 0;
        }
        
        if (inFire)
        {
            if (heat < maxHeat) heat += fireHazardRate * Time.deltaTime;
            if (heat > maxHeat) heat = maxHeat;
            if (cold > 0) cold -= coldHealRate  * Time.deltaTime;
            if (cold < 0) cold = 0;
        }
        
        if (nearFire && !(inWater || inSnow || inFire))
        {
            if (cold > 0) cold -= coldHealRate  * Time.deltaTime;
            if (cold < 0) cold = 0;
            
            if (cold < hazardDamageThreshold && heat < hazardDamageThreshold) health += healthHealRate  * Time.deltaTime;
            if (health > maxHealth) health = maxHealth;
        }
        else if (cold > hazardDamageThreshold)
        {
            health -= hazardDamageRate * Time.deltaTime;
            if (health < 0) health = 0;
        }
        if (heat > hazardDamageThreshold)
        {
            health -= hazardDamageRate * Time.deltaTime;
            if (health < 0) health = 0;
        }
        
        if (healthSlider)
        {
            if (!Mathf.Approximately(healthSlider.maxValue, maxHealth)) healthSlider.maxValue = maxHealth; 
            healthSlider.value = health;
        }
        if (coldSlider)
        {
            if (!Mathf.Approximately(coldSlider.maxValue, maxCold)) coldSlider.maxValue = maxCold; 
            coldSlider.value = cold;
        }
        if (heatSlider)
        {
            if (!Mathf.Approximately(heatSlider.maxValue, maxHeat)) heatSlider.maxValue = maxHeat; 
            heatSlider.value = heat;
        }
        
        
        
    }

    private void OnTriggerEnter(Collider trigger)
    {
        switch (trigger.tag.ToLower())
        {
            case "water":
                inWater = true;
                break;
            case "snow":
                inSnow = true;
                break;
            case "fire":
                inFire = true;
                break;
            case "warming":
                nearFire = true;
                break;
        }
    }
    private void OnTriggerExit(Collider trigger)
    {
        switch (trigger.tag.ToLower())
        {
            case "water":
                inWater = false;
                break;
            case "snow":
                inSnow = false;
                break;
            case "fire":
                inFire = false;
                break;
            case "warming":
                nearFire = false;
                break;
        }
    }
}

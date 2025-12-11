// Created by MrRandomYT on 2025-12-11
// Script: PlayerMovement.cs

using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float sensitivity = 1f;
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;

    private float pitch = 0f;

    void LateUpdate()
    {
        pitch -= inputManager.CameraInputY * sensitivity/10;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}

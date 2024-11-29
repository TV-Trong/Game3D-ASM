using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] GameObject directionalLight;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] Material daySky;
    [SerializeField] Material nightSky;
    public float dayLength = 60f; // Full day duration in seconds
    int currentTime = 6;

    [Tooltip("Axis of rotation for the light")]
    public Vector3 rotationAxis = Vector3.right; // Default to rotating around X-axis

    private float rotationSpeed;

    void Start()
    {
        // Calculate the rotation speed
        rotationSpeed = 360f / dayLength; // Degrees per second
    }

    void Update()
    {
        // Rotate the directional light around the specified axis
        directionalLight.transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);

        // Get current rotation of the light
        float currentRotation = directionalLight.transform.rotation.x;

        // Ensure rotation is within 0-360 range
        if (currentRotation < 0) currentRotation += 360f;

        // Calculate in-game time
        float totalMinutes = (currentRotation / 360f) * 1440f; // Total minutes in 24 hours
        int hours = Mathf.FloorToInt(totalMinutes / 60f); // In-game hours
        int minutes = Mathf.FloorToInt(totalMinutes % 60f); // Remaining in-game minutes
        hours = (hours + currentTime) % 24;
        // Format and display time
        timeText.text = $"{hours:00}:{minutes:00}";

        if (hours > 20 || (hours > 0 && hours < 4)) RenderSettings.skybox = nightSky;
        else RenderSettings.skybox = daySky;
    }
}

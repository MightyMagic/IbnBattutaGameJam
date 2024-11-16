using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Cycle Settings")]
    public float dayLengthInSeconds = 120f; // Duration of one full day in seconds.
    public Transform lightSource;          // Assign your Directional Light here.

    [Header("Lighting Settings")]
    public Gradient lightColor;            // Color gradient for day/night transition.
    public AnimationCurve lightIntensity;  // Intensity curve for light brightness.

    private float timeOfDay = 0f;          // Current time of day as a fraction (0 to 1).

    void Update()
    {
        // Increment time based on the day length.
        timeOfDay += Time.deltaTime / dayLengthInSeconds;
        if (timeOfDay > 1f) timeOfDay -= 1f; // Loop back to the start.

        // Rotate the light source to simulate the sun's movement.
        float sunAngle = Mathf.Lerp(0f, 360f, timeOfDay);
        lightSource.rotation = Quaternion.Euler(new Vector3(sunAngle - 90f, 0f, 0f));

        // Change the light's color and intensity based on the time of day.
        lightSource.GetComponent<Light>().color = lightColor.Evaluate(timeOfDay);
        lightSource.GetComponent<Light>().intensity = lightIntensity.Evaluate(timeOfDay);
    }
}

using UnityEngine;
using System.Collections; // Required for Coroutines

public class Flicker : MonoBehaviour
{
    public Light targetLight; // Assign your light component in the Inspector
    public float minFlickerInterval = 5f; // Minimum time between flicker events
    public float maxFlickerInterval = 15f; // Maximum time between flicker events
    public float flickerDuration = 0.1f; // How long the light stays off during a flicker
    public int flickerCount = 2; // How many times it flickers during an event

    private bool isFlickering = false;

    void Start()
    {
        if (targetLight == null)
        {
            targetLight = GetComponent<Light>();
            if (targetLight == null)
            {
                Debug.LogError("No Light component found on this GameObject or assigned in Inspector.");
                enabled = false; // Disable script if no light is found
                return;
            }
        }
        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            // Wait for a random interval before the next flicker event
            float waitTime = Random.Range(minFlickerInterval, maxFlickerInterval);
            yield return new WaitForSeconds(waitTime);

            // Start flickering
            for (int i = 0; i < flickerCount; i++)
            {
                targetLight.enabled = false;
                yield return new WaitForSeconds(flickerDuration);
                targetLight.enabled = true;
                yield return new WaitForSeconds(flickerDuration); // Small delay between on/off states
            }
        }
    }
}
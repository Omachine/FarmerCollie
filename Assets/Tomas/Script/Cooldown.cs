using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    public Image cooldownImage; // Reference to the Image component
    private bool isCooldownActive = false; // Track whether the cooldown is active

    void Start()
    {
        cooldownImage.fillAmount = 0f; // Set fillAmount to 0 at the start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isCooldownActive) // Check if the cooldown is not active
        {
            StartCoroutine(CooldownEffect());
        }
    }

    private IEnumerator CooldownEffect()
    {
        isCooldownActive = true; // Set the cooldown to active
        cooldownImage.fillAmount = 1f; // Set fillAmount to 1

        float cooldownTime = 7f;
        while (cooldownImage.fillAmount > 0)
        {
            cooldownImage.fillAmount -= Time.deltaTime / cooldownTime; // Decrease fillAmount over 7 seconds
            yield return null; // Wait for the next frame
        }

        isCooldownActive = false; // Set the cooldown to inactive
    }
}

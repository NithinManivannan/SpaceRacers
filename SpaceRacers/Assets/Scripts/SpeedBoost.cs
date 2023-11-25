using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Make sure to include this namespace

public class SpeedBoost : MonoBehaviour
{
    private RedCar redCarScript;
    public Slider slider; // Assign this via the inspector or find it via script
    public GameObject fill;

    public static float energy = 1f; // Initialize energy to 100%
    public ParticleSystem nitroParticles1;  // Assign your nitro particle system in the Inspector
    public ParticleSystem nitroParticles2;
    void Start()
    {
        // Attempt to get the RedCar and ProgressBar script components attached to this GameObject
        redCarScript = GetComponent<RedCar>();
        slider = FindObjectOfType<Slider>();

        if (redCarScript == null)
        {
            Debug.LogError("SpeedBoost: RedCar script not found on this GameObject!");
        }

        if (slider == null)
        {
            Debug.LogError("SpeedBoost: Slider component not found in the scene!");
        }

    }
    private void Awake()
    {
        nitroParticles1.Stop();
        nitroParticles2.Stop();
    }

    void Update()
    {

        if (redCarScript != null && slider != null)
        {


            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) &&
                (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
                 Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) ||
                 Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                 Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            {

                if (SpeedBoost.energy > 0f)
                {

                    ActivateNitro();
                    redCarScript.SetSpeedBoost(true);
                    DecreaseEnergy();
                }
                else
                {
                    DeActivateNitro();
                    // Print a message or handle the case where there's not enough energy
                    Debug.Log("Not enough energy for speed boost!");
                    redCarScript.SetSpeedBoost(false);
                }

            }
            else
            {
                DeActivateNitro();
                redCarScript.SetSpeedBoost(false);
            }


            // Directly set the slider's value to the current energy level
            if (slider != null)
            {
                slider.value = energy;
            }
            if (energy <= 0)
            {
                // If the value is 0, set the handle to inactive
                fill.SetActive(false);
            }
            else
            {
                // Otherwise, make sure it's active
                fill.SetActive(true);
            }
        }
    }
    void ActivateNitro()
    {
        if (nitroParticles1 != null && nitroParticles2 != null)
        {

            nitroParticles1.Play();
            nitroParticles2.Play();

        }
    }

    void DeActivateNitro()
    {
        if (nitroParticles1 != null && nitroParticles2 != null)
        {

            nitroParticles1.Stop();
            nitroParticles2.Stop();

        }
    }
    private void DecreaseEnergy()
    {
        if (energy > 0f)
        {
            energy -= 0.10f * Time.deltaTime; // Decrease energy by 10% per second
            energy = Mathf.Clamp(energy, 0f, 1f); // Ensure energy stays within bounds
        }
    }

}
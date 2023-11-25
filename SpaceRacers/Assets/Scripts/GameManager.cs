using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    private bool isGameOver = false;
    private RedCar redCarScript;
    public Slider slider;

    private float increaseCooldown = 0f; // Cooldown for increase effect
    private float increaseBlueCooldown = 0f; // Cooldown for increase effect
    private float decreaseCooldown = 0f; // Cooldown for decrease effect

    private void Start()
    {
        // Find the GameObject with the RedCar script
        GameObject redCarGameObject = GameObject.FindWithTag("Player");

        // Get the RedCar script from the found GameObject
        redCarScript = redCarGameObject.GetComponent<RedCar>();
    }
    private void Update()
    {
        // Update cooldown timers
        increaseCooldown = Mathf.Max(0f, increaseCooldown - Time.deltaTime);
        decreaseCooldown = Mathf.Max(0f, decreaseCooldown - Time.deltaTime);
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (isGameOver)
        {
            return; // Don't process collisions if the game is already over
        }
        if (other.CompareTag("Player"))
        {
            // Get the ChangeColour script from the player
            ChangeColour playerChangeColour = other.GetComponent<ChangeColour>();

            if (playerChangeColour != null)
            {
                if (playerChangeColour.isRedColour() && gameObject.CompareTag("RedObstacle") && increaseCooldown <= 0f)
                {
                    // Red car passed through a red obstacle
                    IncreaseScore();
                    increaseCooldown = 5f; // Set the cooldown period for increase effect
                }
                else if (!playerChangeColour.isRedColour() && gameObject.CompareTag("BlueObstacle") && increaseBlueCooldown <= 0f)
                {
                    // Blue car passed through a blue obstacle
                    IncreaseScore();
                    increaseBlueCooldown = 5f; // Set the cooldown period for decrease effect
                }
                else if(((playerChangeColour.isRedColour() && gameObject.CompareTag("BlueObstacle")) || (!playerChangeColour.isRedColour()) && gameObject.CompareTag("RedObstacle")) && decreaseCooldown <= 0f)                
                {     // Game over condition
                    DecreaseScore();
                    decreaseCooldown = 5f;
                }
            }
        }
    }

    public void IncreaseScore()
    {
        // Increase energy by 7%
        SpeedBoost.energy = Mathf.Clamp01(SpeedBoost.energy + 0.07f);
        UpdateEnergyUI();

    }

    public void DecreaseScore()
    {
        // Decrease energy by 7%
        if (SpeedBoost.energy > 0f)
        {
            SpeedBoost.energy -= 0.07f;
            UpdateEnergyUI();
        }
        
    }
    private void UpdateEnergyUI()
    {
        if (slider != null)
        {
            slider.value = SpeedBoost.energy;
        }
    }

}

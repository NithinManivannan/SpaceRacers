using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    // This method is called when a button on the end screen is clicked (e.g., a "Restart" button)
    public void RestartGame()
    {
        SpeedBoost.energy = 1f;
        // Load the 'Start' scene
        SceneManager.LoadScene("Start");
    }
}
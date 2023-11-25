using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Ensure this namespace is included to work with UI

public class InstructionManager : MonoBehaviour
{
    public string instructionsSceneName = "Instructions"; // Name of the scene with instructions panel
    public Button instructionsButton;

    void Start() // Use Start or Awake to initialize values
    {
        instructionsButton.interactable = true; // Enable the button when the scene starts
    }

    // This method is called when the button is clicked
    public void ShowInstructions()
    {
        Debug.Log("ShowInstructions method called.");// Load the scene with the instructions panel
        SceneManager.LoadScene(instructionsSceneName); // Use the variable instead of hardcoding the string
    }
}
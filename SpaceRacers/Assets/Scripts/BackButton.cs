using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public Button backButton;
    // This method is called when the back button is clicked
    public void GoBackToStartScene()
    {
        // Load the 'Start' scene
        SceneManager.LoadScene("Start");
    }
}
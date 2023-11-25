using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        // Quit the application
        Application.Quit();
        // If we are running in the Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

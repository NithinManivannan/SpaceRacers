using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class StartGame : MonoBehaviour
{
    public TMP_Text countdownText; // Assign this in the inspector with your UI Text element
    public Button startButton; // Assign your Start button in the inspector
    public AudioClip countdownAudio; // The single audio file for the entire countdown
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // Add an AudioSource component
    }

    void OnEnable()
    {
        InitializeCountdown();
    }

    public void InitializeCountdown()
    {
        countdownText.gameObject.SetActive(false);
        startButton.interactable = true; // Enable the start button if it was disabled
    }

    public void StartCountdown()
    {
        Time.timeScale = 1;
        startButton.interactable = false; // Optionally disable the start button during the countdown
        countdownText.gameObject.SetActive(true);
        audioSource.PlayOneShot(countdownAudio); // Play the countdown audio
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        float audioLength = 3.13f;
        float countdownDuration = 3f; // Duration of the visual countdown (3 seconds)

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        countdownText.gameObject.SetActive(false);

        // Wait for the remainder of the audio clip to finish before loading the next scene
        yield return new WaitForSeconds(audioLength - countdownDuration);

        LoadGameScene(); // Method to load the game scene
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("Night"); // Replace with your game scene's name
    }

}

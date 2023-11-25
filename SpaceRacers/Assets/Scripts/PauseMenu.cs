using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Assign your pause menu panel to this in the inspector
    public TMP_Text timerText;
    public AudioClip backgroundMusicClip; // Assign your AudioClip here in the inspector

    private AudioSource backgroundMusicSource; // This will be your AudioSource
    private bool isPaused = false;
    private float elapsedTime = 0f;
   

    void Start()
    {
        // Ensure there's an AudioSource component on this GameObject
        backgroundMusicSource = gameObject.GetComponent<AudioSource>();
        if (backgroundMusicSource == null) // If not found, add one.
        {
            backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        }
        // Configure the AudioSource component
        backgroundMusicSource.clip = backgroundMusicClip;
        backgroundMusicSource.loop = true; // If you want the music to loop
        backgroundMusicSource.Play(); // Start playing the music
    }

    void Update()
    {
        
        if (!isPaused)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC WAS PRESSED");
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume normal time
        backgroundMusicSource.UnPause(); // Unpause the music
        isPaused = false;

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Debug.Log("before"+elapsedTime);
        /*UpdateTimerText();*/
        Time.timeScale = 0f; // Freeze game time
        backgroundMusicSource.Pause(); // Pause the music
        Debug.Log("after"+elapsedTime);
      
        UpdateTimerText();
        isPaused = true;
    }
    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    void UpdateTimerText()
    {
 
        // Display the elapsed time in minutes and seconds
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }


    public void Restart()
    {
        SpeedBoost.energy = 1f;
        elapsedTime = 0f;
        // Ensure the AudioSource is not null and stop it before restarting the scene
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.Stop();
        }
        
        SceneManager.LoadScene("Start"); 
    }
}


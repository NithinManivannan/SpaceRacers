
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGame : MonoBehaviour
{
    public GameObject finishingLine;
    public GameObject carObject;
    public GameObject endGameUI;
    public TMP_Text currentTimerText; // Assign this to your current timer text in the inspector
    public TMP_Text bestTimerText;    // Assign this to your best timer text in the inspector
    public PauseMenu pauseMenuScript; // Assign your PauseMenu script in the inspector

    private float bestTime = Mathf.Infinity;
    private const string BestTimePrefKey = "BestTime";

    void Start()
    {
        // Load the best time from PlayerPrefs
        bestTime = PlayerPrefs.GetFloat(BestTimePrefKey, 300.0f);
        UpdateBestTimeText(bestTime);

    }
       



void Update()
    {
        carObject = GameObject.FindWithTag("Player");

/*        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs cleared");
*/
        if (IsPlayerAtFinishingLine())
        {
            Debug.Log("Game Finished");
            float currentTime = pauseMenuScript.GetElapsedTime();
            ShowEndGameUI(currentTime);
            Time.timeScale = 0f; // Stop the game time
        }
    }

    void ShowEndGameUI(float currentTime)
    {
        if (endGameUI != null)
        {
            endGameUI.SetActive(true);
            currentTimerText.text = "Current Time: " + FormatTime(currentTime);
            if (currentTime < bestTime)
            {;
                bestTime = currentTime;
                PlayerPrefs.SetFloat(BestTimePrefKey, bestTime);
                PlayerPrefs.Save();
                UpdateBestTimeText(bestTime);
            }
            
            Debug.Log("BEST TIME" + bestTime);
        }
        else
        {
            Debug.LogError("EndGameUI is not set!");
        }
    }

    void UpdateBestTimeText(float time)
    {
       
        bestTimerText.text = "Best Time: " + FormatTime(time);
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    bool IsPlayerAtFinishingLine()
    {
        if (finishingLine == null)
        {
            Debug.LogWarning("Finishing line not assigned!");// If the finishing line object is not assigned, return false
            return false;
        }
        Vector3 carPosition = carObject.transform.position;
        // Check the distance between the player and the finishing line
        float distance = Vector3.Distance(carPosition, finishingLine.transform.position);
        // You can adjust the threshold distance based on your game's scale
        float thresholdDistance = 10.0f;

        return distance < thresholdDistance;
    }
}


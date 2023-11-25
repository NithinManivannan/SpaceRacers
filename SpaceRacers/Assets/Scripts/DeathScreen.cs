using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public GameObject carObject;
    public GameObject deathGameUI;
	public float minYThreshold = -75f;
    void Update()
    {
        carObject = GameObject.FindWithTag("Player");
        if (carObject.transform.position.y < minYThreshold)
        {
            if (deathGameUI != null) deathGameUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
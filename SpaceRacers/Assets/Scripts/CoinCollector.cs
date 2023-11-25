using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private ChangeColour playerChangeColour; // Reference to the ChangeColour component
    public AudioClip pickupSound; // The sound to play when a coin is picked up
    private AudioSource audioSource; // Reference to the AudioSource component


    void Start()
    {
        playerChangeColour = GetComponent<ChangeColour>();
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component

        if (playerChangeColour == null)
        {
            Debug.LogError("CoinCollector: ChangeColour component not found!");
        }
        if (audioSource == null)
        {
            Debug.LogError("CoinCollector: AudioSource component not found!");
        }
    }
     public void IncreaseScore()
    {
        // Increase energy by 3.5%
        SpeedBoost.energy = Mathf.Clamp01(SpeedBoost.energy + 0.035f);
        
    }

    public void DecreaseScore()
    {
        // Decrease energy by 3.5%
        if (SpeedBoost.energy > 0f)
        {
            SpeedBoost.energy -= 0.035f;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isRedCoin = other.gameObject.CompareTag("RedCoin");
        bool isBlueCoin = other.gameObject.CompareTag("BlueCoin");
        bool isPurpleCoin = other.gameObject.CompareTag("PurpleCoin");

        if (isRedCoin || isBlueCoin || isPurpleCoin)
        {
            // Common action for all coins
            Destroy(other.gameObject);

            // Conditional actions based on the coin type and player color
            if (isRedCoin)
            {
                if (playerChangeColour != null)
                {
                    if (playerChangeColour.isRedColour())
                    {
                        IncreaseScore();
                        // Play pickup sound if a valid coin is picked
                        audioSource.PlayOneShot(pickupSound);
                    }
                    else
                    {
                        DecreaseScore();
                    }
                }
            }
            else if (isBlueCoin)
            {
                if (playerChangeColour != null)
                {
                    if (playerChangeColour.isRedColour())
                    {
                        DecreaseScore();
                    }
                    else
                    {
                        IncreaseScore();
                        // Play pickup sound if a valid coin is picked
                        audioSource.PlayOneShot(pickupSound);
                    }
                }
            }
            else if (isPurpleCoin)
            {
                IncreaseScore();
                // Play pickup sound if a valid coin is picked
                audioSource.PlayOneShot(pickupSound);
            }

            
        }
    }
}


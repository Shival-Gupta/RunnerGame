using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Clips")]
    public AudioClip coinCollectClip;
    public AudioClip obstacleHitClip;
    public AudioClip backgroundMusicClip;
    public AudioClip playerDeathClip;

    private AudioSource audioSource;

    void Awake()
    {
        // Ensure there's only one instance of AudioManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optionally keep it across scenes
        }
        else
        {
            Destroy(gameObject);
        }

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Start background music
        PlayBackgroundMusic();
    }

    // Method to play background music
    public void PlayBackgroundMusic()
    {
        if (backgroundMusicClip != null)
        {
            audioSource.loop = true;
            audioSource.clip = backgroundMusicClip;
            audioSource.Play();
        }
    }

    // Method to play coin collection sound
    public void PlayCoinCollectSound()
    {
        if (coinCollectClip != null)
        {
            audioSource.PlayOneShot(coinCollectClip);
        }
    }

    // Method to play obstacle hit sound
    public void PlayObstacleHitSound()
    {
        if (obstacleHitClip != null)
        {
            audioSource.PlayOneShot(obstacleHitClip);
        }
    }

    // Method to play player death sound
    public void PlayPlayerDeathSound()
    {
        if (playerDeathClip != null)
        {
            audioSource.PlayOneShot(playerDeathClip);
        }
    }
}

using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("UI Elements")]
    public TMP_Text scoreText; // Text for score
    public TMP_Text livesText; // Text for lives

    private int totalScore = 0; // Track total score
    private int coins = 0; // Track coins collected

    private void Awake()
    {
        // Singleton pattern to ensure only one UIController
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this across scenes if necessary
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScore(0); // Initialize score display
        UpdateLives(GameController.instance.playerLives); // Initialize lives display
    }

    public void UpdateScore(int newScore)
    {
        totalScore = newScore; // Update total score
        scoreText.text = "Score: " + totalScore;
    }

    public void CollectCoin()
    {
        coins++;
        UpdateScore(totalScore + GameController.instance.coinValue); // Update score with coin value
    }

    public void UpdateLives(int lives)
    {
        if (lives < 0) // Prevent negative lives from being displayed
        {
            lives = 0; // Set lives to 0 if it is negative
        }

        livesText.text = "Lives: " + "♥♥♥♥♥".Substring(0, lives); // Display hearts for lives
    }


    public int GetCoins()
    {
        return coins;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("UI Elements")]
    public Text scoreText; // Text for score
    public Text coinText; // Text for coins
    public Image[] lifeIcons; // Array of heart icons

    private float score = 0f;
    private int coins = 0;
    public Transform playerTransform; // Reference to player for calculating score based on distance traveled
    private Vector3 initialPosition;

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
        initialPosition = playerTransform.position;
        UpdateScore(0); // Initialize score display
        UpdateCoins(0); // Initialize coin display
    }

    private void Update()
    {
        // Update score based on the distance player has traveled
        score = (playerTransform.position.z - initialPosition.z) * 0.1f; // Scale down the score
        UpdateScore(Mathf.FloorToInt(score)); // Update score display in UI
    }

    public void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }

    public void UpdateCoins(int amount)
    {
        coins += amount;
        coinText.text = "Coins: " + coins;
    }

    public void UpdateLives(int lives)
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = i < lives; // Enable/disable hearts based on remaining lives
        }
    }

    public float GetScore()
    {
        return score;
    }

    public int GetCoins()
    {
        return coins;
    }
}

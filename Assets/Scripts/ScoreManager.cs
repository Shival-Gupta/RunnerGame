using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;  // Singleton instance for easy access
    public Text scoreText;
    public Text coinText;

    private float score = 0f;
    private int coins = 0;

    public Transform playerTransform; // Reference to player for calculating score based on distance traveled
    private Vector3 initialPosition;

    void Awake()
    {
        // Singleton pattern to ensure only one ScoreManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        initialPosition = playerTransform.position;
    }

    void Update()
    {
        // Score based on the distance player has traveled
        score = (playerTransform.position.z - initialPosition.z) * 0.1f; // Scale down the score
        scoreText.text = "Score: " + Mathf.FloorToInt(score);

        // Update the coin text
        coinText.text = "Coins: " + coins;
    }

    public void AddCoin(int amount)
    {
        coins += amount;
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

using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton instance
    public Text scoreText;
    private float score = 0f;

    void Awake()
    {
        // Ensure that there is only one instance of ScoreManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
        }
    }

    void Update()
    {
        // Increment score over time
        score += Time.deltaTime * 10;
        scoreText.text = "Score: " + Mathf.FloorToInt(score);
    }

    // Method to return the current score
    public float GetScore()
    {
        return score;
    }
}

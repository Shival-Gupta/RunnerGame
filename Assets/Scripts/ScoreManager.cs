using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;       // Singleton instance
    public Text scoreText;
    private float score = 0f;
    public Transform playerTransform;          // Reference to player's position

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance
        }
    }

    void Update()
    {
        score = playerTransform.position.z;    // Score based on player's Z-position
        scoreText.text = "Score: " + Mathf.FloorToInt(score);
    }
}

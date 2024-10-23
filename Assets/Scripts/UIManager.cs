using UnityEngine;
using TMPro; // Make sure to include this namespace for TextMesh Pro

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TMP_Text scoreText; // Changed from Text to TMP_Text
    public TMP_Text livesText; // Changed from Text to TMP_Text

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score; // TMP supports the same text format
    }

    public void UpdateLives(int lives)
    {
        livesText.text = "Lives: " + lives; // Update lives text
    }
}

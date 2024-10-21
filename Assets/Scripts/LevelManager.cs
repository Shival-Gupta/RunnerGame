using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static int currentLevel = 1; // Tracks current level
    public Text levelText;
    public Text timerText;

    private float levelTimer;
    private float targetScore;
    private bool levelCompleted = false;

    void Start()
    {
        if (currentLevel == 1)
        {
            StartLevel1();
        }
        else if (currentLevel == 2)
        {
            StartLevel2();
        }
        else if (currentLevel == 3)
        {
            StartLevel3();
        }
    }

    void Update()
    {
        if (!levelCompleted)
        {
            if (currentLevel == 1)
            {
                HandleLevel1();
            }
            else if (currentLevel == 2)
            {
                HandleLevel2();
            }
            else if (currentLevel == 3)
            {
                HandleLevel3();
            }
        }
    }

    void StartLevel1()
    {
        levelTimer = 30f; // Player needs to survive for 30 seconds
        levelText.text = "Level 1: Survive!";
    }

    void HandleLevel1()
    {
        levelTimer -= Time.deltaTime;
        timerText.text = "Time Left: " + Mathf.FloorToInt(levelTimer);

        if (levelTimer <= 0)
        {
            CompleteLevel();
        }
    }

    void StartLevel2()
    {
        targetScore = 500f; // Player needs to reach 500 score
        levelText.text = "Level 2: Reach 500 Points!";
    }

    void HandleLevel2()
    {
        if (ScoreManager.instance != null)
        {
            float currentScore = ScoreManager.instance.GetScore(); // Check for null instance
            if (currentScore >= targetScore)
            {
                CompleteLevel();
            }
        }
    }

    void StartLevel3()
    {
        levelTimer = 60f; // Player needs to survive for 60 seconds
        levelText.text = "Level 3: Survive until the timer runs out!";
    }

    void HandleLevel3()
    {
        levelTimer -= Time.deltaTime;
        timerText.text = "Time Left: " + Mathf.FloorToInt(levelTimer);

        if (levelTimer <= 0)
        {
            CompleteLevel();
        }
    }

    void CompleteLevel()
    {
        levelCompleted = true;
        levelText.text = "Level Complete!";
        Invoke("NextLevel", 2f); // Move to the next level after a delay
    }

    void NextLevel()
    {
        currentLevel++;
        if (currentLevel > 3)
        {
            currentLevel = 1; // Reset to Level 1 after completing all levels
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the scene for the next level
    }
}

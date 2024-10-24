using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    // Player properties
    public int playerLives = 3;  // Starting number of lives
    public int playerScore = 0;   // Initial score
    public int coinValue = 100;   // Value for each collected coin

    // Level properties
    public float[] levelTimes = { 30f, 60f, 120f }; // Time for each level
    private int currentLevel = 0; // Index of the current level
    private float levelTimer;

    // UI elements
    public TMP_Text levelText;
    public TMP_Text timerText;
    public GameObject gameOverUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep GameController persistent across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartLevel();
    }

    private void Update()
    {
        levelTimer -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.FloorToInt(levelTimer);

        if (levelTimer <= 0)
        {
            CompleteLevel();
        }
    }

    public void AddScore(int value)
    {
        playerScore += value;
        UIController.instance.UpdateScore(playerScore); // Update the UI with the new score
    }

    public void ReduceLife()
    {
        playerLives--;
        UIController.instance.UpdateLives(playerLives); // Update UI with lives

        if (playerLives <= 0)
        {
            GameOver();
        }
    }

    private void StartLevel()
    {
        levelTimer = levelTimes[currentLevel];
        UpdateLevelText();
        playerScore = 0; // Reset score for new level
    }

    private void CompleteLevel()
    {
        currentLevel++;
        if (currentLevel < levelTimes.Length)
        {
            StartLevel(); // Load the next level
        }
        else
        {
            Debug.Log("Game Completed!"); // Placeholder for victory logic
        }
    }

    private void UpdateLevelText()
    {
        levelText.text = "Level " + (currentLevel + 1); // Display the current level
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true); // Show Game Over screen
        Time.timeScale = 0; // Pause the game
    }

    public void Retry()
    {
        Time.timeScale = 1; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the application
    }
}

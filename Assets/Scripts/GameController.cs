using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("Player Stats")]
    public int playerLives = 3;
    public int playerScore = 0;
    public int coinValue = 100;

    [Header("UI Elements")]
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public GameObject gameOverUI;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // Optionally, DontDestroyOnLoad(gameObject) if you want to persist the GameController across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeGame();
    }

    public void AddScore()
    {
        playerScore += coinValue;
        UpdateScore(playerScore);
        AudioManager.instance.PlayCoinCollectSound(); // Play coin collection sound
    }

    public void ReduceLife()
    {
        AudioManager.instance.PlayObstacleHitSound();
        playerLives--;
        UpdateLives(playerLives);
        if (playerLives <= 0)
        {
            GameOver();
        }
    }

    private void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    private void UpdateLives(int lives)
    {
        livesText.text = $"Lives: {lives}";
    }

    private void GameOver()
    {
        Time.timeScale = 0; // Pause the game
        gameOverUI.SetActive(true); // Show the Game Over UI
        AudioManager.instance.PlayPlayerDeathSound(); // Play player death sound
    }

    // Generic Retry method to load a specific scene by index
    public void RetryLevel(int sceneIndex)
    {
        InitializeGame(); // Reset the game stats before restarting
        SceneManager.LoadScene(sceneIndex); // Load the specified scene by index
        Time.timeScale = 1; // Ensure the game runs normally after the scene reload
    }

    // Quit the game
    public void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }

    // Initialize or reset the game
    private void InitializeGame()
    {
        playerLives = 3;
        playerScore = 0;
        UpdateLives(playerLives);
        UpdateScore(playerScore);
        gameOverUI.SetActive(false); // Hide Game Over UI
        AudioManager.instance.PlayGameStartSound();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScore(0);
        UpdateLives(playerLives);
    }

    public void AddScore()
    {
        playerScore += coinValue;
        UpdateScore(playerScore);
    }

    public void ReduceLife()
    {
        playerLives--;
        UpdateLives(playerLives);
        if (playerLives <= 0) GameOver();
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
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

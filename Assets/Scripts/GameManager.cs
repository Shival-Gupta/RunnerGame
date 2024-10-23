using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerLives = 3; // Starting number of lives
    public int playerScore = 0; // Initial score

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep GameManager persistent across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int value)
    {
        playerScore += value;
        UIManager.instance.UpdateScore(playerScore);
    }

    public void ReduceLife()
    {
        playerLives--;
        UIManager.instance.UpdateLives(playerLives);

        if (playerLives <= 0)
        {
            PlayerDied();
        }
    }

    private void PlayerDied()
    {
        // Load Game Over scene or trigger game over UI
        SceneManager.LoadScene("GameOverScene");
    }
}

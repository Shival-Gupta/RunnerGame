using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        if (gameOverUI != null) // Null check for safety
        {
            gameOverUI.SetActive(true); // Activate Game Over screen
            Time.timeScale = 0; // Pause the game
        }
    }

    public void Retry()
    {
        Time.timeScale = 1; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

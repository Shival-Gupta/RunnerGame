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
        gameOverUI.SetActive(true);  // Show Game Over screen
        Time.timeScale = 0;  // Pause the game
    }

    public void Retry()
    {
        Time.timeScale = 1;  // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the current scene
    }

    public void QuitGame()
    {
        Application.Quit();  // Quit the application
    }
}

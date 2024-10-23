using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    public static CustomSceneManager instance; // Singleton instance

    private void Awake()
    {
        // Implement singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    // Method to load a specific level
    public void LoadLevel(int levelIndex)
    {
        // Load the scene with the specified index
        SceneManager.LoadScene("Level" + levelIndex);
    }

    // Method to pause the game
    public void PauseGame()
    {
        Time.timeScale = 0; // Freeze game time
        // Optionally, show pause menu UI
    }

    // Method to resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1; // Unfreeze game time
        // Optionally, hide pause menu UI
    }

    // Method to restart the current level
    public void RestartLevel()
    {
        // Reload the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // Method to load the main menu (optional)
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Assuming you have a MainMenu scene
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public void RetryGame()
    {
        // Load the Level1 scene (make sure the scene is added to build settings)
        SceneManager.LoadScene("Level1"); // Use the exact name of your level scene
    }
}

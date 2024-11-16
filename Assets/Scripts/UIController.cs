using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text scoreText, livesText;

    public void UpdateUI(int score, int lives)
    {
        scoreText.text = $"Score: {score}";
        livesText.text = $"Lives: {lives}";
    }
}

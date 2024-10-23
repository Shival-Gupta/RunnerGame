using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Text levelText;
    public Text timerText;
    public float phaseTime = 60f;  // Time per phase
    private float timer;

    private int currentPhase = 1;

    void Start()
    {
        timer = phaseTime;
        UpdateLevelText();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.FloorToInt(timer);

        if (timer <= 0)
        {
            CompletePhase();
        }
    }

    void CompletePhase()
    {
        currentPhase++;
        timer = phaseTime;  // Reset timer for next phase
        UpdateLevelText();
        PlayerMovement.instance.IncreaseDifficulty();  // Increase difficulty for next phase
    }

    void UpdateLevelText()
    {
        levelText.text = "Phase " + currentPhase;
    }
}

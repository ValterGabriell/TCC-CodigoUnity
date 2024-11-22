using UnityEngine;

public class ModelTime : MonoBehaviour
{
    public float initialTimeInFloat = 100f;
    public string timeElapsed = string.Empty;
    public GameManager gameManager;
    public bool hasTimeEnded = false;

    private void FixedUpdate()
    {
        if (!gameManager.currentLevel.isLevelCompleted)
        {
            initialTimeInFloat -= Time.deltaTime;

            if (initialTimeInFloat < 0.5)
            {
                gameManager.currentLevel.EndLevel(false);
                hasTimeEnded = true;
            }
            else
            {
                int minutes = Mathf.FloorToInt(initialTimeInFloat / 60);
                int seconds = Mathf.FloorToInt(initialTimeInFloat % 60);
                timeElapsed = string.Format("{0:00}:{1:00}", minutes, seconds);
            }            
        }
    }
}

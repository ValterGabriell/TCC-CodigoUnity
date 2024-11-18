using UnityEngine;

public class ModelTime : MonoBehaviour
{
    public float elapsedTime = 5f; 
    private string timeElapsed = string.Empty;
    public GameManager gameManager;
    private void FixedUpdate()
    {
        if (!gameManager.currentLevel.isLevelCompleted)
        {
            elapsedTime -= Time.deltaTime;

            if (elapsedTime < 0.5)
            {
                gameManager.currentLevel.isLevelCompleted = true;
                gameManager.currentLevel.hasSuccess = false;
            }
            else
            {
                int minutes = Mathf.FloorToInt(elapsedTime / 60);
                int seconds = Mathf.FloorToInt(elapsedTime % 60);
                timeElapsed = string.Format("{0:00}:{1:00}", minutes, seconds);
            }            
        }
    }
}

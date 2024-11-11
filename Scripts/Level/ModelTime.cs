using UnityEngine;

public class ModelTime : MonoBehaviour
{
    private float elapsedTime; 
    public string timeElapsed = string.Empty;


    public GameManager gameManager;
    private void FixedUpdate()
    {
        if (!gameManager.isLevelCompleted)
        {
            elapsedTime += Time.deltaTime;

            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timeElapsed = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}

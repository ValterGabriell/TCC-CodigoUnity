using UnityEngine;

public class Level02 : MonoBehaviour
{
    public GameManager gameManager;
    public PointsModel pointsModel;
    public PlayerCollision playerCollision;
    public bool isOnPickableArea = false;
    public bool hasTheKey = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (hasTheKey)
            {
                pointsModel.increasePoint(50);
              //  gameManager.isLevelCompleted = true;
            }
            else
            {
                pointsModel.decreasePoint(25);
            }
        }
    }

    private void Start()
    {
        Time.timeScale = 0;
    }
}

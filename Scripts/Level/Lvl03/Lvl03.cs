using UnityEngine;

public class Lvl03 : MonoBehaviour
{
    public GameManager gameManager;
    public PointsModel PointsModel;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PointsModel.increasePoint(100);
            gameManager.isLevelCompleted = true;
        }
    }
}

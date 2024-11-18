using UnityEngine;
using UnityEngine.UIElements;

public class Level01 : MonoBehaviour
{
    public GameManager gameManager;
    public PointsModel pointsModel;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            pointsModel.increasePoint(100);
           // gameManager.isLevelCompleted = true;
        }
    } 
}

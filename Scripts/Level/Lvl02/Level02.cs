using UnityEngine;

public class Level02 : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerCollision playerCollision;
    public bool isOnPickableArea = false;
    public bool hasTheKey = false;

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.CompareTag("Player"))
        {
            if (hasTheKey)
            {
                gameManager.CompleteLevel();
            }
        }
    }
}

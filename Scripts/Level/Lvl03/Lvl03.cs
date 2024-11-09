using UnityEngine;

public class Lvl03 : MonoBehaviour
{
    public GameManager gameManager;
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Player"))
        {
            gameManager.isLevelCompleted = true;
        }
    }
}

using UnityEngine;

public class FinalAreaCollision : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.currentLevel.hasEndedLevel = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            gameManager.currentLevel.hasEndedLevel = true;
        }
    }
}

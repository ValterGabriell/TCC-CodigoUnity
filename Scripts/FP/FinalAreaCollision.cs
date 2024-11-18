using UnityEngine;

public class FinalAreaCollision : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            gameManager.currentLevel.hasPassedThrougTheFinalArea = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.tag);
        if (collision.collider.CompareTag("Player"))
        {
            gameManager.currentLevel.hasPassedThrougTheFinalArea = true;
        }
    }
}

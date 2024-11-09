using UnityEngine;
using UnityEngine.UIElements;

public class Level01 : MonoBehaviour
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

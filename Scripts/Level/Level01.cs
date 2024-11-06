using UnityEngine;

public class Level01 : MonoBehaviour
{
    public GameManager gameManager;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisao");
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Colidiu: " + collision.collider.tag);
            gameManager.CompleteLevel();
        }
    }
}

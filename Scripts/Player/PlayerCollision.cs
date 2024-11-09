using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public bool canInteract = false;
    public GameManager gameManager;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("InteractArea"))
        {
            Debug.Log("Jogador entrou na area de colisao");
            canInteract = true;
        }

        if (collision.collider.CompareTag("Finish"))
        {
            Debug.Log("Jogador entrou na area de finalizar");
            gameManager.isLevelCompleted = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("InteractArea"))
        {
            Debug.Log("Jogador saiu na area de colisao");
            canInteract = false;
        }

        if (collision.collider.CompareTag("Finish"))
        {
            Debug.Log("Jogador entrou na area de finalizar");
            gameManager.isLevelCompleted = false;
        }
    }
}

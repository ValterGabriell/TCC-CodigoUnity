using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public bool canInteract = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("InteractArea"))
        {
            Debug.Log("Jogador entrou na area de colisao");
            canInteract = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("InteractArea"))
        {
            Debug.Log("Jogador saiu na area de colisao");
            canInteract = false;
        }
    }
}

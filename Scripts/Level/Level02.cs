using UnityEngine;

public class Level02 : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerCollision playerCollision;
    private bool hasTheKey = false;
    private bool checkIfHasTheKey = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("TEM A CHAVE? " + hasTheKey);
            if (hasTheKey)
            {
                gameManager.CompleteLevel();
            }
        }
    }

    private void FixedUpdate()
    {
        if (playerCollision.canInteract)
        {
            Debug.Log("ENTROU NA AREA DE ITERAÇÃO E PODE INTERGAIR");
            hasTheKey = true;
        }
    }
}

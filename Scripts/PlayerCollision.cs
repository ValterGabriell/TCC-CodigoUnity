using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMove movement;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
           //movement.enabled = false;
        }
    }
}

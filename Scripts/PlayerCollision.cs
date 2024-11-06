using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public bool canInteract = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("InteractArea"))
        {
            canInteract = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("InteractArea"))
        {
            canInteract = false;
        }
    }
}

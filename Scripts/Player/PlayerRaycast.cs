using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{

    public bool playerIsColliding = false;
    private void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        int layerMask = 1 << 8;
        int sizeRaycast = 3;

        Debug.DrawRay(transform.position, fwd, Color.red);

        var isColliding = Physics.Raycast(transform.position, fwd, out RaycastHit hit, sizeRaycast, layerMask);
        if (isColliding)
        {
            if (hit.collider.CompareTag("obstacle"))
            {
                playerIsColliding = true;
            }
        }
        else
        {
            playerIsColliding = false;
        }
    }
}

using System;
using UnityEngine;

interface IInteractable
{
    public void Interact(GameObject gameObject);
    public string TextInteraction(string text);
}

public class FPInteract : MonoBehaviour
{
    const float interactRange = 4f;
    public Transform raycastOrigin;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckInteraction();
        }
    }

    void CheckInteraction()
    {
        Ray r = new(raycastOrigin.position, raycastOrigin.forward);

        
        Debug.DrawRay(r.origin, r.direction * interactRange, Color.red, 0.2f); // Tempo menor para visualização dinâmica

        var isColliding = Physics.Raycast(r, out RaycastHit hitInfo, interactRange);
        if (isColliding)
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactable))
            {
                Debug.Log($"Interactable: {interactable}");
                interactable.Interact(hitInfo.collider.gameObject);
            }
        } 
    }
}

using System.Collections;
using UnityEngine;

public class RotateDice : MonoBehaviour, IInteractable
{
    Outline outline;
    public bool isEnabled;
    public bool isRotating;

    private int currentFace = 1;
    public void DisableOutline()
    {
        isEnabled = false;
        outline.enabled = false;
    }

    public void EnableOutline()
    {
        isEnabled = true;
        outline.enabled = true;
    }

    public void Interact()
    {

        if (isRotating)
        {
            return;
        }

        currentFace++;
        if (currentFace > 6)
        {
            currentFace = 1;
        }

        Vector3 rotation = Vector3.zero;
        switch (currentFace)
        {
            case 1:
                rotation = new Vector3(0, 0, 0); // Face 1
                break;
            case 2:
                rotation = new Vector3(90, 0, 0); // Face 2
                break;
            case 3:
                rotation = new Vector3(180, 0, 0); // Face 3
                break;
            case 4:
                rotation = new Vector3(270, 0, 0); // Face 4
                break;
            case 5:
                rotation = new Vector3(0, 90, 0); // Face 5
                break;
            case 6:
                rotation = new Vector3(0, 270, 0); // Face 6
                break;
        }

        StartCoroutine(RotateToTarget(Quaternion.Euler(rotation)));

    }


    private IEnumerator RotateToTarget(Quaternion targetRotation)
    {
        isRotating = true;
        float duration = 1f; // Duração da rotação em segundos
        float elapsedTime = 0f;
        Quaternion initialRotation = transform.localRotation;

        while (elapsedTime < duration)
        {
            // Interpola entre a rotação inicial e a rotação alvo
            transform.localRotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Garante que a rotação final seja exatamente a desejada
        transform.localRotation = targetRotation;
        isRotating = false;
    }

    public string TextInteraction(string text)
    {
        return text;
    }
    private void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }


}

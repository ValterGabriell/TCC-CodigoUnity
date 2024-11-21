using System.Collections;
using UnityEngine;

public class RotateDice : MonoBehaviour, IInteractable
{

    public bool isRotating;
    public LevelManager01 levelManager;

    public void Interact(GameObject gameObject)
    {
        HUDInteraction.instance.DisableInteractionText();
        if (isRotating)
        {
            return;
        }

        IncrementFaceByDice(gameObject);

    }

    public string TextInteraction(string text)
    {
        return text;
    }

    public void IncrementFaceByDice(GameObject gameObject)
    {
        switch (gameObject.name)
        {
            case "DICE_BLUE_01":
                levelManager.IncrementDiceBlueOneFace();
                RotateDiceByFace(levelManager.ReturnDiceBlueOneFace());
                break;

            case "DICE_BLUE_02":
                levelManager.IncrementDiceBlueTwoFace();
                RotateDiceByFace(levelManager.ReturnDiceBlueTwoFace());
                break;

            case "DICE_RED_01":
                levelManager.IncrementDiceRedOneFace();
                RotateDiceByFace(levelManager.ReturnDiceRedOneFace());
                break;

            case "DICE_RED_02":
                levelManager.IncrementDiceRedTwoFace();
                RotateDiceByFace(levelManager.ReturnDiceRedTwoFace());
                break;
        }
    }

    public void RotateDiceByFace(int currentFace)
    {
        Debug.Log("CurrentDiceFace: " + currentFace);
        Vector3 rotation = Vector3.zero;
        switch (currentFace)
        {
            case 1:
                rotation = new Vector3(270, 0, 0); // Face 1
                break;
            case 2:
                rotation = new Vector3(360, 0, 0); // Face 2
                break;
            case 3:
                rotation = new Vector3(360, 0, 270); // Face 3
                break;
            case 4:
                rotation = new Vector3(180, 0, 270); // Face 4
                break;
            case 5:
                rotation = new Vector3(180, 0, 0); // Face 5
                break;
            case 6:
                rotation = new Vector3(90, 0, 0); // Face 6
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
}

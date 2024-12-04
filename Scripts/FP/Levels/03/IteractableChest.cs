using UnityEngine;

public class IteractableChest : MonoBehaviour, IInteractable
{
    public LevelManager02 levelManager;
    public void Interact(GameObject gameObject)
    {
        if (gameObject.name == "BLUE_CHEST")
        {
            levelManager.hasBlueKey = true;
        }

        if (gameObject.name == "RED_CHEST")
        {
            levelManager.hasRedKey = true;
        }

        if (gameObject.name == "WHITE_CHEST")
        {
            levelManager.hasWhiteKey = true;
        }
    }

    public string TextInteraction(string text)
    {
        return text;
    }
}

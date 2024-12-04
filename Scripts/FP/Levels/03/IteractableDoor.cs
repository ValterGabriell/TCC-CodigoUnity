using UnityEngine;

public class IteractableDoor : MonoBehaviour, IInteractable
{
    public GameManager gameManager;
    public LevelManager02 levelManager;
    public void Interact(GameObject gameObject)
    {
        if (gameObject.name == "BLUE_DOOR")
        {
            if (levelManager.hasBlueKey)
            {
                gameManager.currentLevel.RestartLevel();
            }
        }

        if (gameObject.name == "RED_DOOR")
        {
            if (levelManager.hasRedKey)
            {
                gameManager.currentLevel.RestartLevel();
            }
        }

        if (gameObject.name == "WHITE_DOOR")
        {
            if (levelManager.hasWhiteKey)
            {
                gameManager.currentLevel.EndLevel(true);
            }
        }
    }

    public string TextInteraction(string text)
    {
        return text;
    }
}

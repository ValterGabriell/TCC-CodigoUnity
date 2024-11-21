using UnityEngine;

public class IteractableDoor : MonoBehaviour, IInteractable
{
    public LevelManager02 levelManager;
    public void Interact(GameObject gameObject)
    {
        Debug.Log(gameObject.name);
        if (gameObject.name == "BLUE_DOOR")
        {
            if (levelManager.hasBlueKey)
            {
                levelManager.RestartLevel();
            }
        }

        if (gameObject.name == "RED_DOOR")
        {
            if (levelManager.hasRedKey)
            {
                levelManager.RestartLevel();
            }
        }

        if (gameObject.name == "WHITE_DOOR")
        {
            if (levelManager.hasWhiteKey)
            {
                Debug.Log("OPEN WHITE DOOR");
            }
        }
    }

    public string TextInteraction(string text)
    {
        return text;
    }
}

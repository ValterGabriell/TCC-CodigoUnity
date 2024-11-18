using UnityEngine;

public class OpenDoor : MonoBehaviour, IInteractable
{
    public LevelManager01 levelManager;
    public void Interact(GameObject gameObject)
    {
        levelManager.CheckIfCanOpenDoor();
    }

    public string TextInteraction(string text)
    {
       return text;
    }
}

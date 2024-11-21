using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int currentLevel = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (HUDInteractionToDo.instance.root.visible)
            {
                Time.timeScale = 1f;
                HUDInteractionToDo.instance.DisableInteractionText();
            }
            else
            {
                Time.timeScale = 0f;
                HUDInteractionToDo.instance.EnableInteractionText(currentLevel);
            }
        }
    }
}

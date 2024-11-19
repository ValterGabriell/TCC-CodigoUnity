using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (HUDInteractionToDo.instance.root.visible)
            {
                HUDInteractionToDo.instance.DisableInteractionText();
            }
            else
            {
                HUDInteractionToDo.instance.EnableInteractionText();
            }
        }
    }
}

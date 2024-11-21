using UnityEngine;
using UnityEngine.UIElements;

public class HUDKeys : MonoBehaviour
{
    public LevelManager02 levelManager;
    VisualElement root;
    VisualElement blueKey;
    VisualElement redKey;
    VisualElement whiteKey;

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        blueKey = root.Q<VisualElement>("BlueKey");
        redKey = root.Q<VisualElement>("RedKey");
        whiteKey = root.Q<VisualElement>("WhiteKey");

        blueKey.visible = false;
        redKey.visible = false;
        whiteKey.visible = false;
    }

    private void FixedUpdate()
    {
        if (levelManager.hasBlueKey)
        {
            blueKey.visible = true;
        }

        if (levelManager.hasRedKey)
        {
            redKey.visible = true;
        }

        if (levelManager.hasWhiteKey)
        {
            whiteKey.visible = true;
        }
    }

}

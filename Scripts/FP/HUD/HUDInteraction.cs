using UnityEngine;
using UnityEngine.UIElements;

public class HUDInteraction : MonoBehaviour
{
    public static HUDInteraction instance;
    private VisualElement root;
    private Label interactionText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        interactionText = root.Q<Label>("dicaIteracao");
        root.visible = false;
    }

    public void EnableInteractionText(string text)
    {
        root.visible = true;
        interactionText.text = text;
    }

    public void DisableInteractionText()
    {
        root.visible = false;
    }
}

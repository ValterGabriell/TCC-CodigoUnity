using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDInteraction : MonoBehaviour
{
    public static HUDInteraction instance;
    private VisualElement root;
    private Label interactionText;
    private Label msg;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        interactionText = root.Q<Label>("mensagemPrincipal");
        msg = root.Q<Label>("mensagemSecundaria");
        root.visible = false;
    }

    public void EnableInteractionText(string text,string _msg)
    {
        root.visible = true;
        interactionText.text = text;
        msg.text = _msg;
    }

  

    public void DisableInteractionText()
    {
        root.visible = false;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDInteractionToDo : MonoBehaviour
{
    public static HUDInteractionToDo instance;
    public VisualElement root;
    public GameManager gameManager;
    private Label goal;
    private Label problem;
    private Label msg;
   

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        goal = root.Q<Label>("objetivoFase");
        problem = root.Q<Label>("problemaFase");
        msg = root.Q<Label>("mensagemEducativa");
        root.visible = false;
    }

    public void EnableInteractionText()
    {
        goal.text = gameManager.currentLevel.Level_O1().MSG;
        problem.text = gameManager.currentLevel.Level_O1().EX;
        msg.text = gameManager.currentLevel.Level_O1().ED;
        root.visible = true;
    }

  

    public void DisableInteractionText()
    {
        root.visible = false;
    }
}

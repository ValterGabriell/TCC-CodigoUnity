using UnityEngine;
using UnityEngine.UIElements;

public class UIFinalLevel : MonoBehaviour
{
 
    private VisualElement root;
    public GameManager manager;
    public ModelTime _time;

    public PhaseType faseAtual = PhaseType.THIRD;
    public enum PhaseType
    {
        FIRST, SECOND, THIRD
    }

    Label time = null;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        root.visible = false;

        Button avancar = root.Q<Button>("nextPhase");
        time = root.Q<Label>("modelTime");
        Label fase = root.Q<Label>("faseAtual");
        fase.text = "Fase: " + faseAtual.ToString();

        avancar.clicked += () => {
            manager.CompleteLevel(root);
        };
    }

    private void FixedUpdate()
    {
        if (manager.isLevelCompleted)
        {
            root.visible = true;
            Time.timeScale = 0;
            Debug.Log(_time.timeElapsed);

            time.text = "Tempo: " + _time.timeElapsed;
        }
    }
}

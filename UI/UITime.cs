using UnityEngine;
using UnityEngine.UIElements;

public class UITime : MonoBehaviour
{
    private VisualElement root;
    public GameManager gameManager;
    private Label timeLabel;
    private Label pointsLabel;
    public ModelTime _time;
    public PointsModel _points;

   



    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        timeLabel = root.Q<Label>("tempoLabel");
        pointsLabel = root.Q<Label>("pointsLabel");
    }


    private void FixedUpdate()
    {
        timeLabel.text = _time.timeElapsed.ToString();
        pointsLabel.text = "Pontos: " + _points.GetCurrentPoints().ToString();

        if (gameManager.movimentosUI.Count > 0)
        {
            PopulateInterface();
        }
    }

    private void PopulateInterface()
    {

        // Itera sobre a fila de a��es
        int currentActionIndex = 0; // Indice para destacar a a��o atual
        foreach (string acao in gameManager.movimentosUI)
        {
            // Cria um cont�iner para cada a��o
            var actionContainer = new VisualElement
            {
                style =
            {
                flexDirection = FlexDirection.Row,
                alignItems = Align.Center,
                marginBottom = 5
            }
            };

            // Se for a a��o em execu��o, destaca ela
            if (currentActionIndex == 0)  // O �ndice '0' ser� a a��o atual
            {
                var highlightIcon = new VisualElement
                {
                    style =
                {
                    width = 16,
                    height = 16,
                    backgroundColor = new Color(1f, 0f, 0f), // Cor de destaque vermelha
                    marginRight = 10
                }
                };
                actionContainer.Add(highlightIcon); // Adiciona o �cone de destaque
            }

            // Cria o label para a a��o
            var actionLabel = new Label(acao)
            {
                style =
            {
                unityTextAlign = TextAnchor.MiddleLeft,
                fontSize = 14,
                color = new Color(0.2f, 0.2f, 0.2f)
            }
            };

            actionContainer.Add(actionLabel); // Adiciona o texto da a��o ao cont�iner


            // Atualiza o �ndice para a pr�xima a��o
            currentActionIndex++;
        }
    }
}



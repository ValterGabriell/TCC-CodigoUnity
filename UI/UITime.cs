using UnityEngine;
using UnityEngine.UIElements;

public class UITime : MonoBehaviour
{
    private VisualElement root;
    public GameManager gameManager;
    private Label timeLabel;
    private Label pointsLabel;
    private Button clearActions;
    public ModelTime _time;
    public PointsModel _points;
    private bool clearList = false;
    ScrollView actionsList;



    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        timeLabel = root.Q<Label>("timeLabel");
        pointsLabel = root.Q<Label>("pointsLabel");
        clearActions = root.Q<Button>("clearButton");

        clearActions.clicked += () => 
        {
            gameManager.ClearActions();
            clearList = true;
        };
    }


    private void FixedUpdate()
    {
        timeLabel.text = _time.timeElapsed;
        pointsLabel.text = "Pontos: " + _points.GetCurrentPoints().ToString();
       
        if (gameManager.movimentosUI.Count > 0)
        {
            PopulateInterface();
        }

        if (clearList)
        {
            actionsList.Clear();
            clearList = false;
        }
    }

    private void PopulateInterface()
    {
        actionsList = root.Q<ScrollView>("actionsList");
        actionsList.Clear();

        // Itera sobre a fila de ações
        int currentActionIndex = 0; // Indice para destacar a ação atual
        foreach (string acao in gameManager.movimentosUI)
        {
            // Cria um contêiner para cada ação
            var actionContainer = new VisualElement
            {
                style =
            {
                flexDirection = FlexDirection.Row,
                alignItems = Align.Center,
                marginBottom = 5
            }
            };

            // Se for a ação em execução, destaca ela
            if (currentActionIndex == 0)  // O índice '0' será a ação atual
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
                actionContainer.Add(highlightIcon); // Adiciona o ícone de destaque
            }

            // Cria o label para a ação
            var actionLabel = new Label(acao)
            {
                style =
            {
                unityTextAlign = TextAnchor.MiddleLeft,
                fontSize = 14,
                color = new Color(0.2f, 0.2f, 0.2f)
            }
            };

            actionContainer.Add(actionLabel); // Adiciona o texto da ação ao contêiner

            // Adiciona o contêiner completo (com ou sem o ícone de destaque) à lista
            actionsList.Add(actionContainer);

            // Atualiza o índice para a próxima ação
            currentActionIndex++;
        }
    }
}



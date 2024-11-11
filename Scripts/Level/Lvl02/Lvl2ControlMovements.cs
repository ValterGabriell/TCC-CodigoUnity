using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;

public class Lvl2Controle : MonoBehaviour
{
    private VisualElement root;

    public GameManager manager;
    public PointsModel PointsModel;
    // Lista para armazenar as a��es e condi��es
    private readonly List<string> actionsList = new List<string>();
    private readonly List<ConditionBlock> conditionBlocks = new List<ConditionBlock>();

    private bool hasAllIfBeenClosed = false;
    private bool ifWasOpened = false;

    public PlayerCollision playerCollision; 
    // Refer�ncia ao GameObject do jogador
    public Transform playerTransform;

    // Velocidade do movimento suave
    public float moveSpeed = 2f;
    public Level02 level02;


    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        // BOTOES DE MOVIMENTO
        Button left = root.Q<Button>("left");
        Button right = root.Q<Button>("right");
        Button forward = root.Q<Button>("forward");
        Button backward = root.Q<Button>("backward");

        // BOTOES CONDICIONAIS ESCOLHAS  
        Button pickKey = root.Q<Button>("pickKey");
        Button pickableArea = root.Q<Button>("pickableArea");

        // BOTOES CONDICOES
        Button btnIf = root.Q<Button>("if");
        Button btnEndIf = root.Q<Button>("end_if");
    

        // BOTOES START
        Button start = root.Q<Button>("start");


        // A��es de Movimento
        left.clicked += () => RecordAction("left");
        right.clicked += () => RecordAction("right");
        forward.clicked += () => RecordAction("forward");
        backward.clicked += () => RecordAction("backward");

        // Condi��es
        pickKey.clicked += () => RecordAction("pickKey");
        pickableArea.clicked += () => RecordAction("isOnPickableArea");

        // Controle de Estruturas de Controle
        btnIf.clicked += () => RecordAction("if");
        btnEndIf.clicked += () => RecordAction("end_if");
     

        // Iniciar Execu��o
        start.clicked += () => ExecuteActions();

    }

    // Fun��o para armazenar a��es de movimento
    private void RecordAction(string action)
    {
        if (action == "end_if")
        {
            hasAllIfBeenClosed = true;
        }else if (action == "end")
        {
            hasAllIfBeenClosed = false;
        }
        actionsList.Add(action);
    }


    // Fun��o para executar as a��es armazenadas
    private void ExecuteActions()
    {

        if (hasAllIfBeenClosed && ifWasOpened)
        {
            manager.isWalking = true;
            // Iterar sobre as a��es armazenadas e executar em sequ�ncia
            StartCoroutine(ExecuteActionsSequence());
        }
        else if (!ifWasOpened)
        {
            StartCoroutine(ExecuteActionsSequence());
        }
        else
        {
            Debug.LogWarning("FECHA OS IF");
        }
    }

    private IEnumerator ExecuteActionsSequence()
    {
        bool insideIf = false;  // Verifica se estamos dentro de um if
        List<string> currentIfActions = new(); // A��es armazenadas para o if
        bool wannExecSmthOnPickableArea = false;
        

        List<string> actionsCopy = new List<string>(actionsList); // Copia a lista original

        // Itera sobre as a��es para processar e armazenar as a��es para o while
        foreach (var action in actionsCopy)
        {
            // Se encontramos um "if", verificamos a condi��o
            if (action == "if")
            {
                insideIf = true;
                currentIfActions.Clear(); // Limpar a��es do if
                continue;  // Pula para a pr�xima itera��o
            }

            // Se encontramos um "end_if", executamos as a��es dentro do if
            if (action == "end_if" && insideIf)
            {
                insideIf = false;
                StartCoroutine(ExecuteIfActions(currentIfActions));
                continue;  // Pula para a pr�xima itera��o
            }

            // Se estamos dentro de um bloco if, armazena as a��es
            if (insideIf)
            {
                if (action == "isOnPickableArea")
                {
                    wannExecSmthOnPickableArea = true;
                    continue;  // Pula para a pr�xima itera��o    
                }
                if (wannExecSmthOnPickableArea == true)
                {
                    currentIfActions.Add(action);
                }
            }
            else
            {
                // Se chegamos aqui, n�o estamos em um bloco if ou while, ent�o executamos a a��o normalmente
                yield return StartCoroutine(ExecuteMovementSmooth(action));
            }
        }
        currentIfActions.Clear(); // Limpar a��es ap�s execu��o
        actionsList.Clear(); // Limpar a lista original
        manager.isWalking = false;
    }


    // Fun��o que executa as a��es dentro de um bloco if
    private IEnumerator ExecuteIfActions(List<string> actions)
    {
        if (playerCollision.canInteract)
        {
            // Criar uma c�pia da lista de a��es
            List<string> actionsCopy = new(actions);

            // Iterar sobre a c�pia da lista para evitar o erro de modifica��o durante a itera��o
            foreach (var action in actionsCopy)
            {
                Debug.Log(action);
                if (action == "pickKey")
                {
                    PointsModel.increasePoint(50);
                    level02.hasTheKey = true;
                    continue;
                }
                yield return StartCoroutine(ExecuteMovementSmooth(action));
            }
        }
    }


    private IEnumerator ExecuteMovementSmooth(string direction)
    {
        Vector3 startPosition = playerTransform.position;
        Vector3 targetPosition = startPosition;

        // Determinando a dire��o de movimento com base na a��o
        switch (direction)
        {
            case "left":
                targetPosition += Vector3.left;
                break;
            case "right":
                targetPosition += Vector3.right;
                break;
            case "forward":
                targetPosition += Vector3.forward;
                break;
            case "backward":
                targetPosition += Vector3.back;
                break;
        }
        float timeToMove = 1f / moveSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < timeToMove)
        {
            playerTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerTransform.position = targetPosition;
    }

    // Classe para armazenar as condi��es associadas a um bloco (if ou while)
    public class ConditionBlock
    {
        public string BlockType { get; private set; } // Tipo de bloco ("if" ou "while")
        public bool Condition { get; set; } // Condi��o associada

        public ConditionBlock(string blockType, bool condition)
        {
            BlockType = blockType;
            Condition = condition;
        }

        // Verifica se a condi��o do bloco � verdadeira
        public bool IsConditionTrue()
        {
            return Condition;
        }
    }
}

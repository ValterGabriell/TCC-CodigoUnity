using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Lvl03Control : MonoBehaviour
{
    private VisualElement root;


    // Lista para armazenar as a��es e condi��es
    private readonly List<string> actionsList = new();
    private readonly List<ConditionBlock> conditionBlocks = new();

    private bool hasAllWhileBeenClosed = false;

    public PlayerCollision playerCollision;
    // Refer�ncia ao GameObject do jogador
    public Transform playerTransform;

    // Velocidade do movimento suave
    public float moveSpeed = 2f;
    public PlayerRaycast raycast;
    public GameManager gameManager; 

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
        Button btnWhile = root.Q<Button>("while");
        Button btnEndWhile = root.Q<Button>("end_while");
        Button btnFreeWay = root.Q<Button>("freeWay");


        // BOTOES START
        Button start = root.Q<Button>("start");


        // A��es de Movimento
        left.clicked += () => RecordAction("left");
        right.clicked += () => RecordAction("right");
        forward.clicked += () => RecordAction("forward");
        backward.clicked += () => RecordAction("backward");

        // Controle de Estruturas de Controle
        btnWhile.clicked += () => RecordAction("while");
        btnEndWhile.clicked += () => RecordAction("end_while");
        btnFreeWay.clicked += () => RecordAction("freeWay");


        // Iniciar Execu��o
        start.clicked += () => ExecuteActions();

    }

    // Fun��o para armazenar a��es de movimento
    private void RecordAction(string action)
    {
        if (action == "end_while")
        {
            hasAllWhileBeenClosed = true;
        }
        else if (action == "end")
        {
            hasAllWhileBeenClosed = false;
        }
        actionsList.Add(action);
    }


    // Fun��o para executar as a��es armazenadas
    private void ExecuteActions()
    {
        if (hasAllWhileBeenClosed)
        {
            gameManager.isWalking = true;
            // Iterar sobre as a��es armazenadas e executar em sequ�ncia
            StartCoroutine(ExecuteActionsSequence());
        }
        else
        {
            Debug.LogWarning("PRECISA FECHAR OS WHILE");
        }

    }

    private IEnumerator ExecuteActionsSequence()
    {
        bool insideWhile = false;  // Verifica se estamos dentro de um if
        List<string> currentWhileActions = new(); // A��es armazenadas para o if
        bool isFreeWay = false;
        ConditionBlock currentConditionBlock = null; // Bloco atual de condi��o (if ou while)

        List<string> actionsCopy = new List<string>(actionsList); // Copia a lista original

        // Itera sobre as a��es para processar e armazenar as a��es para o while
        foreach (var action in actionsCopy)
        {
            // Se encontramos um "if", verificamos a condi��o
            if (action == "while")
            {
                insideWhile = true;
                currentWhileActions.Clear(); // Limpar a��es do while
                continue;  // Pula para a pr�xima itera��o
            }

            // Se encontramos um "end_while", executamos as a��es dentro do while
            if (action == "end_while" && insideWhile)
            {
                insideWhile = false;
                StartCoroutine(ExecuteWhileActions(currentWhileActions));
                continue;  // Pula para a pr�xima itera��o
            }

            // Se estamos dentro de um bloco if, armazena as a��es
            if (insideWhile)
            {
                if (action == "freeWay")
                {
                    continue;  
                }
                currentWhileActions.Add(action);
            }
            else
            {
                // Se chegamos aqui, n�o estamos em um bloco if ou while, ent�o executamos a a��o normalmente
                yield return StartCoroutine(ExecuteMovementSmooth(action));
            }
        }
        currentWhileActions.Clear(); // Limpar a��es ap�s execu��o
        actionsList.Clear(); // Limpar a lista original
        gameManager.isWalking = false;
    }


    // Fun��o que executa as a��es dentro de um bloco if
    private IEnumerator ExecuteWhileActions(List<string> actions)
    {  
        // Criar uma c�pia da lista de a��es
        List<string> actionsCopy = new(actions);

        while (!raycast.playerIsColliding)
        {
            // Iterar sobre a c�pia da lista para evitar o erro de modifica��o durante a itera��o
            foreach (var action in actionsCopy)
            {
                Debug.Log(action);
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

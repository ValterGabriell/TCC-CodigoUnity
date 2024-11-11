using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Lvl03Control : MonoBehaviour
{
    private VisualElement root;


    // Lista para armazenar as ações e condições
    private readonly List<string> actionsList = new();
    private readonly List<ConditionBlock> conditionBlocks = new();

    private bool hasAllWhileBeenClosed = false;
    private bool whileHasActive = false;
    private bool foundWhileAction = false;

    public PlayerCollision playerCollision;
    // Referência ao GameObject do jogador
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


        // Ações de Movimento
        left.clicked += () => RecordAction("left");
        right.clicked += () => RecordAction("right");
        forward.clicked += () => RecordAction("forward");
        backward.clicked += () => RecordAction("backward");

        // Controle de Estruturas de Controle
        btnWhile.clicked += () => RecordAction("while");
        btnEndWhile.clicked += () => RecordAction("end_while");
        btnFreeWay.clicked += () => RecordAction("freeWay");


        // Iniciar Execução
        start.clicked += () => ExecuteActions();

    }

    // Função para armazenar ações de movimento
    private void RecordAction(string action)
    {
        if (action == "end_while")
        {
            hasAllWhileBeenClosed = true;
            whileHasActive = false;
        }
        else if (action == "while")
        {
            foundWhileAction = true;
            whileHasActive = true;
        }
        actionsList.Add(action);
    }


    // Função para executar as ações armazenadas
    private void ExecuteActions()
    {
        if (whileHasActive)
        {
            if (hasAllWhileBeenClosed)
            {
                gameManager.isWalking = true;
                // Iterar sobre as ações armazenadas e executar em sequência
                StartCoroutine(ExecuteActionsSequence());
            }
            else
            {
                Debug.LogWarning("PRECISA FECHAR OS WHILE");
            }
        }
        else if (!foundWhileAction)
        {
            Debug.LogWarning("PRECISA TER OS WHILE");
        }
        else
        {
            gameManager.isWalking = true;
            // Iterar sobre as ações armazenadas e executar em sequência
            StartCoroutine(ExecuteActionsSequence());
        }


    }

    private IEnumerator ExecuteActionsSequence()
    {
        bool insideWhile = false;  // Verifica se estamos dentro de um if
        List<string> currentWhileActions = new(); // Ações armazenadas para o if
        bool isFreeWay = false;
        

        List<string> actionsCopy = new List<string>(actionsList); // Copia a lista original

        // Itera sobre as ações para processar e armazenar as ações para o while
        foreach (var action in actionsCopy)
        {
            // Se encontramos um "if", verificamos a condição
            if (action == "while")
            {
                foundWhileAction= true;
                insideWhile = true;
                currentWhileActions.Clear(); // Limpar ações do while
                continue;  // Pula para a próxima iteração
            }

            // Se encontramos um "end_while", executamos as ações dentro do while
            if (action == "end_while" && insideWhile)
            {
                insideWhile = false;
                StartCoroutine(ExecuteWhileActions(currentWhileActions));
                continue;  // Pula para a próxima iteração
            }

            // Se estamos dentro de um bloco if, armazena as ações
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
                // Se chegamos aqui, não estamos em um bloco if ou while, então executamos a ação normalmente
                yield return StartCoroutine(ExecuteMovementSmooth(action));
            }
        }
        currentWhileActions.Clear(); // Limpar ações após execução
        actionsList.Clear(); // Limpar a lista original
        gameManager.isWalking = false;
    }


    // Função que executa as ações dentro de um bloco if
    private IEnumerator ExecuteWhileActions(List<string> actions)
    {  
        // Criar uma cópia da lista de ações
        List<string> actionsCopy = new(actions);

        while (!raycast.playerIsColliding)
        {
            // Iterar sobre a cópia da lista para evitar o erro de modificação durante a iteração
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

        // Determinando a direção de movimento com base na ação
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

    // Classe para armazenar as condições associadas a um bloco (if ou while)
    public class ConditionBlock
    {
        public string BlockType { get; private set; } // Tipo de bloco ("if" ou "while")
        public bool Condition { get; set; } // Condição associada

        public ConditionBlock(string blockType, bool condition)
        {
            BlockType = blockType;
            Condition = condition;
        }

        // Verifica se a condição do bloco é verdadeira
        public bool IsConditionTrue()
        {
            return Condition;
        }
    }
}

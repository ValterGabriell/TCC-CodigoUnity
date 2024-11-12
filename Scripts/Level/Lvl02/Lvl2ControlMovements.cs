using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;

public class Lvl2Controle : MonoBehaviour
{
    private VisualElement root;

    public GameManager gameManager;
    public PointsModel PointsModel;
    // Lista para armazenar as ações e condições
    private readonly List<string> actionsList = new List<string>();
    private readonly List<ConditionBlock> conditionBlocks = new List<ConditionBlock>();

    private bool hasAllIfBeenClosed = false;
    private bool ifWasOpened = false;

    public PlayerCollision playerCollision; 
    // Referência ao GameObject do jogador
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


        // Ações de Movimento
        left.clicked += () => RecordAction("left");
        right.clicked += () => RecordAction("right");
        forward.clicked += () => RecordAction("forward");
        backward.clicked += () => RecordAction("backward");

        // Condições
        pickKey.clicked += () => RecordAction("pickKey");
        pickableArea.clicked += () => RecordAction("isOnPickableArea");

        // Controle de Estruturas de Controle
        btnIf.clicked += () => RecordAction("if");
        btnEndIf.clicked += () => RecordAction("end_if");
     

        // Iniciar Execução
        start.clicked += () => ExecuteActions();

    }

    // Função para armazenar ações de movimento
    private void RecordAction(string action)
    {
        gameManager.movimentosUI.Enqueue(action);
        //manager.movimentosUI.Add(";");
        if (action == "end_if")
        {
            hasAllIfBeenClosed = true;
        }else if (action == "end")
        {
            hasAllIfBeenClosed = false;
        }
        actionsList.Add(action);
    }


    // Função para executar as ações armazenadas
    private void ExecuteActions()
    {

        if (hasAllIfBeenClosed && ifWasOpened)
        {
            gameManager.isWalking = true;
            // Iterar sobre as ações armazenadas e executar em sequência
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
        List<string> currentIfActions = new(); // Ações armazenadas para o if
        bool wannExecSmthOnPickableArea = false;
        

        List<string> actionsCopy = new List<string>(actionsList); // Copia a lista original

        // Itera sobre as ações para processar e armazenar as ações para o while
        foreach (var action in actionsCopy)
        {
            // Se encontramos um "if", verificamos a condição
            if (action == "if")
            {
                insideIf = true;
                currentIfActions.Clear(); // Limpar ações do if
                continue;  // Pula para a próxima iteração
            }

            // Se encontramos um "end_if", executamos as ações dentro do if
            if (action == "end_if" && insideIf)
            {
                insideIf = false;
                StartCoroutine(ExecuteIfActions(currentIfActions));
                continue;  // Pula para a próxima iteração
            }

            // Se estamos dentro de um bloco if, armazena as ações
            if (insideIf)
            {
                if (action == "isOnPickableArea")
                {
                    wannExecSmthOnPickableArea = true;
                    continue;  // Pula para a próxima iteração    
                }
                if (wannExecSmthOnPickableArea == true)
                {
                    currentIfActions.Add(action);
                }
            }
            else
            {
                // Se chegamos aqui, não estamos em um bloco if ou while, então executamos a ação normalmente
                yield return StartCoroutine(ExecuteMovementSmooth(action));
            }

            //remove o item da pilha
            gameManager.movimentosUI.Dequeue();


        }
        gameManager.movimentosUI.Clear();
        currentIfActions.Clear(); // Limpar ações após execução
        actionsList.Clear(); // Limpar a lista original
        gameManager.isWalking = false;
    }


    // Função que executa as ações dentro de um bloco if
    private IEnumerator ExecuteIfActions(List<string> actions)
    {
        if (playerCollision.canInteract)
        {
            // Criar uma cópia da lista de ações
            List<string> actionsCopy = new(actions);

            // Iterar sobre a cópia da lista para evitar o erro de modificação durante a iteração
            foreach (var action in actionsCopy)
            {
                if (action == "pickKey")
                {
                    var obj = GameObject.Find("rust_key");
                    if (obj != null)
                    {
                        Destroy(obj);
                    }
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

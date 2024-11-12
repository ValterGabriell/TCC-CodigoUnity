using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class ControleJogo : MonoBehaviour
{

    public GameManager gameManager;
    private VisualElement root;

    //define o level
    public int currentLevel = 1;

    // Vari�veis para controlar o estado do jogador e das condi��es
    private bool hasKey = false;
    private bool isFreeway = false;
    private bool playerHasCollide = false;

    // Representando a posi��o do jogador
    private Vector3 playerPosition = Vector3.zero;

    // Lista para armazenar as a��es e condi��es
    private List<string> actionsList = new List<string>();
    private List<ConditionBlock> conditionBlocks = new List<ConditionBlock>();

    

    // Refer�ncia ao GameObject do jogador
    public Transform playerTransform;

    // Velocidade do movimento suave
    public float moveSpeed = 2f;

    private void FixedUpdate()
    {
        if (gameManager.isLevelCompleted)
        {
            root.visible = false;
        }
    }

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        // BOTOES DE MOVIMENTO
        Button left = root.Q<Button>("left");
        Button right = root.Q<Button>("right");
        Button forward = root.Q<Button>("forward");
        Button backward = root.Q<Button>("backward");

        // BOTOES CONDICIONAIS ESCOLHAS
        Button conditionFreeWay = root.Q<Button>("freeWay");
        Button conditionhasKey = root.Q<Button>("hasKey");
        Button pickableArea = root.Q<Button>("pickableArea");

        // BOTOES CONDICOES
        Button btnIf = root.Q<Button>("if");
        Button btnElse = root.Q<Button>("else");
        Button btnEndIf = root.Q<Button>("end_if");
        Button btnEndWhile = root.Q<Button>("end_while");
        Button btnWhile = root.Q<Button>("while");

        // BOTOES START
        Button start = root.Q<Button>("start");


        // A��es de Movimento
        left.clicked += () => RecordAction("left");
        right.clicked += () => RecordAction("right");
        forward.clicked += () => RecordAction("forward");
        backward.clicked += () => RecordAction("backward");

        // Condi��es
        //conditionFreeWay.clicked += () => RecordAction("freeWay");
        conditionhasKey.clicked += () => RecordAction("hasKey");
        pickableArea.clicked += () => RecordAction("isOnPickableArea");

        // Controle de Estruturas de Controle
        btnIf.clicked += () => RecordAction("if");
        btnEndIf.clicked += () => RecordAction("end_if");
        btnEndWhile.clicked += () => RecordAction("end_while");
        btnWhile.clicked += () => RecordAction("while");

        // Iniciar Execu��o
        start.clicked += () => ExecuteActions();

        DisableButtons(new List<Button> { left,right,forward,backward, btnIf ,btnElse,btnEndIf,btnEndWhile,btnWhile, conditionFreeWay, pickableArea, conditionhasKey });

        List<Button> buttons = new();
        switch (currentLevel)
        {
            case 1:
                buttons.Add(left);
                buttons.Add(right);
                buttons.Add(forward);
                buttons.Add(backward);
                DefineVisibilityOfButtonsByLevel(buttons);
                break;
            case 2:
                buttons.Add(left);
                buttons.Add(right);
                buttons.Add(forward);
                buttons.Add(backward);
                buttons.Add(btnIf);
                buttons.Add(btnEndIf);
                buttons.Add(pickableArea);


                DefineVisibilityOfButtonsByLevel(buttons);
                break;

        }
    }

    private static void DisableButtons(List<Button> buttons)
    {
        foreach (var item in buttons)
        {
            item.visible = false;
        }
    }

    // Fun��o para armazenar a��es de movimento
    private void RecordAction(string action)
    {
        gameManager.movimentosUI.Enqueue(action);
        //Manager.movimentosUI.Add(";");
        actionsList.Add(action);
    }

    // Fun��o para armazenar condi��es (como 'if', 'while', 'freeWay', etc)
    private void RecordCondition(string condition)
    {
        if (condition == "hasKey")
        {
            hasKey = true;
        }
        if (condition == "freeWay")
        {
            isFreeway = true;
        }
       
    }

    // Fun��o para executar as a��es armazenadas
    private void ExecuteActions()
    {
        gameManager.isWalking = true;
        // Iterar sobre as a��es armazenadas e executar em sequ�ncia
        StartCoroutine(ExecuteActionsSequence());
    }

    private IEnumerator ExecuteActionsSequence()
    {
        bool insideWhile = false;  // Verifica se estamos dentro de um while
        bool insideIf = false;  // Verifica se estamos dentro de um if
        List<string> currentIfActions = new List<string>(); // A��es armazenadas para o if
        List<string> currentWhileActions = new List<string>(); // A��es armazenadas para o while
        ConditionBlock currentConditionBlock = null; // Bloco atual de condi��o (if ou while)
        bool foundEndWhile = false; // Flag para saber se encontramos "end_while"

        // Itera sobre as a��es para processar e armazenar as a��es para o while
        foreach (var action in actionsList)
        {
            // Verifica se estamos em um bloco while
            if (action == "while")
            {
                insideWhile = true;
                currentWhileActions.Clear(); // Limpar a��es do while

                // Defina a condi��o para o while (chamando GetConditionForWhile)
                currentConditionBlock = GetConditionForWhile();
                
                continue;  // Pula para a pr�xima itera��o
            }

            // Se encontramos um "end_while", apenas marca que encontrou, mas n�o sai do while ainda
            if (action == "end_while" && insideWhile)
            {
                foundEndWhile = true;
                continue;  // Pula para a pr�xima itera��o
            }

            // Se estamos dentro de um bloco while, armazena as a��es
            if (insideWhile)
            {
                currentWhileActions.Add(action);
                continue;  // Pula para a pr�xima itera��o
            }

            // Se encontramos um "if", verificamos a condi��o
            if (action == "if")
            {
                insideIf = true;
                currentIfActions.Clear(); // Limpar a��es do if
                currentConditionBlock = new ConditionBlock("if", hasKey); // Condi��o associada ao if
                continue;  // Pula para a pr�xima itera��o
            }

            // Se encontramos um "end_if", executamos as a��es dentro do if
            if (action == "end_if" && insideIf)
            {
                insideIf = false;
                ExecuteIfBlock(currentIfActions, currentConditionBlock);  // Executa as a��es do bloco if
                currentIfActions.Clear(); // Limpar a��es ap�s execu��o
                continue;  // Pula para a pr�xima itera��o
            }

            // Se estamos dentro de um bloco if, armazena as a��es
            if (insideIf)
            {
                currentIfActions.Add(action);
                continue;  // Pula para a pr�xima itera��o
            }
            //remove o item da pilha
            gameManager.movimentosUI.Dequeue();
            // Se chegamos aqui, n�o estamos em um bloco if ou while, ent�o executamos a a��o normalmente
            yield return StartCoroutine(ExecuteMovementSmooth(action));
        }
        // Se chegamos ao fim do loop e encontramos um "end_while", chamamos ExecuteWhileActions
        if (insideWhile && foundEndWhile)
        {
            // Chama ExecuteWhileActions para executar o loop enquanto a condi��o for verdadeira
            yield return StartCoroutine(ExecuteWhileActions(currentWhileActions, currentConditionBlock));
        }
        actionsList.Clear();
        gameManager.isWalking = false;
        gameManager.movimentosUI.Clear();
    }
    private ConditionBlock GetConditionForWhile()
    {
        // Exemplo de como voc� pode definir condi��es para o while:
        // Aqui voc� pode verificar algo na lista de condi��es, ou outras vari�veis.

        // Aqui eu fa�o uma verifica��o de exemplo, mas voc� pode personalizar isso
        if (actionsList.Contains("freeWay"))
        {
            isFreeway = true;
            return new ConditionBlock("while", isFreeway);  // Retorna a condi��o de freeWay
        }

        // Outra condi��o, dependendo de como voc� quiser definir as regras para o "while"
        if (actionsList.Contains("hasKey"))
        {
            hasKey = true; 
            return new ConditionBlock("while", hasKey);  // Retorna a condi��o de hasKey
        }

        // Condi��o padr�o (caso n�o tenha uma condi��o espec�fica)
        return new ConditionBlock("while", false);  // Executa sempre, como uma condi��o verdadeira
    }


    // Exemplo de m�todo que verifica a condi��o do while
    public bool IsConditionTrue()
    {
        // A condi��o de exemplo poderia ser algo assim:
        // Se o caminho estiver livre e o jogador puder andar
        return isFreeway && !playerHasCollide && hasKey;
    }

    private IEnumerator ExecuteWhileActions(List<string> actions, ConditionBlock block)
    {
        // Definindo um contador de tentativas para evitar loop infinito
        int attemptCount = 0;
        int maxAttempts = 100; // N�mero m�ximo de tentativas antes de interromper o loop

        bool insideIf = false; // Flag para verificar se estamos dentro de um bloco 'if'
        List<string> currentIfActions = new List<string>(); // A��es armazenadas do bloco 'if'

        // Continua o loop enquanto a condi��o do while for verdadeira
        while (block.IsConditionTrue())
        {
            // Verifique se a condi��o do while est� sendo atendida
           

            // Verifica o n�mero de tentativas para evitar loop infinito
            attemptCount++;
            if (attemptCount == 3)
            {
                isFreeway = false;
                block.Condition = isFreeway;
            }
            if (attemptCount > maxAttempts)
            {

                break; // Interrompe o loop se atingir o limite
            }

            // **Verificar se h� um bloco 'if' dentro do 'while'**
            if (insideIf)
            {
                // Se a condi��o do if for verdadeira, executamos as a��es dentro do if
              
                foreach (var action in currentIfActions)
                {
              
                    yield return StartCoroutine(ExecuteMovementSmooth(action));
                }
            }

            // Aguarda o pr�ximo quadro para evitar que o loop bloqueie o jogo
            yield return null; // Aguarda o pr�ximo frame
        }

      
    }

  



    // Fun��o que executa as a��es de um bloco if
    private void ExecuteIfBlock(List<string> actions, ConditionBlock block)
    {
        // Se a condi��o do if for verdadeira, executa as a��es
        if (block.IsConditionTrue())
        {
            StartCoroutine(ExecuteIfActions(actions));
        }
    }

    // Fun��o que executa as a��es dentro de um bloco if
    private IEnumerator ExecuteIfActions(List<string> actions)
    {
        foreach (var action in actions)
        {
            yield return StartCoroutine(ExecuteMovementSmooth(action));
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


    private void DefineVisibilityOfButtonsByLevel(List<Button> buttons)
    {
        foreach (var item in buttons)
        {
            item.visible = true;
        }
    }
}

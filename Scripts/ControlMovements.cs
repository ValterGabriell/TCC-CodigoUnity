using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;

public class ControleJogo : MonoBehaviour
{
    private VisualElement root;

    //define o level
    public int currentLevel = 1;

    // Variáveis para controlar o estado do jogador e das condições
    private bool hasKey = false;
    private bool isFreeway = false;
    private bool playerHasCollide = false;

    // Representando a posição do jogador
    private Vector3 playerPosition = Vector3.zero;

    // Lista para armazenar as ações e condições
    private List<string> actionsList = new List<string>();
    private List<ConditionBlock> conditionBlocks = new List<ConditionBlock>();

    

    // Referência ao GameObject do jogador
    public Transform playerTransform;

    // Velocidade do movimento suave
    public float moveSpeed = 2f;


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


        // Ações de Movimento
        left.clicked += () => RecordAction("left");
        right.clicked += () => RecordAction("right");
        forward.clicked += () => RecordAction("forward");
        backward.clicked += () => RecordAction("backward");

        // Condições
        //conditionFreeWay.clicked += () => RecordAction("freeWay");
        conditionhasKey.clicked += () => RecordAction("hasKey");
        pickableArea.clicked += () => RecordAction("isOnPickableArea");

        // Controle de Estruturas de Controle
        btnIf.clicked += () => RecordAction("if");
        btnElse.clicked += () => RecordCondition("else");
        btnEndIf.clicked += () => RecordAction("end_if");
        btnEndWhile.clicked += () => RecordAction("end_while");
        btnWhile.clicked += () => RecordAction("while");

        // Iniciar Execução
        start.clicked += () => ExecuteActions();

        DisableButtons(new List<Button> { left,right,forward,backward, btnIf ,btnElse,btnEndIf,btnEndWhile,btnWhile, conditionFreeWay, conditionhasKey});

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

    // Função para armazenar ações de movimento
    private void RecordAction(string action)
    {
        Debug.Log("Armazenando ação: " + action);
        actionsList.Add(action);
    }

    // Função para armazenar condições (como 'if', 'while', 'freeWay', etc)
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
        Debug.Log("Armazenando condição: " + condition);  // Verifique o valor de isFreeway aqui
    }

    // Função para executar as ações armazenadas
    private void ExecuteActions()
    {
        Debug.Log("Iniciando Execução...");

        // Iterar sobre as ações armazenadas e executar em sequência
        StartCoroutine(ExecuteActionsSequence());
    }

    private IEnumerator ExecuteActionsSequence()
    {
        bool insideWhile = false;  // Verifica se estamos dentro de um while
        bool insideIf = false;  // Verifica se estamos dentro de um if
        List<string> currentIfActions = new List<string>(); // Ações armazenadas para o if
        List<string> currentWhileActions = new List<string>(); // Ações armazenadas para o while
        ConditionBlock currentConditionBlock = null; // Bloco atual de condição (if ou while)
        bool foundEndWhile = false; // Flag para saber se encontramos "end_while"

        // Itera sobre as ações para processar e armazenar as ações para o while
        foreach (var action in actionsList)
        {
            // Verifica se estamos em um bloco while
            if (action == "while")
            {
                insideWhile = true;
                currentWhileActions.Clear(); // Limpar ações do while

                // Defina a condição para o while (chamando GetConditionForWhile)
                currentConditionBlock = GetConditionForWhile();
                Debug.Log("Iniciando bloco while com condição: " + currentConditionBlock.Condition);
                continue;  // Pula para a próxima iteração
            }

            // Se encontramos um "end_while", apenas marca que encontrou, mas não sai do while ainda
            if (action == "end_while" && insideWhile)
            {
                foundEndWhile = true;
                continue;  // Pula para a próxima iteração
            }

            // Se estamos dentro de um bloco while, armazena as ações
            if (insideWhile)
            {
                currentWhileActions.Add(action);
                continue;  // Pula para a próxima iteração
            }

            // Se encontramos um "if", verificamos a condição
            if (action == "if")
            {
                insideIf = true;
                currentIfActions.Clear(); // Limpar ações do if
                currentConditionBlock = new ConditionBlock("if", hasKey); // Condição associada ao if
                continue;  // Pula para a próxima iteração
            }

            // Se encontramos um "end_if", executamos as ações dentro do if
            if (action == "end_if" && insideIf)
            {
                insideIf = false;
                ExecuteIfBlock(currentIfActions, currentConditionBlock);  // Executa as ações do bloco if
                currentIfActions.Clear(); // Limpar ações após execução
                continue;  // Pula para a próxima iteração
            }

            // Se estamos dentro de um bloco if, armazena as ações
            if (insideIf)
            {
                currentIfActions.Add(action);
                continue;  // Pula para a próxima iteração
            }

            // Se chegamos aqui, não estamos em um bloco if ou while, então executamos a ação normalmente
            yield return StartCoroutine(ExecuteMovementSmooth(action));
        }
        // Se chegamos ao fim do loop e encontramos um "end_while", chamamos ExecuteWhileActions
        if (insideWhile && foundEndWhile)
        {
            // Chama ExecuteWhileActions para executar o loop enquanto a condição for verdadeira
            yield return StartCoroutine(ExecuteWhileActions(currentWhileActions, currentConditionBlock));
        }
        actionsList.Clear();
    }
    private ConditionBlock GetConditionForWhile()
    {
        // Exemplo de como você pode definir condições para o while:
        // Aqui você pode verificar algo na lista de condições, ou outras variáveis.

        // Aqui eu faço uma verificação de exemplo, mas você pode personalizar isso
        if (actionsList.Contains("freeWay"))
        {
            isFreeway = true;
            return new ConditionBlock("while", isFreeway);  // Retorna a condição de freeWay
        }

        // Outra condição, dependendo de como você quiser definir as regras para o "while"
        if (actionsList.Contains("hasKey"))
        {
            hasKey = true; 
            return new ConditionBlock("while", hasKey);  // Retorna a condição de hasKey
        }

        // Condição padrão (caso não tenha uma condição específica)
        return new ConditionBlock("while", false);  // Executa sempre, como uma condição verdadeira
    }


    // Exemplo de método que verifica a condição do while
    public bool IsConditionTrue()
    {
        // A condição de exemplo poderia ser algo assim:
        // Se o caminho estiver livre e o jogador puder andar
        return isFreeway && !playerHasCollide && hasKey;
    }

    private IEnumerator ExecuteWhileActions(List<string> actions, ConditionBlock block)
    {
        // Definindo um contador de tentativas para evitar loop infinito
        int attemptCount = 0;
        int maxAttempts = 100; // Número máximo de tentativas antes de interromper o loop

        bool insideIf = false; // Flag para verificar se estamos dentro de um bloco 'if'
        List<string> currentIfActions = new List<string>(); // Ações armazenadas do bloco 'if'

        // Continua o loop enquanto a condição do while for verdadeira
        while (block.IsConditionTrue())
        {
            // Verifique se a condição do while está sendo atendida
            Debug.Log("Condição do while é verdadeira. Executando ações...");

            // Verifica o número de tentativas para evitar loop infinito
            attemptCount++;
            if (attemptCount == 3)
            {
                isFreeway = false;
                block.Condition = isFreeway;
            }
            if (attemptCount > maxAttempts)
            {
                Debug.LogWarning("Loop while atingiu o limite de tentativas.");
                break; // Interrompe o loop se atingir o limite
            }

            // **Verificar se há um bloco 'if' dentro do 'while'**
            if (insideIf)
            {
                // Se a condição do if for verdadeira, executamos as ações dentro do if
                Debug.Log("Executando ações do IF dentro do While");
                foreach (var action in currentIfActions)
                {
                    Debug.Log("Ação do IF: " + action);
                    yield return StartCoroutine(ExecuteMovementSmooth(action));
                }
            }

            // Executa as ações dentro do bloco while
            foreach (var action in actions)
            {
                Debug.Log("Executando ação do While: " + action); // Verificar qual ação está sendo executada
                yield return StartCoroutine(ExecuteMovementSmooth(action));
            }
            // Aguarda o próximo quadro para evitar que o loop bloqueie o jogo
            yield return null; // Aguarda o próximo frame
        }

        Debug.Log("Saindo do loop while.");
    }

  



    // Função que executa as ações de um bloco if
    private void ExecuteIfBlock(List<string> actions, ConditionBlock block)
    {
        // Se a condição do if for verdadeira, executa as ações
        if (block.IsConditionTrue())
        {
            StartCoroutine(ExecuteIfActions(actions));
        }
    }

    // Função que executa as ações dentro de um bloco if
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

        Debug.Log("Movendo para: " + targetPosition);  // Verificar se a posição de destino está sendo calculada corretamente

        float timeToMove = 1f / moveSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < timeToMove)
        {
            playerTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerTransform.position = targetPosition;
        Debug.Log("Movimento concluído para: " + targetPosition);  // Verificar se o movimento terminou corretamente
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


    private void DefineVisibilityOfButtonsByLevel(List<Button> buttons)
    {
        foreach (var item in buttons)
        {
            item.visible = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    public bool isWalking = false;
    public bool isLevelCompleted = false;

    // Gerenciamento de cenas (níveis)
    public void CompleteLevel(VisualElement root)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        isLevelCompleted = false;
        root.visible = false;
    }

    // Enum para os tipos de movimento
    public enum MovementType
    {
        LEFT, RIGHT, FORWARD, BACKWARD
    }

    // Enum para as condições de movimento
    public enum MovementConditions
    {
        FREE_WAY, HAS_COLLIDE_WITH_OBSTACLE
    }

    // Enum para as condições do "SE"
    public enum IFConditions
    {
        OBSTACLE
    }

    // Controle de estrutura de blocos (SE, ENQUANTO)
    public enum ControlClick
    {
        IF, WHILE
    }

    // Enum para os movimentos concluídos
    public enum MoveComplete
    {
        LEFT, RIGHT, FORWARD, BACKWARD, IF, ELSE, WHILE, END_IF, END_WHILE, FREE_WAY, HAS_COLLIDE_WITH_OBSTACLE, IDLE
    }

    // Estado do jogo
    public bool gameIsRunning = false;

    // Fila de movimentos e ações
    public Queue<MovementType> movementTypes = new();
    public Queue<MoveComplete> AllMovements = new();

    // Dicionários de condições
    public Dictionary<MovementConditions, bool> whileCondition = new();
    public Dictionary<IFConditions, bool> ifCondition = new();


    public Queue<string> movimentosUI = new();


    

    // Funções para verificar condições
    public bool IsFreeWay() => whileCondition.ContainsKey(MovementConditions.FREE_WAY) && whileCondition[MovementConditions.FREE_WAY];
    public bool HasObstacle() => ifCondition.ContainsKey(IFConditions.OBSTACLE) && ifCondition[IFConditions.OBSTACLE];
}

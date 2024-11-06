using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Gerenciamento de cenas (n�veis)
    public void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Enum para os tipos de movimento
    public enum MovementType
    {
        LEFT, RIGHT, FORWARD, BACKWARD
    }

    // Enum para as condi��es de movimento
    public enum MovementConditions
    {
        FREE_WAY, HAS_COLLIDE_WITH_OBSTACLE
    }

    // Enum para as condi��es do "SE"
    public enum IFConditions
    {
        OBSTACLE
    }

    // Controle de estrutura de blocos (SE, ENQUANTO)
    public enum ControlClick
    {
        IF, WHILE
    }

    // Enum para os movimentos conclu�dos
    public enum MoveComplete
    {
        LEFT, RIGHT, FORWARD, BACKWARD, IF, ELSE, WHILE, END_IF, END_WHILE, FREE_WAY, HAS_COLLIDE_WITH_OBSTACLE, IDLE
    }

    // Estado do jogo
    public bool gameIsRunning = false;

    // Fila de movimentos e a��es
    public Queue<MovementType> movementTypes = new();
    public Queue<MoveComplete> AllMovements = new();

    // Dicion�rios de condi��es
    public Dictionary<MovementConditions, bool> whileCondition = new();
    public Dictionary<IFConditions, bool> ifCondition = new();

    // Fun��es para verificar condi��es
    public bool IsFreeWay() => whileCondition.ContainsKey(MovementConditions.FREE_WAY) && whileCondition[MovementConditions.FREE_WAY];
    public bool HasObstacle() => ifCondition.ContainsKey(IFConditions.OBSTACLE) && ifCondition[IFConditions.OBSTACLE];



}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public enum MovementType
    {
        LEFT, RIGHT, FORWARD, BACKWARD
    }
    public enum MovementConditions
    {
        FREE_WAY
    }

    public enum IFConditions
    {
        OBSTACLE
    }

    public enum MoveComplete
    {
        LEFT, RIGHT, FORWARD, BACKWARD, IF, ELSE, WHILE, END_IF, END_WHILE, FREE_WAY, OBSTACLE,IDLE
    }

    public enum MoveListType
    {
        NORMAL, IF, WHILE
    }

    public Queue<MovementType> movementTypes = new();
    public Queue<MoveComplete> AllMovements = new();


    public Dictionary<MovementConditions, bool> whileCondition = new();
    public Dictionary<IFConditions, bool> ifCondition = new();
}

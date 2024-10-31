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

    public Queue<MovementType> movementTypes = new Queue<MovementType>();
    public Dictionary<MovementConditions, bool> whileCondition = new Dictionary<MovementConditions, bool>();
    public Dictionary<IFConditions, bool> ifCondition = new Dictionary<IFConditions, bool>();

}

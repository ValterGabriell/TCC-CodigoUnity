using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameManager;

public class PlayerMove : MonoBehaviour
{
    public GameManager manager;
    public Rigidbody rb;
    public float walkForce = 5f;
    public float jumpForce = 10f;
    public bool isGrounded = true;
    private bool isMoving = true;
    private bool isObstacleIf = false;
    private bool whileIsOk = false;

    public void MoveLeft() => manager.movementTypes.Enqueue(MovementType.LEFT);
    public void MoveRight() => manager.movementTypes.Enqueue(MovementType.RIGHT);
    public void MoveForward() => manager.movementTypes.Enqueue(MovementType.FORWARD);
    public void MoveBackward() => manager.movementTypes.Enqueue(MovementType.BACKWARD);

    public void WhileCondition(MovementConditions condition)
    {
        if (!manager.whileCondition.ContainsKey(condition))
            manager.whileCondition.Add(condition, true);
    }

    public void SetIfCondition(IFConditions condition)
    {
        if (!manager.ifCondition.ContainsKey(condition))
            manager.ifCondition.Add(condition, true);
    }

  


    public IEnumerator StartMovements()
    {
        if (!isMoving)
        {
            isMoving = true;
        }

        while (isMoving)
        {
            yield return HandleMovePlayer();
        }
    }
    private IEnumerator HandleMovePlayer()
    {
        var has = manager.whileCondition.GetValueOrDefault(MovementConditions.FREE_WAY, false);
        if (has)
        {
            while (has)
            {
                foreach (var currentMove in manager.movementTypes)
                {
                    yield return SwitchMovementHandle(currentMove);
                }
            }
        }
        if (manager.movementTypes.Count != 0)
        {
            var currentMove = manager.movementTypes.Dequeue();
            yield return SwitchMovementHandle(currentMove);
        }
        else
        {
            isMoving = false;
        }
    }

    private IEnumerator SwitchMovementHandle(MovementType movementType)
    {
        //lida com o IF
        if (isObstacleIf)
        {
            movementType = MovementType.LEFT;
            isObstacleIf = false;
        }
        switch (movementType)
        {
            case MovementType.LEFT:
                yield return MoveCharacter(Vector3.left * walkForce);
                break;
            case MovementType.RIGHT:
                yield return MoveCharacter(Vector3.right * walkForce);
                break;
            case MovementType.FORWARD:
                yield return MoveCharacter(Vector3.forward * walkForce);
                break;
            case MovementType.BACKWARD:
                yield return MoveCharacter(Vector3.back * walkForce);
                break;
        }
    }

    private IEnumerator MoveCharacter(Vector3 direction)
    {
        rb.linearVelocity = direction;
        yield return new WaitForSeconds(0.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.collider.CompareTag("Obstacle"))
        {
            if (manager.ifCondition.GetValueOrDefault(IFConditions.OBSTACLE, false))
            {
                isObstacleIf = true;
                manager.ifCondition[IFConditions.OBSTACLE] = false;

            }
            else if (manager.whileCondition.GetValueOrDefault(MovementConditions.FREE_WAY,false))
            {
                whileIsOk = manager.whileCondition.GetValueOrDefault(MovementConditions.FREE_WAY, false);
                manager.whileCondition[MovementConditions.FREE_WAY] = false;
            }
            else
            {
                manager.movementTypes.Clear();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;
using static UnityEditor.Progress;

public class PlayerMove : MonoBehaviour
{
    public GameManager manager;
    public Rigidbody rb;
    public float walkForce = 6f;
    public float jumpForce = 10f;
    public bool isGrounded = true;
    private bool isMoving = true;
    private bool collideWithAnObstacle = false;


    private bool isFirstTimeIterateMovements = true;

    public void MoveLeft()
    {
        manager.AllMovements.Enqueue(MoveComplete.LEFT);
    }
    public void MoveRight()
    {
        manager.AllMovements.Enqueue(MoveComplete.RIGHT);
    }

    public void MoveForward()
    {
        manager.AllMovements.Enqueue(MoveComplete.FORWARD);
    }
    public void MoveBackward()
    {
        manager.AllMovements.Enqueue(MoveComplete.BACKWARD);
    }

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


    private IEnumerator ProcessCurrentMovement(MoveComplete currentMove)
    {
        switch (currentMove)
        {
            case MoveComplete.LEFT:
                yield return MoveCharacter(Vector3.left * walkForce);
                break;
            case MoveComplete.RIGHT:
                yield return MoveCharacter(Vector3.right * walkForce);
                break;
            case MoveComplete.FORWARD:
                yield return MoveCharacter(Vector3.forward * walkForce);
                break;
            case MoveComplete.BACKWARD:
                yield return MoveCharacter(Vector3.back * walkForce);
                break;
        }
    }




    private IEnumerator MoveCharacter(Vector3 direction)
    {
        rb.linearVelocity = direction;
        yield return new WaitForSeconds(0.5f);
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
        if (manager.AllMovements.Count != 0)
        {
            var currentMove = manager.AllMovements.Dequeue();
            //se achar while
            if (currentMove == MoveComplete.WHILE)
            {
                yield return ProcessWhile();
            }else if (currentMove == MoveComplete.IF)
            {
                yield return ProcessIf();
            }
            else
            {
                yield return ProcessCurrentMovement(currentMove);
            }
        }
        else
        {
            isMoving = false;
        }
    }


    /*LIDA WHILE*/
    private IEnumerator ProcessWhile()
    {
        //pega a condicao ao entrar no while
        var condition = manager.AllMovements.Dequeue();
        Queue<MoveComplete> insideWhileMovement = GetMovementsInsideWhileAndReturn();

        switch (condition)
        {
            case MoveComplete.FREE_WAY:
                while (!collideWithAnObstacle)
                {
                    //remove o item
                    MoveComplete movementInsideWhile = insideWhileMovement.Dequeue();
                    //coloca de novo ao final
                    insideWhileMovement.Enqueue(movementInsideWhile);
                    yield return ProcessCurrentMovement(movementInsideWhile);
                }

                //reseta o collide
                if (collideWithAnObstacle)
                {
                    collideWithAnObstacle = false;
                }

                break;
        }


    }

    private Queue<MoveComplete> GetMovementsInsideWhileAndReturn()
    {
        //cria uma lista auxiliar para os movimentos que estão apenas dentro do while
        Queue<MoveComplete> insideWhileMovement = new();

        //movimentos apos a condicao
        var movement = manager.AllMovements.Dequeue();
        while (movement != MoveComplete.END_WHILE)
        {
            insideWhileMovement.Enqueue(movement);
            movement = manager.AllMovements.Dequeue();
        }
        return insideWhileMovement;
    }
    /*FIM LIDA WHILE*/


    /*LIDA IF*/
    private IEnumerator ProcessIf()
    {
        //pega a condicao ao entrar no if
        var condition = manager.AllMovements.Dequeue();
        Queue<MoveComplete> insideIfMoves = GetMovementsInsideWhileAndReturn();

        switch (condition)
        {
            case MoveComplete.FREE_WAY:
                while (!collideWithAnObstacle)
                {
                    //remove o item
                    MoveComplete moveInsideIf = insideIfMoves.Dequeue();
                    //coloca de novo ao final
                    insideIfMoves.Enqueue(moveInsideIf);
                    yield return ProcessCurrentMovement(moveInsideIf);
                }

                //reseta o collide
                if (collideWithAnObstacle)
                {
                    collideWithAnObstacle = false;
                }

                break;
        }
    }

    private Queue<MoveComplete> GetMovementsInsideIFAndReturn()
    {
        //cria uma lista auxiliar para os movimentos que estão apenas dentro do while
        Queue<MoveComplete> insideIfMoves = new();

        //movimentos apos a condicao
        var movement = manager.AllMovements.Dequeue();
        while (movement != MoveComplete.END_IF)
        {
            insideIfMoves.Enqueue(movement);
            movement = manager.AllMovements.Dequeue();
        }
        return insideIfMoves;
    }
    /*FIM LIDA IF*/







    //COLISAO
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.collider.CompareTag("Obstacle"))
        {
            collideWithAnObstacle = true;
        }
    }
}



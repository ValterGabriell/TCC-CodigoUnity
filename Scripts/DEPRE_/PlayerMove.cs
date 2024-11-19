using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;


public class PlayerMove : MonoBehaviour
{
    public GameManager manager;
    public Rigidbody rb;
    public float walkForce = 6f;
    public float jumpForce = 10f;
    public bool isGrounded = true;
    private bool isMoving = true;
    private bool collideWithAnObstacle = false;
        
    public bool raycastIsCollidingOnObstacle = false;
    public bool handleCollisionWithObstacle = false;






    private void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        int layerMask = 1 << 8;
        int sizeRaycast = 3;

        Debug.DrawRay(transform.position, fwd * sizeRaycast, Color.red);

        if (Physics.Raycast(transform.position, fwd, out RaycastHit hit,sizeRaycast, layerMask))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                if (handleCollisionWithObstacle)
                {
                    raycastIsCollidingOnObstacle = false;
                }
                else
                {
                    raycastIsCollidingOnObstacle = true;
                }
            }
        }
        else
        {
            //NAO COLIDE COM NADA
            raycastIsCollidingOnObstacle = false;
        }

    }




    private IEnumerator HandleMovePlayer()
    {
        manager.gameIsRunning = true;
        if (manager.AllMovements.Count != 0)
        {
            var currentMove = manager.AllMovements.Dequeue();
            if (raycastIsCollidingOnObstacle)
            {
                isMoving = false;
                manager.AllMovements.Clear();
                handleCollisionWithObstacle = true;
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
        Queue<MoveComplete> insideIfMoves = GetMovementsInsideIFAndReturn();

        switch (condition)
        {
            case MoveComplete.HAS_COLLIDE_WITH_OBSTACLE:
                if (collideWithAnObstacle)
                {
                    //remove o item
                    MoveComplete moveInsideIf = insideIfMoves.Dequeue();
                    yield return ProcessCurrentMovement(moveInsideIf);
                }

                //reseta o collide
                if (collideWithAnObstacle)
                {
                    collideWithAnObstacle = false;
                }
                break;

            case MoveComplete.FREE_WAY:
                if (!collideWithAnObstacle)
                {
                    //remove o item
                    MoveComplete moveInsideIf = insideIfMoves.Dequeue();
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



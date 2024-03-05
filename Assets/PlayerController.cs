using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    Vector2 moveDir;
    public bool pushBox = false;
    private Stack<bool> pushBoxStack = new Stack<bool>();
    private Stack<Vector2> directionStack = new Stack<Vector2>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            moveDir = Vector2.right;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            moveDir = Vector2.left;

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            moveDir = Vector2.up;

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            moveDir = Vector2.down;

        if (Input.GetKeyDown(KeyCode.Q))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (Input.GetKeyDown(KeyCode.P))
            UndoMove();


        if (moveDir != Vector2.zero)
        {
            if (CanMoveToDir(moveDir))
            {
                Move(moveDir);
                pushBoxStack.Push(pushBox);
                directionStack.Push(moveDir);
            }
        }
        moveDir= Vector2.zero;
    }

    bool CanMoveToDir(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)dir * 0.5f, dir, 0.5f);
        if (!hit)
        {
            return true;
        }
            
        else
        {
            if (hit.collider.GetComponent<Box>() != null)
            {
                if (hit.collider.GetComponent<Box>().CanMoveToDir(dir))
                {
                    pushBox = true;
                    return true;
                }
            }
                
        }

        return false;
    }

    void Move(Vector2 dir)
    {
        transform.Translate(dir);
    }


    void UndoMove()
    {
        if (directionStack.Count > 0)
        {
            Vector2 moveDirection = directionStack.Pop();

            if (pushBoxStack.Count > 0)
            {
                bool wasBoxPushed = pushBoxStack.Pop();

                if (wasBoxPushed)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)moveDirection * 0.5f, moveDirection, 0.5f);
                    if (hit.collider != null && hit.collider.GetComponent<Box>())
                    {
                        hit.collider.transform.Translate(-moveDirection);
                    }
                }


            }
            transform.Translate(-moveDirection);
        }
    }

}

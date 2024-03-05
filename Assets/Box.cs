using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Box : MonoBehaviour
{
    public Color targetColor;
    Color originColor;
    public bool isOnTarget = false;

    public bool CanMoveToDir(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position+(Vector3)dir*0.5f, dir, 0.5f);//Avoid to detect box itself
        if (!hit)
        {
            transform.Translate(dir);
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            isOnTarget = true;
            originColor = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = targetColor;
            transform.position = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            isOnTarget = false;
            GetComponent<SpriteRenderer>().color = originColor;
        }
    }


}

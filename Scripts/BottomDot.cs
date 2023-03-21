using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomDot : MonoBehaviour
{

    public bool canMerge = false;

    Vector2 startPos;

    public string mergeTag;

    BottomBoard bottomBoard;
    GameManager gameManager;
    void Start()
    {
        bottomBoard = GameObject.FindObjectOfType<BottomBoard>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        startPos = this.transform.position;
    }

    private void OnMouseDown()
    {
        canMerge = false;
    }

    private void OnMouseDrag()
    {
        canMerge = false;
        Move();
    }

    private void OnMouseUp()
    {
        canMerge = true;

        StartCoroutine(ReturnStartPosCo());
;    }

    private void Move()
    {
        if (gameManager.Moves > 0)
        {
            this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9));
        }
    }

    IEnumerator ReturnStartPosCo()
    {
        yield return new WaitForSeconds(.2f);
        gameObject.transform.position = startPos;   
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");
        if (collision.gameObject.tag == mergeTag && gameManager.Moves > 0)
        {
            if (!canMerge) return;
            Debug.Log("Collided Dot");
            AddDot(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Collided");
        if (collision.gameObject.tag == mergeTag && gameManager.Moves > 0)
        {
            if (!canMerge) return;
            Debug.Log("Collided Dot");
            AddDot(collision.gameObject);
        }
    }

    public void AddDot(GameObject dot)
    {
        dot.GetComponent<Dot>().dotNumber *= 2;
        dot.GetComponent<Dot>().SetColorAndNumber();
        bottomBoard.InstantiateNewDot(startPos);
        Destroy(gameObject);
    }
}

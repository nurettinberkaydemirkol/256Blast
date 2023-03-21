using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindDot : MonoBehaviour
{
    [SerializeField] public bool canMerge = false;

    Vector2 startPos;

    bool used = false;

    public string mergeTag;

    BottomBoard bottomBoard;
    GameManager gameManager;
    Board board;

    void Start()
    {
        board = GameObject.FindObjectOfType<Board>();
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
        ;
    }

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
        if (gameManager.Moves > 0 && collision.gameObject.tag != "Bottom Dot")
        {
            if (!canMerge) return;
            if (used) return;
            Bomb(collision.gameObject);
            used = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameManager.Moves > 0 && collision.gameObject.tag != "Bottom Dot")
        {
            if (!canMerge) return;
            Bomb(collision.gameObject);
        }
    }

    public void Bomb(GameObject dot)
    {

        if (dot == null) return;

        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        gameManager.DecreaseMove();

        bottomBoard.InstantiateNewDot(startPos);


        for (float a = 0; a < board.width; a += 1)
        {
            for (float b = 0; b < board.height; b += 1)
            {
                if(dot.GetComponent<Dot>().dotNumber == board.allDots[(int)a, (int)b].GetComponent<Dot>().dotNumber)
                {
                    GameObject.FindObjectOfType<Board>().allDots[(int)a, (int)b].GetComponent<Dot>().isMatched = true;
                    GameObject.FindObjectOfType<MatchFinding>().FindAllMatches();
                }
            }
        }
        StartCoroutine(DestroyMatchesBombCo());
    }

    IEnumerator DestroyMatchesBombCo()
    {
        yield return new WaitForSeconds(.25f);
        board.DestroyMatches();

        Destroy(gameObject);
    }
}


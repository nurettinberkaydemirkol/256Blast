using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDot : MonoBehaviour
{
    [SerializeField] public bool canMerge = false;

    Vector2 startPos;

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
        List<GameObject> touchs = new List<GameObject>();
        touchs.Add(collision.gameObject);
        if (gameManager.Moves > 0 && collision.gameObject.tag != "Bottom Dot")
        {
            if (!canMerge) return;
            Bomb(touchs[0]);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        List<GameObject> touchs = new List<GameObject>();
        touchs.Add(collision.gameObject);
        if (gameManager.Moves > 0 && collision.gameObject.tag != "Bottom Dot")
        {
            if (!canMerge) return;
            Bomb(touchs[0]);
        }
    }

    public void Bomb(GameObject dot)
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        float i = dot.transform.position.y;
        float j = dot.transform.position.x;

        gameManager.DecreaseMove();
        bottomBoard.InstantiateNewDot(startPos);

        for (float a = j - 1; a <= j + 1 && a < board.width; a += 1)
        {
            for(float b = i-1; b <= i+1 && b < board.height; b += 1)
            {
                if (a < 0) a = 0;
                if (b < 0) b = 0;

                GameObject.FindObjectOfType<Board>().allDots[(int)a, (int)b].GetComponent<Dot>().isMatched = true;
                GameObject.FindObjectOfType<MatchFinding>().FindAllMatches();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchFinding : MonoBehaviour
{
    private Board board;
    public List<GameObject> currentMatches = new List<GameObject>();

    void Start()
    {
        board = FindObjectOfType<Board>();
        StartCoroutine(FindAllMatchesCo());
    }

    public void FindAllMatches()
    {
        StartCoroutine(FindAllMatchesCo());
    }

    private IEnumerator FindAllMatchesCo()
    {
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                GameObject currentDot = board.allDots[i, j];
                if(currentDot != null)
                {
                    if(i > 0 && i < board.width - 1)
                    {
                        GameObject leftDot = board.allDots[i - 1, j];
                        GameObject rightDot = board.allDots[i + 1, j];
                        if(leftDot != null && rightDot != null)
                        {
                            if(leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag)
                            {

                                leftDot.GetComponent<Dot>().isMatched = true;

                                rightDot.GetComponent<Dot>().isMatched = true;

                                currentDot.GetComponent<Dot>().isMatched = true;
                            }
                        }
                    }

                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject upDot = board.allDots[i, j + 1];
                        GameObject downDot = board.allDots[i, j - 1];
                        if (upDot != null && downDot != null)
                        {
                            if (upDot.tag == currentDot.tag && downDot.tag == currentDot.tag)
                            {
                                upDot.GetComponent<Dot>().isMatched = true;

                                downDot.GetComponent<Dot>().isMatched = true;

                                currentDot.GetComponent<Dot>().isMatched = true;
                            }
                        }
                    }
                }
            }
        }

    }
    
}

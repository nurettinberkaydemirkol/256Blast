using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xtwoAll : MonoBehaviour
{

    Board board;
    int jokerCount;

    public TextMesh jokerCountText;

    private void Start()
    {
        board = GameObject.FindObjectOfType<Board>();
        jokerCount = PlayerPrefs.GetInt("xtwo");
        jokerCountText.text = jokerCount.ToString();
    }

    private void OnMouseEnter()
    {
        UseJoker();
    }

    void UseJoker()
    {
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                board.allDots[i, j].GetComponent<Dot>().dotNumber *= 2;
                board.allDots[i, j].GetComponent<Dot>().SetColorAndNumber();
            }
        }

        jokerCount--;
        PlayerPrefs.SetInt("xtwo", jokerCount);
        jokerCount = PlayerPrefs.GetInt("addmove");
        jokerCountText.text = jokerCount.ToString();
    }
}

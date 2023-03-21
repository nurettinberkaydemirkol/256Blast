using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addMove : MonoBehaviour
{
    GameManager mng;
    int jokerCount;

    public TextMesh jokerCountText;

    private void Start()
    {
        mng = GameObject.FindObjectOfType<GameManager>();
        jokerCount = PlayerPrefs.GetInt("addmove");
        jokerCountText.text = jokerCount.ToString();
    }

    private void OnMouseEnter()
    {
        UseJoker();
    }

    void UseJoker()
    {
        mng.Moves += 10;
        jokerCount--;
        jokerCountText.text = jokerCount.ToString();
        PlayerPrefs.SetInt("addmove", jokerCount);
        jokerCount = PlayerPrefs.GetInt("addmove");
    }
}

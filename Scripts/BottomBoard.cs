using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBoard : MonoBehaviour
{
    public int dotCount;
    public GameObject[] dotPrefabs;
    public dotDB dots;

    void Start()
    {
        SetBottomDots();
    }

    void Update()
    {
        
    }

    void SetBottomDots()
    {
        for(int i = 0; i < dotCount; i++)
        {
            Vector2 pos = new Vector2(i, -2);
            GameObject dot = Instantiate(dots.bottomDots[Random.Range(0, dotPrefabs.Length)], pos, Quaternion.identity);
        }
    }

    public void InstantiateNewDot(Vector2 pos)
    {
        Instantiate(dotPrefabs[Random.Range(0, dotPrefabs.Length)], pos, Quaternion.identity);
    }
}

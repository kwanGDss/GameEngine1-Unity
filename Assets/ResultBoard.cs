using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultBoard : MonoBehaviour
{
    private GameObject winBoard;
    private GameObject loseBoard;

    // Start is called before the first frame update
    void Start()
    {
        winBoard = transform.Find("WinBoard").gameObject;
        loseBoard = transform.Find("LoseBoard").gameObject;

        winBoard.SetActive(false);
        loseBoard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Result();
    }

    private void Result()
    {
        if (GameManager.winner)
        {
            winBoard.SetActive(true);
        }

        if (GameManager.loser)
        {
            loseBoard.SetActive(true);
        }
    }
}

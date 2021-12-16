using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XOManager : MonoBehaviour
{

    public static XOManager x;

    [SerializeField] private GameObject[] Xs;
    [SerializeField] private GameObject[] Os;

    private GameObject[][] gamePieces;
    private int[][] gameState = new int[][] {
        new int[] { -1, -1, -1},
        new int[] { -1, -1, -1},
        new int[] { -1, -1, -1}
    };

    private void Awake()
    {
        x = this;
        gamePieces = new GameObject[2][] { Xs, Os };
        ResetBoard();
    }

    public void SetSlot(int player, int x, int y)
    {
        gameState[x][y] = player;
        UpdateBoard();
    }

    private void UpdateBoard()
    {

        for (int i = 0; i < gameState.Length; i++)
        {
            for (int j = 0; j < gameState[i].Length; j++)
            {
                gamePieces[0][i + (j * 3)].SetActive(false); ;
                gamePieces[1][i + (j * 3)].SetActive(false); ;
                if (gameState[i][j] != -1)
                    gamePieces[gameState[i][j]][i + (j * 3)].SetActive(true);
            }
        }
    }

    private int CheckDifference()
    {
        int diff = 0;
        for (int i = 0; i < gameState.Length; i++)
        {
            for (int j = 0; j < gameState[i].Length; j++)
            {
                if (gameState[i][j] != -1)
                    diff++;
            }
        }
        return diff;
    } 

    public void ResetBoard()
    {
        for (int i = 0; i < gameState.Length; i++)
        {
            for (int j = 0; j < gameState[i].Length; j++)
            {
                gameState[i][j] = -1;
            }
        }
        UpdateBoard();
    }

    public int[][] GetState()
    {
        return gameState;
    }

}

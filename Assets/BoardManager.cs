using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager boardManager;

    private const float GRID_HEIGHT = 5.0f;
    private const float GRID_WIDTH = 5.0f;
    [SerializeField] private static int occupiedSpacesCount = 0;        
    public static int totalScore = 0;

    [SerializeField] private static bool[,] boardState = new bool[5, 5];
    public static bool[,]  BoardState { get => boardState; set => boardState = value; }


    // Start is called before the first frame update
    void Awake()
    {
        MakeThisTheOnlyBoardManager();
    }

    // Update is called once per frame
    void MakeThisTheOnlyBoardManager()
    {   
        if (boardManager == null)
        {
            DontDestroyOnLoad(gameObject);
            boardManager = this;
        }
        else
        {
            if (boardManager != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void DetermineBoardState()
    {
        //count grid
        occupiedSpacesCount = 0;

        for (int x = 0; x < GRID_WIDTH; x++)
        {
            for (int y = 0; y < GRID_HEIGHT; y++)
            {
                Vector2 gridSpace = new Vector2(x, y);
                RaycastHit2D hit = Physics2D.Raycast(gridSpace, Vector2.zero);
                if (hit.collider != null && hit.collider.CompareTag("Tile"))
                {
                    //Debug.DrawLine(
                    //    new Vector3(gridSpace.x, gridSpace.y, -1f), 
                    //    new Vector3(gridSpace.x+0.3f, gridSpace.y, -1f),
                    //    Color.green, 100f, false);
                    BoardState[x, y] = true;
                    occupiedSpacesCount++;
                }
            }
        }
    }

    internal void GenerateScore(List<GameTile> gameTiles)
    {
        totalScore = 0;
        foreach (GameTile gameTile in gameTiles)
        {
            totalScore += gameTile.scoreValue;
        }
    }
}

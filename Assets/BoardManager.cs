using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{    
    private const float GRID_HEIGHT = 5.0f;
    private const float GRID_WIDTH = 5.0f;
    [SerializeField] private int occupiedSpacesCount = 0;
    
    [SerializeField] private bool[,] boardState = new bool[5, 5];
    public bool[,] BoardState { get => boardState; set => boardState = value; }

    private bool CheckIfValidPlacement()
    {

        return false;
    }


    // Start is called before the first frame update
    void Start()
    {   
        
    }


    // Update is called once per frame
    void Update()
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

        //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        //if (occupiedSpacesCount != 0)
        //{
            //Debug.Log(occupiedSpacesCount);
        //}
        
    }
    
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile : MonoBehaviour
{
    [SerializeField] private float startPosX;
    [SerializeField] float startPosY;
    [SerializeField] private Vector2 mousePos;
    [SerializeField] private Vector2 oldPosition;
    [SerializeField] private bool isInsideGrid = false;
    [SerializeField] private bool isInsideBackground = true;    
    [SerializeField] private bool isBeingHeld = false;
    public bool isLockedIn = false;
    [SerializeField] private bool isInsideTile = false;
    [SerializeField] private bool isValidPosition;
    public int scoreValue;

    public BoardManager boardManager;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isLockedIn)
        {
            if (collision.CompareTag("Grid"))
            {
                isInsideGrid = true;
            }
            else if (collision.CompareTag("Background"))
            {
                isInsideBackground = true;
            }
            else if (collision.CompareTag("Tile"))
            {
                isInsideTile = true;
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (!isLockedIn)
    //    {
    //        if (collision.CompareTag("Grid"))
    //        {
    //            isInsideGrid = true;
    //        }
    //        else if (collision.CompareTag("Background"))
    //        {
    //            isInsideBackground = true;
    //        }            
    //        else if (collision.CompareTag("Tile"))
    //        {
    //            isInsideTile = true;
    //        }            
    //    }
    //}


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isLockedIn)
        {
            if (collision.CompareTag("Grid"))
            {
                isInsideGrid = false;
            }
            else if (collision.CompareTag("Background"))
            {
                isInsideBackground = false;
            }
            else if (collision.CompareTag("Tile"))
            {
                isInsideTile = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld)
        {

            //TODO: Fix collision bugging out and allowing invalid placements
            //WhatIsColliding();

            if (Input.GetMouseButtonDown(1))
            {                
                transform.Rotate(Vector3.forward * -90);
                //TODO:recenter object on rotate
                gameObject.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }

            //Find mouse position
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Enable snapping when:
            if (isInsideGrid && !isInsideBackground && !isInsideTile)
            {   
                //snap to grid
                gameObject.transform.localPosition = new Vector2(
                    Mathf.Round((mousePos.x - startPosX) * 1),
                    Mathf.Round((mousePos.y - startPosY) * 1));
            }
            else
            { 
                //free form drag
                gameObject.transform.localPosition = new Vector2(mousePos.x - startPosX, mousePos.y - startPosY);
            }
        }
    }

    private void WhatIsColliding()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();
        Physics2D.OverlapCollider(GetComponent<Collider2D>(), contactFilter, colliders);

        isInsideGrid = true;
        isInsideBackground = true;
        isInsideTile = true;

        foreach (Collider2D collider in colliders)
        {
            Debug.Log(collider.name);
            if (!isLockedIn)
            {
                if (collider.CompareTag("Grid"))
                {
                    Debug.Log("grid");
                    isInsideGrid = false;
                }
                else if (collider.CompareTag("Background"))
                {
                    isInsideBackground = false;
                }
                else if (collider.CompareTag("Tile"))
                {
                    isInsideTile = false;
                }
            }
        }
    }

    //Move the piece on LMB hold
    private void OnMouseDown()
    {        
        if (Input.GetMouseButtonDown(0) && !isLockedIn)
        {
            this.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            oldPosition = this.transform.localPosition;
                        
            isBeingHeld = true;
        }

        
    }

    //Release the tile
    private void OnMouseUp()
    {
        isBeingHeld = false;

        if (!IsValidFinalPlacement())
        {
            gameObject.transform.position = oldPosition;
            this.GetComponentInChildren<SpriteRenderer>().sortingOrder = 0;
        }
        else
        {
            this.GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
        }

        if (IsValidFinalPlacement() && isInsideGrid)
        {
            isLockedIn = true;
        }
    }

    private bool IsValidFinalPlacement()
    {
        //raycast coordinates of item;

        if (isInsideBackground) //in the background
        {
            isValidPosition = false;
        }
        else if (isInsideGrid && isInsideTile) //in grid but in a tile!
        {
            isValidPosition = false;
        }
        else
        {
            isValidPosition = true;
        }

        return isValidPosition;

    }
}


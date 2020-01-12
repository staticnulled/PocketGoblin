using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] private float startPosX;
    [SerializeField] float startPosY;
    [SerializeField] private Vector2 mousePos;
    [SerializeField] private Vector2 oldPosition;
    [SerializeField] private bool isInsideGrid = false;
    [SerializeField] private bool isInsideBackground = false;    
    [SerializeField] private bool isBeingHeld = false;
    [SerializeField] private bool isLockedIn = false;
    [SerializeField] private bool isInsideTile = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isLockedIn)
        {
            if (collision.CompareTag("Grid"))
            {
                isInsideGrid = true;
            }
            else
            {
                isInsideGrid = false;
            }

            if (collision.CompareTag("Background"))
            {
                isInsideBackground = true;
            }
            else
            {
                isInsideBackground = false;
            }

            if (collision.CompareTag("Tile"))
            {
                isInsideTile = true;
            }
            else
            {
                isInsideTile = false;
            }
        }

        //if (isBeingHeld && isInsideGrid & collision.CompareTag("Tile"))
        //{
        //    var collisionDirection = (gameObject.transform.position - collision.transform.position);

        //       isCollidingWithTile = true;
        //}
        //else
        //{            
        //    isCollidingWithTile = false;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("rightclick");
                transform.Rotate(Vector3.forward * -90);
                //TODO:recenter object on rotate
                gameObject.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (isInsideGrid && !isInsideBackground)
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

    //Move the piece on LMB hold
    private void OnMouseDown()
    {
        this.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            oldPosition = this.transform.localPosition;

            isLockedIn = false;
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
        bool isValidPosition;

        //Part way in two zones. Bad placement.
        if (isInsideGrid && isInsideBackground)
        {
            isValidPosition = false;
        }
        //In neither. Means colliding with other tile in grid or off screen.
        else if (!isInsideGrid && !isInsideBackground)
        {
            isValidPosition = false;
        }
        //Inside tile and grid. outside of bg. Invalid
        else if (isInsideGrid && isInsideTile)
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


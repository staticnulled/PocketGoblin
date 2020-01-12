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
    [SerializeField] private bool isBeingHeld = false;    
    [SerializeField] private bool isCollidingWithTile = false;    

    private void OnTriggerEnter2D(Collider2D collision)
    {     
        isInsideGrid = true;
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(gameObject.GetComponent<Collider2D>().bounds.Intersects(collision.bounds));

        if (collision.CompareTag("Grid"))
        {
            isInsideGrid = true;
        }

        if (isBeingHeld && isInsideGrid & collision.CompareTag("Tile"))
        {
            
            isCollidingWithTile = true;
        }
        else
        {
            
            isCollidingWithTile = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {        
        isInsideGrid = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInsideGrid) Debug.Log("is inside grid");

            if (isBeingHeld)
            {            
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (isInsideGrid)
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

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            oldPosition = this.transform.localPosition;
            
            isBeingHeld = true;
        }
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;

        if (isCollidingWithTile)
        {
            gameObject.transform.position = oldPosition;
            isCollidingWithTile = false;
        }        
    }
}


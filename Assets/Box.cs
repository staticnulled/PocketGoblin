using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    private float startPosX;
    private float startPosY;
    private Vector2 mousePos;
    private bool isInsideGrid = false;

    [SerializeField]
    private bool isBeingHeld = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("inside grid");
        isInsideGrid = true;
    }   

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("leftgrid");
        isInsideGrid = false;
    }

    // Update is called once per frame
    void Update()
    {
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

            isBeingHeld = true;
        }
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;
    }
}


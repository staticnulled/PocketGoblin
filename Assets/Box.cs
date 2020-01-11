using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    private float startPosX;
    private float startPosY;
    private Vector2 mousePos;

    [SerializeField]
    private bool isBeingHeld = false;

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld)
        {   
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.localPosition = new Vector2(mousePos.x - startPosX, mousePos.y - startPosY);
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


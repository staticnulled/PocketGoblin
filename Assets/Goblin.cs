using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    private SpriteRenderer spriteR;
    public Sprite[] sprites;
    private bool isCoroutineExecuting = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        float mousePosX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        Debug.Log(spriteR.sprite);

        if (mousePosX < -5f)
        {
            spriteR.sprite = sprites[0];
        }
        else
        {
            spriteR.sprite = sprites[2];
        }
    }
}

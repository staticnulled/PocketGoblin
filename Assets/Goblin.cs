using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    private SpriteRenderer spriteR;
    private Sprite[] sprites;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.mousePosition.x > -5f)
        //{            
        //    spriteR.sprite = sprites[0];
        //}
        //else
        //{
        //   spriteR.sprite = sprites[1];
        //}
    }
}

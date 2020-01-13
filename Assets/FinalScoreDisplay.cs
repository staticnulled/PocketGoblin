using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<UnityEngine.UI.Text>().text = BoardManager.totalScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = 0.25f;

    private void OnMouseDown()
    {
        Debug.Log("mouse down");
        if (Input.GetMouseButtonDown(0))
        {
            Invoke("LoadFirstLevel", loadLevelDelay);
        }        
    }

    private void LoadFirstLevel()
    {
        Debug.Log("Loading Level 1");
        SceneManager.LoadSceneAsync("Level1");
    }
}

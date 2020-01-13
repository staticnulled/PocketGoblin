using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroText : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = 8f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadFirstLevel", loadLevelDelay);
    }

    private void LoadFirstLevel()
    {
        Debug.Log("Loading Level 1");
        SceneManager.LoadSceneAsync("Level1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

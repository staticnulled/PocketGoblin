﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimedSpawn : MonoBehaviour
{
    //Timer related
    private GameObject spawnee;
    private GameObject[] gameTiles;
    public bool stopSpawning = false;
    public float spawnTime = 5f;
    public float spawnDelay = 7f;
    public float speed = 2.0f;
    public GameObject Hand;
    public bool isAnimating = false;
    public float handDelay = 0.25f;
    private Vector2 direction = Vector2.down;
    private GameObject generatedTile;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip newItemAvailableSound;
    [SerializeField] private int spawnLimit = 10;
    [SerializeField] private int spawnCount = 0;
    [SerializeField] float loadLevelDelay = 3f;
    public BoardManager boardManager;


    public void SpawnObject()
    {   
        if (!stopSpawning && spawnCount != spawnLimit)
        {
            spawnee = gameTiles[Random.Range(0, gameTiles.Length)];
            generatedTile  = Instantiate(spawnee, transform.position, transform.rotation);
            generatedTile.tag = "Tile";
            spawnCount++;
            StartAnimating();
        }
        else
        {
            
            List<GameTile> lockedInGameTiles = new List<GameTile>(FindObjectsOfType<GameTile>());
            lockedInGameTiles.RemoveAll(x => !x.isLockedIn);
            boardManager.GenerateScore(lockedInGameTiles);
            
            Invoke("LoadScoreScreen", loadLevelDelay);
        }
    }

    private void LoadScoreScreen()
    {
        Debug.Log("Loading Score Screen");
        SceneManager.LoadSceneAsync("ScoreScreen");
    }

    private void StopAnimating()
    {
        isAnimating = false;
    }
    private void StartAnimating()
    {
        isAnimating = true;
    }


    private void AnimateHandAndItem()
    {
        //Debug.Log(Hand.transform.position.y);
        if (Hand.transform.position.y <= 4)
        {        
            direction = Vector2.up;
            DestroyUnlockedTiles();
            StopAnimating();
                        
            audioSource.PlayOneShot(newItemAvailableSound);

            Invoke("StartAnimating", handDelay);
        }
        else if (Hand.transform.position.y >= 10 && direction == Vector2.up)
        {
            direction = Vector2.down;
            StopAnimating();
        }

        Hand.transform.Translate(direction * speed * Time.deltaTime);
        
        if (generatedTile.transform.position.y >= 2f)
        {
            generatedTile.transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }

    private void DestroyUnlockedTiles()
    {
        GameObject[] expiredTiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in expiredTiles)
        {
            GameTile gameTile = tile.GetComponent<GameTile>();
            if (tile.tag == "Tile" && !gameTile.isLockedIn && !GameObject.ReferenceEquals(tile, generatedTile))
            Destroy(tile);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        if (gameTiles == null)
            gameTiles = GameObject.FindGameObjectsWithTag("TemplateTile");

        //Debug.Log(gameTiles.Length);

        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    //private GameObject[] GetGameTiles()
    //{        
    //    return 
    //}

    // Update is called once per frame
    void Update()
    {
        if (isAnimating) AnimateHandAndItem();
    }
}

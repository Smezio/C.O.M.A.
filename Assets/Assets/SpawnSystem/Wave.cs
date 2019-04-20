using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wave : MonoBehaviour
{
    private bool canSpawn;
    private float resetWave;
    private float cooldown;
    private int counter;
    private SceneControlScript levelControl;
    private int minEnemies;
    private int maxEnemies;

    private float pauseStart;
    private float pauseFinish;
    
    private GameObject[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        for (uint i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].SetActive(false);
        }
        levelControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SceneControlScript>();

        canSpawn = true;
        cooldown = levelControl.CurrentLevel.CooldownSpawn;
        resetWave = cooldown;
        counter = 0;
        
        minEnemies = levelControl.CurrentLevel.MinSpawn;
        maxEnemies = levelControl.CurrentLevel.MaxSpawn;

        pauseStart = 0;
        pauseFinish = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            if (Time.time - (pauseFinish - pauseStart) > resetWave)
            {
                pauseStart = 0;
                pauseFinish = 0;

                counter++;
                Debug.Log(counter + " spawn");
                if (levelControl.CurrentLevel.Number == 1 && counter == 3)
                {
                    levelControl.CurrentLevel = new Level(2);
                    cooldown = levelControl.CurrentLevel.CooldownSpawn;
                    minEnemies = levelControl.CurrentLevel.MinSpawn;
                    maxEnemies = levelControl.CurrentLevel.MaxSpawn;
                    counter = 0;

                    /* Modificare */
                    GameObject.FindWithTag("GameController").GetComponent<Text>().text = "Level 2";
                }
                else if (levelControl.CurrentLevel.Number == 2 && counter == 3)
                {
                    levelControl.CurrentLevel = new Level(3);
                    cooldown = levelControl.CurrentLevel.CooldownSpawn;
                    minEnemies = levelControl.CurrentLevel.MinSpawn;
                    maxEnemies = levelControl.CurrentLevel.MaxSpawn;
                    counter = 0;

                    /* Modificare */
                    GameObject.FindWithTag("GameController").GetComponent<Text>().text = "Level 3";
                }

                SpawnWave();
                resetWave = Time.time + cooldown;
            }
        }
    }

    private void SpawnWave()
    {
        int numEnemies = Random.Range(minEnemies, maxEnemies);
        List<int> indexList = new List<int>();
        for (int i = 0; i < numEnemies; i++)
        {
            // Random index without repeating
            int indexEnemy;
            do
            {
                indexEnemy = Random.Range(0, spawnPoints.Length);
            }
            while (indexList.Contains(indexEnemy));
            indexList.Add(indexEnemy);

            // Activate points
            for (int j = 0; j < indexList.Count; j++)
                spawnPoints[indexList[j]].SetActive(true);
        }
    }

    public void PauseOn()
    {
        canSpawn = false;
        pauseStart = Time.time;

        for (int i = 0; i < spawnPoints.Length; i++)
            spawnPoints[i].GetComponent<Animator>().enabled = false;
    }

    public void PauseOff()
    {
        canSpawn = true;
        pauseFinish = Time.time;

        for (int i = 0; i < spawnPoints.Length; i++)
            spawnPoints[i].GetComponent<Animator>().enabled = true;
    }

    public bool CanSpawn
    {
        get { return canSpawn; }
    }
}

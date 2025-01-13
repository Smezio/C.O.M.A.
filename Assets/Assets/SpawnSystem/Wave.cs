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
    private int minSpawnIndex;
    private int maxSpawnIndex;

    private float pauseStart;
    private float pauseFinish;
    
    private GameObject[] spawnPoints;
    private List<int> indexList;

    // Start is called before the first frame update
    void Start()
    {
        levelControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SceneControlScript>();

        canSpawn = true;
        cooldown = levelControl.CurrentLevel.CooldownSpawn;
        resetWave = cooldown;
        counter = 0;

        indexList = new List<int>();

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
            if (Time.timeSinceLevelLoad - (pauseFinish - pauseStart) > resetWave)
            {
                pauseStart = 0;
                pauseFinish = 0;

                counter++;
                if (levelControl.CurrentLevel.Number+1 == 2 && counter == 3)
                {
                    levelControl.CurrentLevel = new Level(2);
                    cooldown = levelControl.CurrentLevel.CooldownSpawn;
                    minEnemies = levelControl.CurrentLevel.MinSpawn;
                    maxEnemies = levelControl.CurrentLevel.MaxSpawn;
                    counter = 0;
                    
                    GameObject.FindWithTag("GameController").GetComponent<Text>().text = "Level 2";
                }
                else if (levelControl.CurrentLevel.Number+1 == 3 && counter == 4)
                {
                    levelControl.CurrentLevel = new Level(3);
                    cooldown = levelControl.CurrentLevel.CooldownSpawn;
                    minEnemies = levelControl.CurrentLevel.MinSpawn;
                    maxEnemies = levelControl.CurrentLevel.MaxSpawn;
                    counter = 0;
                    
                    GameObject.FindWithTag("GameController").GetComponent<Text>().text = "Level 3";
                }
                else if (levelControl.CurrentLevel.Number+1 == 4 && counter == 4)
                {
                    levelControl.CurrentLevel = new Level(4);
                    cooldown = levelControl.CurrentLevel.CooldownSpawn;
                    minEnemies = levelControl.CurrentLevel.MinSpawn;
                    maxEnemies = levelControl.CurrentLevel.MaxSpawn;
                    counter = 0;

                    levelControl.GetComponent<AudioSource>().Stop();
                    levelControl.GetComponent<AudioSource>().PlayOneShot(levelControl.bossTheme);
                    GameObject boss = Instantiate(Resources.Load("Enemies/Boss")) as GameObject;
                    boss.GetComponentInChildren<BossClass>().Frequency = levelControl.CurrentLevel.EnemyBossFrequency;
                    boss.GetComponentInChildren<BossClass>().Amplitude = levelControl.CurrentLevel.EnemyBossAmplitude;
                    boss.GetComponentInChildren<BossClass>().Cooldown = levelControl.CurrentLevel.EnemyBossCooldown;
                    boss.GetComponentInChildren<BossClass>().RushCooldown = levelControl.CurrentLevel.EnemyBossRushCooldown;
                    boss.GetComponentInChildren<BossClass>().RushFrequency = levelControl.CurrentLevel.EnemyBossRushFrequency;
                    boss.GetComponentInChildren<BossClass>().RushAmplitude = levelControl.CurrentLevel.EnemyBossRushAmplitude;
                    boss.GetComponentInChildren<BossClass>().RushSpeed = levelControl.CurrentLevel.EnemyBossRushSpeed;
                    
                    GameObject.FindWithTag("GameController").GetComponent<Text>().text = "Level 4";
                }

                SpawnWave();
                resetWave = Time.timeSinceLevelLoad + cooldown;
            }
        }
    }

    private void SpawnWave()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        if (GameObject.Find("Boss") != null)
        {
            if (GameObject.Find("Boss").GetComponentInChildren<BossClass>().IsRight &&
                !GameObject.Find("Boss").GetComponentInChildren<BossClass>().IsLeft)
            {
                minSpawnIndex = 0;
                maxSpawnIndex = spawnPoints.Length - 3;
            }
            else if (GameObject.Find("Boss").GetComponentInChildren<BossClass>().IsLeft &&
                !GameObject.Find("Boss").GetComponentInChildren<BossClass>().IsRight)
            {
                minSpawnIndex = 3;
                maxSpawnIndex = spawnPoints.Length;
            }
        }
        else
        {
            minSpawnIndex = 0;
            maxSpawnIndex = spawnPoints.Length;
        }

        int numEnemies = Random.Range(minEnemies, maxEnemies);
        indexList = new List<int>();
        for (int i = 0; i < numEnemies; i++)
        {
            // Random index without repeating
            int indexEnemy;
            do
            {
                indexEnemy = Random.Range(minSpawnIndex, maxSpawnIndex);
            }
            while (indexList.Contains(indexEnemy));
            indexList.Add(indexEnemy);

            // Activate points
            for (int j = 0; j < indexList.Count; j++)
                spawnPoints[indexList[j]].GetComponent<Animator>().enabled = true;
        }
    }

    public void PauseOn()
    {
        canSpawn = false;
        pauseStart = Time.timeSinceLevelLoad;

        for (int i = 0; i < indexList.Count; i++)
            spawnPoints[indexList[i]].GetComponent<Animator>().speed = 0f;
    }

    public void PauseOff()
    {
        canSpawn = true;
        pauseFinish = Time.timeSinceLevelLoad;

        for (int i = 0; i < indexList.Count; i++)
            spawnPoints[indexList[i]].GetComponent<Animator>().speed = 1f;
    }

    public bool CanSpawn
    {
        get { return canSpawn; }
    }
}

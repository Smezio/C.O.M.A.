using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlScript : MonoBehaviour
{
    Transform pauseMenu;
    private bool inPause;
    private GameObject player;
    private GameObject[] enemies;
    private GameObject boss;
    private GameObject[] bullets;
    private GameObject[] playerBullets;
    private Wave wave;
    private BackgroundMovement[] background;

    private Level currentLevel;

    // Start is called before the first frame update
    void Awake()
    {
        inPause = false;
        pauseMenu = GameObject.Find("PauseMenu").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        wave = GameObject.Find("Wave").GetComponent<Wave>();
        background = GameObject.Find("Backgrounds").GetComponentsInChildren<BackgroundMovement>();
        currentLevel = new Level(1);
    }

    // Update is called once per frame
    void Update()
    {
        PauseClick();
    }

    private void PauseClick ()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            bullets = GameObject.FindGameObjectsWithTag("Bullet");

            if (GameObject.Find("Boss") != null)
                boss = GameObject.Find("Boss").transform.GetChild(0).gameObject;
            else
                boss = null;

            if (!inPause)
            {
                inPause = true;
                pauseMenu.GetChild(0).gameObject.SetActive(true);
                pauseMenu.GetChild(1).gameObject.SetActive(true);

                for (int i = 0; i < enemies.Length; i++)
                    enemies[i].GetComponent<EnemyClass>().PauseOn();
                for (int i = 0; i < bullets.Length; i++)
                    bullets[i].GetComponent<BulletClass>().PauseOn();
                player.GetComponent<PlayerClass>().PauseOn();
                if (boss != null)
                    boss.GetComponent<BossClass>().PauseOn();
                wave.PauseOn();
                for (int i = 0; i < background.Length; i++)
                    background[i].PauseOn();
            }
            else
                Resume();
        }
    }

    public void Resume()
    {
        inPause = false;
        pauseMenu.GetChild(0).gameObject.SetActive(false);
        pauseMenu.GetChild(1).gameObject.SetActive(false);

        for (int i = 0; i < enemies.Length; i++)
            enemies[i].GetComponent<EnemyClass>().PauseOff();
        for (int i = 0; i < bullets.Length; i++)
            bullets[i].GetComponent<BulletClass>().PauseOff();
        player.GetComponent<PlayerClass>().PauseOff();
        if (boss != null)
            boss.GetComponent<BossClass>().PauseOff();
        wave.PauseOff();
        for (int i = 0; i < background.Length; i++)
            background[i].PauseOff();
    }

    public void Rematch()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetValues()
    {
        
    }

    public Level CurrentLevel
    {
        get { return currentLevel; }
        set { currentLevel = value; }
    }
}

public struct Level
{
    int number;

    int minSpawn;
    int maxSpawn;
    float cooldownSpawn;

    float enemyLineSpeed;
    float enemyLineCooldown;

    float enemyDashSpeed;
    float enemyDashFrequency;
    float enemyDashCooldown;

    float enemyCircleSpeed;
    float enemyCircleFrequency;
    float enemyCircleCooldown;

    float enemyChargeSpeed;

    bool enemyBossSpawn;
    float enemyBossFrequency;
    float enemyBossCooldown;
    float enemyBossRushSpeed;
    float enemyBossRushFrequency;
    float enemyBossRushCooldown;


    public Level (int num)
    {
        number = num;
        minSpawn = 0;
        maxSpawn = 0;
        cooldownSpawn = 0f;
        enemyLineSpeed = 0f;
        enemyLineCooldown = 0f;
        enemyDashSpeed = 0f;
        enemyDashFrequency = 0f;
        enemyDashCooldown = 0f;
        enemyCircleSpeed = 0f;
        enemyCircleFrequency = 0f;
        enemyCircleCooldown = 0f;
        enemyChargeSpeed = 0f;
        enemyBossSpawn = false;
        enemyBossFrequency = 0f;
        enemyBossCooldown = 0f;
        enemyBossRushSpeed = 0f;
        enemyBossRushFrequency = 0f;
        enemyBossRushCooldown = 0f;

        switch (number)
        {
            case 1:
                {
                    minSpawn = 3;
                    maxSpawn = 6;
                    cooldownSpawn = 10f;
                    enemyLineSpeed = 0.5f;
                    enemyLineCooldown = 3f;
                    enemyDashSpeed = 0.5f;
                    enemyDashFrequency = 1f;
                    enemyDashCooldown = 5f;
                    enemyCircleSpeed = 0.5f;
                    enemyCircleFrequency = 1f;
                    enemyCircleCooldown = 5f;
                    enemyChargeSpeed = 0.5f;
                }
                break;

            case 2:
                {
                    minSpawn = 6;
                    maxSpawn = 8;
                    cooldownSpawn = 7f;
                    enemyLineSpeed = 0.5f;
                    enemyLineCooldown = 2f;
                    enemyDashSpeed = 1f;
                    enemyDashFrequency = 1.5f;
                    enemyDashCooldown = 3f;
                    enemyCircleSpeed = 1f;
                    enemyCircleFrequency = 1.5f;
                    enemyCircleCooldown = 3f;
                    enemyChargeSpeed = 1f;
                }
                break;

            case 3:
                {
                    minSpawn = 6;
                    maxSpawn = 8;
                    cooldownSpawn = 4f;
                    enemyLineSpeed = 0.8f;
                    enemyLineCooldown = 2f;
                    enemyDashSpeed = 2f;
                    enemyDashFrequency = 2f;
                    enemyDashCooldown = 2f;
                    enemyCircleSpeed = 2f;
                    enemyCircleFrequency = 2f;
                    enemyCircleCooldown = 2f;
                    enemyChargeSpeed = 2f;
                }
                break;

            case 4:
                {
                    minSpawn = 6;
                    maxSpawn = 8;
                    cooldownSpawn = 7f;
                    enemyLineSpeed = 0.5f;
                    enemyLineCooldown = 2f;
                    enemyDashSpeed = 1f;
                    enemyDashFrequency = 1.5f;
                    enemyDashCooldown = 3f;
                    enemyCircleSpeed = 1f;
                    enemyCircleFrequency = 1.5f;
                    enemyCircleCooldown = 3f;
                    enemyChargeSpeed = 1f;
                    enemyBossSpawn = true;
                    enemyBossFrequency = 0.8f;
                    enemyBossCooldown = 4f;
                    enemyBossRushSpeed = 1.5f;
                    enemyBossRushFrequency = 3f;
                    enemyBossRushCooldown = 5f;
                }
                break;
        }
    }

    public int Number { get => number; set => number = value; }
    public int MinSpawn { get => minSpawn; }
    public int MaxSpawn { get => maxSpawn; }
    public float CooldownSpawn { get => cooldownSpawn; }
    public float EnemyLineSpeed { get => enemyLineSpeed; }
    public float EnemyLineCooldown { get => enemyLineCooldown; }
    public float EnemyDashSpeed { get => enemyDashSpeed; }
    public float EnemyDashFrequency { get => enemyDashFrequency; }
    public float EnemyDashCooldown { get => enemyDashCooldown; }
    public float EnemyCircleSpeed { get => enemyCircleSpeed; }
    public float EnemyCircleFrequency { get => enemyCircleFrequency; }
    public float EnemyCircleCooldown { get => enemyCircleCooldown; }
    public float EnemyChargeSpeed { get => enemyChargeSpeed; }
    public bool EnemyBossSpawn { get => enemyBossSpawn; }
    public float EnemyBossFrequency { get => enemyBossFrequency; }
    public float EnemyBossCooldown { get => enemyBossCooldown; }
    public float EnemyBossRushSpeed { get => enemyBossRushSpeed; }
    public float EnemyBossRushFrequency { get => enemyBossRushFrequency; }
    public float EnemyBossRushCooldown { get => enemyBossRushCooldown; }
}

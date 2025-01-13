using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SceneControlScript : MonoBehaviour
{
    Transform gameMenu;
    private bool inPause;
    private bool gameOver;
    private bool gameVictory;
    private GameObject player;
    private GameObject[] enemies;
    private GameObject boss;
    private GameObject[] bullets;
    private GameObject[] playerBullets;
    private Wave wave;
    private BackgroundMovement[] background;

    public AudioMixer audioMixer;
    public AudioClip mainTheme;
    public AudioClip bossTheme;
    public AudioClip deathTheme;
    public AudioClip victoryTheme;

    Transform screenRes;
    Transform masterVol;
    Transform musicVol;
    Transform sfxVol;

    private Level currentLevel;

    // Start is called before the first frame update
    void Awake()
    {
        inPause = false;
        gameOver = false;
        gameVictory = false;
        gameMenu = GameObject.Find("GameMenu").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        wave = GameObject.Find("Wave").GetComponent<Wave>();
        background = GameObject.Find("Backgrounds").GetComponentsInChildren<BackgroundMovement>();
        currentLevel = new Level(1);

        GetComponent<AudioSource>().PlayOneShot(mainTheme);
    }

    // Update is called once per frame
    void Update()
    {
        PauseMode();
    }

    private void PauseMode ()
    {
        if (Input.GetButtonDown("Cancel") && !gameOver && !gameVictory)
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
                gameMenu.GetChild(0).gameObject.SetActive(true);
                gameMenu.GetChild(1).gameObject.SetActive(true);

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
        if (!gameMenu.GetChild(4).gameObject.active)
        {
            inPause = false;
            gameMenu.GetChild(0).gameObject.SetActive(false);
            gameMenu.GetChild(1).gameObject.SetActive(false);

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
        else
        {
            gameMenu.GetChild(4).gameObject.SetActive(false);
            gameMenu.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void Rematch()
    {
        SceneManager.LoadScene(2);
    }

    public void Option()
    {
        gameMenu.GetChild(1).gameObject.SetActive(false);
        gameMenu.GetChild(4).gameObject.SetActive(true);

        screenRes = gameMenu.GetChild(4).GetChild(2);
        masterVol = gameMenu.GetChild(4).GetChild(3);
        musicVol = gameMenu.GetChild(4).GetChild(4);
        sfxVol = gameMenu.GetChild(4).GetChild(5);
        /* CARICAMENTO VALORI DELLE OPZIONI */
        List<string> resolutions = new List<string>();
        int index = 0;
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            resolutions.Add(Screen.resolutions[i].ToString());

            if (Screen.resolutions[i].ToString().Equals(Screen.currentResolution.ToString()))
            {
                index = i;
            }
               
        }
        screenRes.GetComponentInChildren<Dropdown>().AddOptions(resolutions);
        screenRes.GetComponentInChildren<Dropdown>().value = index;

        float level = 0;
        audioMixer.GetFloat("Master", out level);
        masterVol.GetComponentInChildren<Slider>().value = level;
        masterVol.GetChild(1).GetComponent<Text>().text = ((int)level).ToString();
        audioMixer.GetFloat("Music", out level);
        musicVol.GetComponentInChildren<Slider>().value = level;
        musicVol.GetChild(1).GetComponent<Text>().text = ((int)level).ToString();
        audioMixer.GetFloat("SFX", out level);
        sfxVol.GetComponentInChildren<Slider>().value = level;
        sfxVol.GetChild(1).GetComponent<Text>().text = ((int)level).ToString();
    }

    public void ChangeOptions(Transform option)
    {
        if (option.parent.name.Equals("ScreenRes"))
        {
            /* Cambio risoluzione **TESTARE DOPO BUILD** */
            string res = option.GetComponent<Dropdown>().options[option.GetComponent<Dropdown>().value].text;
            string[] temp = res.Split((' '));
            Screen.SetResolution(int.Parse(temp[0]), int.Parse(temp[2]), FullScreenMode.ExclusiveFullScreen, int.Parse(temp[4].Substring(0,2)));
            Debug.Log(int.Parse(temp[0]) + " " + int.Parse(temp[2]) + " " + int.Parse(temp[4].Substring(0, 2)));
        }
        else if (option.parent.name.Equals("MasterVol"))
        {
            audioMixer.SetFloat("Master", masterVol.GetComponentInChildren<Slider>().value);
            masterVol.GetChild(1).GetComponent<Text>().text = masterVol.GetComponentInChildren<Slider>().value.ToString();
        }
        else if (option.parent.name.Equals("MusicVol"))
        {
            audioMixer.SetFloat("Music", musicVol.GetComponentInChildren<Slider>().value);
            musicVol.GetChild(1).GetComponent<Text>().text = musicVol.GetComponentInChildren<Slider>().value.ToString();
        }
        else if (option.parent.name.Equals("SFXVol"))
        {
            audioMixer.SetFloat("SFX", sfxVol.GetComponentInChildren<Slider>().value);
            sfxVol.GetChild(1).GetComponent<Text>().text = sfxVol.GetComponentInChildren<Slider>().value.ToString();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void GameOverMode()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        bullets = GameObject.FindGameObjectsWithTag("Bullet");

        if (GameObject.Find("Boss") != null)
            boss = GameObject.Find("Boss").transform.GetChild(0).gameObject;
        else
            boss = null;

        gameOver = true;
        gameMenu.GetChild(0).gameObject.SetActive(true);
        gameMenu.GetChild(2).gameObject.SetActive(true);

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

        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(deathTheme);
        GetComponent<AudioSource>().loop = false;
    }

    /* SOSTITUIRE CON IMMAGINI POST VICTORY */
    public void GameVictoryMode()
    {
        gameVictory = true;
        gameMenu.GetChild(0).gameObject.SetActive(true);
        gameMenu.GetChild(3).gameObject.SetActive(true);

        player.GetComponent<PlayerClass>().PauseOn();

        wave.PauseOn();
        for (int i = 0; i < background.Length; i++)
            background[i].PauseOn();

        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(victoryTheme);
        GetComponent<AudioSource>().loop = false;
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
    float enemyDashAmplitude;
    float enemyDashCooldown;

    float enemyCircleSpeed;
    float enemyCircleFrequency;
    float enemyCircleAmplitude;
    float enemyCircleCooldown;

    float enemyChargeSpeed;

    bool enemyBossSpawn;
    float enemyBossFrequency;
    float enemyBossAmplitude;
    float enemyBossCooldown;
    float enemyBossRushSpeed;
    float enemyBossRushFrequency;
    float enemyBossRushAmplitude;
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
        enemyDashAmplitude = 0f;
        enemyDashCooldown = 0f;
        enemyCircleSpeed = 0f;
        enemyCircleFrequency = 0f;
        enemyCircleAmplitude = 0f;
        enemyCircleCooldown = 0f;
        enemyChargeSpeed = 0f;
        enemyBossSpawn = false;
        enemyBossFrequency = 0f;
        enemyBossAmplitude = 0f;
        enemyBossCooldown = 0f;
        enemyBossRushSpeed = 0f;
        enemyBossRushFrequency = 0f;
        enemyBossRushAmplitude = 0f;
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
                    enemyDashFrequency = 7f;
                    enemyDashAmplitude = 0.3f;
                    enemyDashCooldown = 5f;

                    enemyCircleSpeed = 0.5f;
                    enemyCircleFrequency = 6.5f;
                    enemyCircleAmplitude = 0.4f;
                    enemyCircleCooldown = 5f;

                    enemyChargeSpeed = 0.5f;
                }
                break;

            case 2:
                {
                    minSpawn = 4;
                    maxSpawn = 6;
                    cooldownSpawn = 7f;

                    enemyLineSpeed = 0.5f;
                    enemyLineCooldown = 2f;

                    enemyDashSpeed = 0.6f;
                    enemyDashFrequency = 5.5f;
                    enemyDashAmplitude = 0.3f;
                    enemyDashCooldown = 4f;

                    enemyCircleSpeed = 0.6f;
                    enemyCircleFrequency = 5.5f;
                    enemyCircleAmplitude = 0.3f;
                    enemyCircleCooldown = 3f;

                    enemyChargeSpeed = 1.3f;
                }
                break;

            case 3:
                {
                    minSpawn = 4;
                    maxSpawn = 6;
                    cooldownSpawn = 4f;

                    enemyLineSpeed = 0.6f;
                    enemyLineCooldown = 2f;

                    enemyDashSpeed = 0.7f;
                    enemyDashFrequency = 5.5f;
                    enemyDashAmplitude = 0.3f;
                    enemyDashCooldown = 3f;

                    enemyCircleSpeed = 0.7f;
                    enemyCircleFrequency = 5.5f;
                    enemyCircleAmplitude = 0.3f;
                    enemyCircleCooldown = 3f;

                    enemyChargeSpeed = 1.5f;
                }
                break;

            case 4:
                {
                    minSpawn = 4;
                    maxSpawn = 5;
                    cooldownSpawn = 7f;

                    enemyLineSpeed = 0.5f;
                    enemyLineCooldown = 2f;

                    enemyDashSpeed = 0.6f;
                    enemyDashFrequency = 7f;
                    enemyDashAmplitude = 0.3f;
                    enemyDashCooldown = 4f;

                    enemyCircleSpeed = 0.6f;
                    enemyCircleFrequency = 6.5f;
                    enemyCircleAmplitude = 0.4f;
                    enemyCircleCooldown = 3f;

                    enemyChargeSpeed = 1f;

                    enemyBossSpawn = true;
                    enemyBossFrequency = 0.8f;
                    enemyBossAmplitude = 0.5f;
                    enemyBossCooldown = 4f;
                    enemyBossRushSpeed = 1.5f;
                    enemyBossRushFrequency = 3f;
                    enemyBossRushAmplitude = 0.5f;
                    enemyBossRushCooldown = 8f;
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
    public float EnemyDashAmplitude { get => enemyDashAmplitude; }
    public float EnemyDashCooldown { get => enemyDashCooldown; }
    public float EnemyCircleSpeed { get => enemyCircleSpeed; }
    public float EnemyCircleFrequency { get => enemyCircleFrequency; }
    public float EnemyCircleAmplitude { get => enemyCircleAmplitude; }
    public float EnemyCircleCooldown { get => enemyCircleCooldown; }
    public float EnemyChargeSpeed { get => enemyChargeSpeed; }
    public bool EnemyBossSpawn { get => enemyBossSpawn; }
    public float EnemyBossFrequency { get => enemyBossFrequency; }
    public float EnemyBossAmplitude { get => enemyBossAmplitude; }
    public float EnemyBossCooldown { get => enemyBossCooldown; }
    public float EnemyBossRushSpeed { get => enemyBossRushSpeed; }
    public float EnemyBossRushFrequency { get => enemyBossRushFrequency; }
    public float EnemyBossRushAmplitude { get => enemyBossRushAmplitude; }
    public float EnemyBossRushCooldown { get => enemyBossRushCooldown; }
}

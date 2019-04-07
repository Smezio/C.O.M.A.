using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlScript : MonoBehaviour
{
    Transform pauseMenu;
    private bool inPause;
    private PlayerClass player;
    private GameObject[] enemies;
    private GameObject[] bullets;
    private GameObject[] playerBullets;

    // Start is called before the first frame update
    void Awake()
    {
        inPause = false;
        pauseMenu = GameObject.Find("PauseMenu").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>();
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

            if (!inPause)
            {
                inPause = true;
                pauseMenu.GetChild(0).gameObject.SetActive(true);
                pauseMenu.GetChild(1).gameObject.SetActive(true);

                for (int i = 0; i < enemies.Length; i++)
                    enemies[i].GetComponent<EnemyClass>().PauseOn();
                for (int i = 0; i < bullets.Length; i++)
                    bullets[i].GetComponent<BulletClass>().PauseOn();
                player.PauseOn();
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
        player.PauseOff();
    }

    public void Rematch()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseMode()
    {
        //Imposta le proprietà (canShoot, canMove) degli oggetti nella scena a false

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    protected string enemyType;
    public float healthPoint;
    protected float speed;
    protected string bulletType;
    protected bool immune;
    protected int score;

    protected bool canMove;
    protected bool canShoot;
    protected bool nextFire;
    protected float resetTime;
    protected float cooldown;

    protected float pauseStart;
    protected float pauseFinish;

    protected Vector3 screenDimension;
    public AudioClip deathEnemyAudio;

    // Start is called before the first frame update
    void Awake()
    {
        canMove = true;
        canShoot = true;

        pauseStart = 0;
        pauseFinish = 0;

        screenDimension = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Shoot ()
    {
        if (canShoot)
        {
            if ((Time.timeSinceLevelLoad - (pauseFinish - pauseStart)) > resetTime)
                nextFire = true;

            if (nextFire && (Time.timeSinceLevelLoad - (pauseFinish - pauseStart)) > resetTime)
            {
                pauseStart = 0;
                pauseFinish = 0;

                GetComponent<Animator>().SetBool("ShootClick", true);

                resetTime = Time.timeSinceLevelLoad + cooldown;
            }
        }
    }

    protected void DisableShootAnimation()
    {
        GetComponent<Animator>().SetBool("ShootClick", false);
    }

    public void TakeDamage (float damage)
    {
        if (!immune)
            healthPoint -= damage;

        if (healthPoint <= 0)
        {
            canShoot = false;
            GetComponent<BoxCollider2D>().enabled = false;

            if (transform.name.Equals("BossCore"))
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
                GameObject.Find("Wave").SetActive(false);

                for (int i = 0; i < enemies.Length; i++)
                    enemies[i].GetComponent<Animator>().SetBool("Death", true);
                for (int i = 0; i < bullets.Length; i++)
                    Destroy(bullets[i]);
                GetComponent<Animator>().SetBool("Death", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("Death", true);
                GetComponent<AudioSource>().PlayOneShot(deathEnemyAudio, 0.5f);
            }
        }
    }

    protected void DestroyEnemy ()
    {
        GameObject.Destroy(transform.parent.gameObject);
    }

    virtual public void PauseOn()
    {
        canShoot = false;
        canMove = false;
        pauseStart = Time.timeSinceLevelLoad;
        GetComponent<Animator>().speed = 0f;
    }

    virtual public void PauseOff()
    {
        canShoot = true;
        canMove = true;
        pauseFinish = Time.timeSinceLevelLoad;
        GetComponent<Animator>().speed = 1f;
    }

    protected void CheckBounds()
    {
        if (transform.parent.position.x >= screenDimension.x ||
            transform.parent.position.x <= -screenDimension.x ||
            transform.parent.position.y >= screenDimension.y ||
            transform.parent.position.y <= -screenDimension.y)
            canShoot = false;
        else
            canShoot = true;
        
        if (transform.parent.position.x > 2.1f || 
            transform.parent.position.x< -2.1f || 
            transform.parent.position.y> 1.4f || 
            transform.parent.position.y< -1.4f)
            GameObject.Destroy(transform.parent.gameObject);
    }
    
    public int Score
    {
        get { return score; }
    }

    public float Speed { get => speed; set => speed = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }
}

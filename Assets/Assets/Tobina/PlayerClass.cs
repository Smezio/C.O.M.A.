﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerClass : MonoBehaviour
{
    /* Status properties */
    private float healthPoint;
    private bool immune;
    private float resetTimeImmune;
    public float cooldownImmune;
    private int score;

    /* Movement properties */
    private bool canMove;
    public float speed;
    private Rigidbody2D rb2d;
    private BoxCollider2D tobinaCollider;
    private SpriteRenderer tobinaSprite;
    private Vector2 vel;
    private bool facing;

    /* Shoot properties */
    private bool canShoot;
    public GameObject defaultBullet;
    private Transform rightBulletSpawn;
    private Transform leftBulletSpawn;
    private bool nextFire;
    private float resetTimeShoot;
    public float cooldownShoot;

    /* Parry properties */
    private bool canParry;
    private float resetTimeParry;
    public float cooldownParry;

    private float pauseStart;
    private float pauseFinish;

    public AudioClip damageAudio;
    public AudioClip shootAudio;
    public AudioClip parryAudio;

    void Start()
    {
        healthPoint = 4;
        immune = true;
        score = 0;

        canMove = true;
        rb2d = GetComponent<Rigidbody2D>();
        tobinaCollider = GetComponent<BoxCollider2D>();
        tobinaSprite = GetComponent<SpriteRenderer>();
        facing = false;

        canShoot = true;
        rightBulletSpawn = transform.GetChild(0);
        leftBulletSpawn = transform.GetChild(1);

        canParry = true;

        pauseStart = 0;
        pauseFinish = 0;
    }

    void Update()
    {
        Movement();
        Shoot();
        Parry();
        CountImmuneTime();
        ScoreControl();
    }

    public void Movement()
    {
        if (canMove)
        {
            // Move
            vel.x = Input.GetAxisRaw("Horizontal");
            vel.y = Input.GetAxisRaw("Vertical");
            rb2d.velocity = vel * speed * Time.deltaTime;

            // Clamp inside camera view
            var pos = rb2d.position;
            pos.x = Mathf.Clamp(pos.x, -1.6f, 1.6f);
            pos.y = Mathf.Clamp(pos.y, -0.9f, 0.9f);
            rb2d.position = pos;

            // Flip sprite according to velocity
            if (vel.x != 0)
                facing = vel.x < 0 ? true : false;

            tobinaSprite.flipX = facing;
        }
        else
        {
            vel.x = 0;
            vel.y = 0;
            rb2d.velocity = vel;
        }
    }
    
    /* Accesso e uscita dalla PauseMode */
    public void PauseOn()
    {
        canShoot = false;
        canMove = false;
        canParry = false;
        pauseStart = Time.timeSinceLevelLoad;
        GetComponent<Animator>().speed = 0f;
    }

    public void PauseOff()
    {
        canShoot = true;
        canMove = true;
        canParry = true;
        pauseFinish = Time.timeSinceLevelLoad;
        GetComponent<Animator>().speed = 1f;
    }

    /* Collisioni power up */
    public void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if (collision.gameObject.tag != "PowerUp")
            return;

        if (collision.gameObject.name == "Cookie")
        {
            Debug.Log("Cookie");

            healthPoint++;
            if (healthPoint > maxhealthPoint)
                healthPoint = maxhealthPoint;
        }
        else if (collision.gameObject.name == "TeddyBear")
        {
            Debug.Log("Teddy");
            immune = true;
        }

        Destroy(collision.gameObject);*/

        /* Danni da contatto */
        if (collision.gameObject.tag.Equals("Enemy"))
            TakeDamage(1f);
    }

    /* Calcolo dei punti vita e controllo della morte */
    public void TakeDamage(float damage)
    {
        if (!immune && healthPoint > 0)
        {
            healthPoint -= damage;
            resetTimeImmune = Time.timeSinceLevelLoad + cooldownImmune;
            immune = true;
            GetComponent<AudioSource>().PlayOneShot(damageAudio, 0.7f);
            GetComponent<Animator>().SetLayerWeight(1, 1);
        }

        if (healthPoint <= 0)
            GetComponent<Animator>().SetBool("Death", true);
    }

    private void DestroyTobina()
    {
        Camera.main.GetComponent<SceneControlScript>().GameOverMode();
        GameObject.Destroy(this.gameObject);
    }
    
    /* Attiva lo sparo */
    private void Shoot()
    {
        if (canShoot)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if ((Time.timeSinceLevelLoad - (pauseFinish - pauseStart)) > resetTimeShoot)
                    nextFire = true;

                if (nextFire && (Time.timeSinceLevelLoad - (pauseFinish - pauseStart)) > resetTimeShoot)
                {
                    canParry = false;
                    pauseStart = 0;
                    pauseFinish = 0;

                    GetComponent<Animator>().SetBool("ShootClick", true);

                    resetTimeShoot = Time.timeSinceLevelLoad + cooldownShoot;
                }
            }
        }
    }

    /* Genera il proiettle */
    private void InstantiateBullet()
    {
        if (nextFire)
        {
            // Spawn Bullet
            var trans = facing ? leftBulletSpawn : rightBulletSpawn;
            GameObject obj = Instantiate(defaultBullet, trans.position, trans.rotation);

            // Flip bullet
            obj.GetComponent<SpriteRenderer>().flipX = facing;

            nextFire = false;

            GetComponent<AudioSource>().PlayOneShot(shootAudio, 0.7f);
        }
    }

    private void DisableShootAnimation()
    {
        GetComponent<Animator>().SetBool("ShootClick", false);
        canParry = true;
    }

    /* Attiva il parry */
    private void Parry()
    {
        if (canParry)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if ((Time.timeSinceLevelLoad - (pauseFinish - pauseStart)) > resetTimeParry)
                {
                    canShoot = false;
                    pauseStart = 0;
                    pauseFinish = 0;

                    GetComponent<Animator>().SetBool("ParryClick", true);
                    GetComponent<AudioSource>().PlayOneShot(parryAudio, 0.7f);

                    resetTimeParry = Time.timeSinceLevelLoad + cooldownParry;
                }
            }
        }
    }

    public void DisableParryAnimation()
    {
        GetComponent<Animator>().SetBool("ParryClick", false);
        canShoot = true;
    }

    private void CountImmuneTime()
    {
        if (immune && (Time.timeSinceLevelLoad - (pauseFinish - pauseStart)) > resetTimeImmune)
        {
            immune = false;
            GetComponent<Animator>().SetLayerWeight(1, 0);
        }
    }

    /* Confronta lo score del player con quello nella UI */
    private void ScoreControl()
    {
        if (!GameObject.Find("Score").GetComponent<Text>().text.Equals(score.ToString()))
            GameObject.Find("Score").GetComponent<Text>().text = score.ToString();
    }


    public bool Facing
    {
        get { return facing; }
    }

    public bool CanShoot
    {
        get { return canShoot; }
    }

    public bool CanParry
    {
        get { return canParry; }
    }

    public float HealthPoint
    {
        get { return healthPoint; }
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }
}

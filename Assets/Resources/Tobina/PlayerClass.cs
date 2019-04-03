using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    /* Status properties */
    public float maxhealthPoint = 4;
    private float healthPoint;
    private bool immune;
    public float immuneDuration = 10; // sec
    private float immuneTimer;

    /* Movement properties */
    public float speed;
    private Rigidbody2D rb2d;
    private BoxCollider2D tobinaCollider;
    private SpriteRenderer tobinaSprite;
    private Vector2 vel;
    private bool facing;

    /* Shoot properties */
    public GameObject defaultBullet;
    private Transform rightBulletSpawn;
    private Transform leftBulletSpawn;
    private bool NextFire;
    public float cooldown;
    private float time;
    private AudioSource audioShoot;

    void Start()
    {
        healthPoint = maxhealthPoint;
        immuneTimer = immuneDuration;

        rb2d = GetComponent<Rigidbody2D>();
        tobinaCollider = GetComponent<BoxCollider2D>();
        tobinaSprite = GetComponent<SpriteRenderer>();
        facing = false;

        rightBulletSpawn = transform.GetChild(0);
        leftBulletSpawn = transform.GetChild(1);
        time = 2f;
        audioShoot = GetComponent<AudioSource>();
    }

    void Update()
    {
        Movement();
        ImmuneControl();
        Shoot();
    }

    public void Movement()
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

    public bool GetFacing()
    {
        return facing;
    }

    public void DisableParryAnimation()
    {
        GetComponent<Animator>().SetBool("ParryClick", false);
    }

    /* Collisioni power up */
    public void OnCollisionEnter2D(Collision2D collision)
    {
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

        Destroy(collision.gameObject);
    }

    public void TakeDamage(float damage)
    {
        if (immune)
            return;

        healthPoint -= damage;

        if (healthPoint <= 0)
            GetComponent<Animator>().SetBool("Death", true);
    }

    public void ImmuneControl()
    {
        if (immune)
        {
            immuneTimer -= Time.deltaTime;
            if (immuneTimer <= 0)
                immune = false;
        }
    }

    protected void DestroyTobina()
    {
        GameObject.Destroy(this.gameObject);
    }

    public float GetHealthPoint()
    {
        return healthPoint;
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time > cooldown)
                NextFire = true;

            if (NextFire && Time.time > cooldown)
            {
                GetComponent<Animator>().SetBool("ShootClick", true);

                cooldown = Time.time + time;
            }
        }
    }

    /* AGGIUNGERE EVENTO AD ANIMAZIONE */
    protected void DisableShootAnimation()
    {
        GetComponent<Animator>().SetBool("ShootClick", false);
    }

    protected void InstantiateBullet()
    {
        if (NextFire)
        {
            // Spawn Bullet
            var trans = facing ? leftBulletSpawn : rightBulletSpawn;
            GameObject obj = Instantiate(defaultBullet, trans.position, trans.rotation);

            // Flip bullet
            obj.GetComponent<SpriteRenderer>().flipX = facing;

            NextFire = false;

            audioShoot.Play();
        }
    }
}

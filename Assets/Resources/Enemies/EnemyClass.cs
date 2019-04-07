using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    protected string enemyType;
    public float healthPoint;
    public float speed;
    protected string bulletType;
    protected bool immune;
    protected int score;

    protected bool canMove;
    protected bool canShoot;
    protected bool nextFire;
    protected float resetTime;
    public float cooldown;

    private float pauseStart;
    private float pauseFinish;

    // Start is called before the first frame update
    void Awake()
    {
        canMove = true;
        canShoot = true;

        pauseStart = 0;
        pauseFinish = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void Shoot ()
    {
        if (canShoot)
        {
            if ((Time.time - (pauseFinish - pauseStart)) > resetTime)
                nextFire = true;

            if (nextFire && (Time.time - (pauseFinish - pauseStart)) > resetTime)
            {
                pauseStart = 0;
                pauseFinish = 0;

                GetComponent<Animator>().SetBool("ShootClick", true);

                resetTime = Time.time + cooldown;
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
            GetComponent<Animator>().SetBool("Death", true);
    }

    protected void DestroyEnemy ()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void PauseOn()
    {
        canShoot = false;
        canMove = false;
        pauseStart = Time.time;
        GetComponent<Animator>().enabled = false;
    }

    public void PauseOff()
    {
        canShoot = true;
        canMove = true;
        pauseFinish = Time.time;
        GetComponent<Animator>().enabled = true;
    }

    protected void CheckBounds()
    {
        var pos = transform.position;
        if (pos.x > 2.1f || pos.x< -2.1f || pos.y> 1.4f || pos.y< -1.4f)
            GameObject.Destroy(gameObject);
    }


    public int Score
    {
        get { return score; }
    }
}

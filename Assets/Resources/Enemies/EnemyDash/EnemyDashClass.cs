using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashClass : EnemyClass
{
    public new string bulletType;
    Vector3 pos;
    float t;
    float frequency;
    float amplitude;

    void Awake()
    {
        enemyType = "Dash";
        healthPoint = 2;
        speed = 1;
        immune = false;
        cooldown = 0.5f;
        score = 20;

        frequency = 10f;
        amplitude = 0.2f;
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotation();
        Shoot();
    }

    private void Movement()
    {
        if (healthPoint > 0)
        {
            pos.y = pos.y + amplitude * Mathf.Sin(t);
            pos.x += speed * Time.deltaTime;
            t += Time.deltaTime * frequency;
            transform.position = pos;
        }
    }

    private void Rotation()
    {
        if (GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    protected void InstantiateBullet()
    {
        if (nextFire)
        {
            if (bulletType.Equals("Direct"))
                Instantiate(Resources.Load("Enemies/Bullets/DirectBullet"), transform.GetChild(0).position, transform.GetChild(0).rotation);
            else if (bulletType.Equals("4Dir"))
                Instantiate(Resources.Load("Enemies/Bullets/AllDirBullet"), transform.GetChild(0).position, transform.GetChild(0).rotation);
            nextFire = false;
        }
    }
}

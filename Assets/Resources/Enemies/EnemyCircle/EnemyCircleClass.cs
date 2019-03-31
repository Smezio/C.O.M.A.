using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleClass : EnemyClass
{
    Vector3 pos;
    float centerx;
    float centery;
    float amplitude;
    float speedRotation;
    float t;

    void Awake()
    {
        enemyType = "Circle";
        healthPoint = 2;
        speed = 0.5f;
        bulletType = "Direct";
        immune = false;
        cooldown = 0.5f;
        score = 30;

        pos = transform.position;
        centerx = pos.x;
        centery = pos.y;
        speedRotation = 5f;
        amplitude = 0.5f;
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
            pos.x = centerx + amplitude * Mathf.Cos(t);
            pos.y = centery + amplitude * Mathf.Sin(t);
            t += Time.deltaTime * speedRotation;
            centerx += Time.deltaTime * speed;
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
            Instantiate(Resources.Load("Enemies/Bullets/DirectBullet"), transform.GetChild(0).position, transform.GetChild(0).rotation);
            nextFire = false;
        }
    }
}

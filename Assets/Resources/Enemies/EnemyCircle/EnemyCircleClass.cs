using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleClass : EnemyClass
{
    private Vector3 pos;
    private float centerx;
    private float centery;
    public float amplitude;
    public float speedRotation;
    private float t;

    void Awake()
    {
        enemyType = "Circle";
        bulletType = "Direct";
        immune = false;

        pos = transform.position;
        centerx = pos.x;
        centery = pos.y;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
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

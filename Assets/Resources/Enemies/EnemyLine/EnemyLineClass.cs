using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineClass : EnemyClass
{

    Vector3 direction;

    void Awake()
    {
        enemyType = "Inline";
        healtPoint = 2;
        speed = 1;
        bulletType = "Inline";
        immune = false;
        cooldown = 0.8f;
        score = 10;
    }

    void Start()
    {
        direction = Movement();
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
        Shoot();
        transform.Translate(direction.normalized * Time.deltaTime * speed);
    }

    private void Rotation()
    {
        if (GameObject.FindGameObjectWithTag("Player").transform.position.y > transform.position.y)
        {
             transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }

    private Vector3 Movement()
    {
        Vector3 dir;
        if (GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x)
        {
            dir = Vector3.up;
        }
        else
        {
            dir = Vector3.down;
        }

        return dir;
    }

    protected void InstantiateBullet()
    {
        if (nextFire)
        {
            Instantiate(Resources.Load("Enemies/Bullets/InlineBullet"), transform.GetChild(0).position, transform.GetChild(0).rotation);
            nextFire = false;
        }
    }
}

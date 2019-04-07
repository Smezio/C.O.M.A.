using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineClass : EnemyClass
{

    Vector3 direction;

    void Start()
    {
        enemyType = "Inline";
        bulletType = "Inline";
        immune = false;
        score = 10;

        direction = Facing();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
        Movement();
        Shoot();
    }

    private void Movement()
    {
        if (canMove)
        {
            if (healthPoint > 0)
                transform.Translate(direction.normalized * Time.deltaTime * speed);

            if (GameObject.FindGameObjectWithTag("Player").transform.position.y > transform.position.y)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
        }
    }

    private Vector3 Facing()
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

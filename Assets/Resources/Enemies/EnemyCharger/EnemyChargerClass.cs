using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargerClass : EnemyClass
{
    Vector3 target;

    void Awake()
    {
        enemyType = "Charger";
        healtPoint = 2;
        speed = 3f;
        bulletType = "Deflect";
        immune = true;
        cooldown = 0.5f;
        score = 10000;

        target = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        Rotation();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }

    private void Movement()
    {
        transform.Translate(target*Time.deltaTime *speed );
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
}

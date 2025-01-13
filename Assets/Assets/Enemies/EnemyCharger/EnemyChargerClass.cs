using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargerClass : EnemyClass
{
    Vector3 target;

    void Start()
    {
        enemyType = "Charger";
        bulletType = "Deflect";
        immune = true;
        score = 100;
        
        target = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        Direction();

        speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
        Movement();
    }

    private void Movement()
    {
        if (canMove)
        {
            if (healthPoint > 0)
                transform.parent.Translate(target * Time.deltaTime * speed);
        }
    }

    private void Direction()
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

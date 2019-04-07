using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashClass : EnemyClass
{
    private Vector3 pos;
    private float t;
    public float frequency;
    public float amplitude;

    void Start()
    {
        enemyType = "Dash";
        bulletType = "Direct";
        immune = false;
        score = 30;

        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
        Shoot();
        Movement();
    }

    private void Movement()
    {
        if (canMove)
        {
            if (healthPoint > 0)
            {
                pos.y = pos.y + amplitude * Mathf.Sin(t);
                pos.x += speed * Time.deltaTime;
                t += Time.deltaTime * frequency;
                transform.position = pos;
            }

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

    protected void InstantiateBullet()
    {
        if (nextFire)
        {
            Instantiate(Resources.Load("Enemies/Bullets/DirectBullet"), transform.GetChild(0).position, transform.GetChild(0).rotation);
            nextFire = false;
        }
    }
}

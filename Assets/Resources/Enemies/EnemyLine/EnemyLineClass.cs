using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineClass : EnemyClass
{

    void Start()
    {
        enemyType = "Inline";
        bulletType = "Inline";
        immune = false;
        score = 10;
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
                transform.parent.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }

    public void Facing(string spawnPoint)
    {
        if (spawnPoint.Contains("R"))
        {
            transform.parent.Rotate(Vector3.forward, 180f);
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<SpriteRenderer>().flipY = true;
        }
        else if (spawnPoint.Contains("L"))
        {
            transform.parent.Rotate(Vector3.forward, 0f);
            transform.Rotate(Vector3.forward, 0f);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (spawnPoint.Contains("U"))
        {
            if (GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x)
            {
                transform.parent.Rotate(Vector3.forward, -90f);
                transform.Rotate(Vector3.forward, 90f);
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                transform.parent.Rotate(Vector3.forward, -90f);
                transform.Rotate(Vector3.forward, -90f);
                GetComponent<SpriteRenderer>().flipX = true;
                GetComponent<SpriteRenderer>().flipY = true;
            }

        }
        else if (spawnPoint.Contains("D"))
        {
            if (GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x)
            {
                transform.parent.Rotate(Vector3.forward, 90f);
                transform.Rotate(Vector3.forward, -90f);
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                transform.parent.Rotate(Vector3.forward, 90f);
                transform.Rotate(Vector3.forward, 90f);
                GetComponent<SpriteRenderer>().flipX = true;
                GetComponent<SpriteRenderer>().flipY = true;
            }
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashClass : EnemyClass
{
    private Vector3 pos;
    private float t;
    private float frequency;
    private float amplitude;

    void Start()
    {
        enemyType = "Dash";
        bulletType = "Direct";
        immune = false;

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
                transform.parent.transform.Translate(Vector3.right * speed * Time.deltaTime);
                pos.y = transform.parent.position.y + amplitude * Mathf.Sin(t);
                pos.x = transform.parent.position.x;
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

    public void Facing(string spawnPoint)
    {
        float angle = 0f;
        if (spawnPoint.Contains("R"))
        {
            if (spawnPoint.Substring(spawnPoint.Length - 1).Equals("1") || spawnPoint.Substring(spawnPoint.Length - 1).Equals("3"))
                angle = Random.Range(200f, 160f);
            else if (spawnPoint.Substring(spawnPoint.Length - 1).Equals("2"))
                angle = Random.Range(210f, 150f);
        }
        else if (spawnPoint.Contains("L"))
        {
            if (spawnPoint.Substring(spawnPoint.Length - 1).Equals("1") || spawnPoint.Substring(spawnPoint.Length - 1).Equals("3"))
                angle = Random.Range(20f, -20f);
            else if (spawnPoint.Substring(spawnPoint.Length - 1).Equals("2"))
                angle = Random.Range(30f, -30f);
        }
        else if (spawnPoint.Contains("U") || spawnPoint.Contains("D"))
        {
            switch (spawnPoint.Substring(spawnPoint.Length - 1))
            {
                case "1":
                    angle = Random.Range(45f, 90f);
                    break;

                case "2":
                    angle = Random.Range(45f, 120f);
                    break;

                case "3":
                    angle = Random.Range(45f, 135f);
                    break;

                case "4":
                    angle = Random.Range(60f, 135f);
                    break;

                case "5":
                    angle = Random.Range(90f, 135f);
                    break;
            }

            if (spawnPoint.Contains("U"))
                angle = -angle;
        }

        transform.parent.Rotate(Vector3.forward, angle);
        transform.Rotate(Vector3.forward, -angle);
    }

    protected void InstantiateBullet()
    {
        if (nextFire)
        {
            GameObject bullet = Instantiate(Resources.Load("Enemies/Bullets/DirectBullet"), transform.GetChild(0).position, transform.GetChild(0).rotation) as GameObject;
            bullet.GetComponent<BulletClass>().Shooter = gameObject;
            nextFire = false;
        }
    }

    public float Frequency { get => frequency; set => frequency = value; }
    public float Amplitude { get => amplitude; set => amplitude = value; }
}

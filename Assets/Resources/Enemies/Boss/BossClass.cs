using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClass : EnemyClass
{
    Vector3 direction;
    bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        enemyType = "Boss";
        bulletType = "Direct";
        immune = false;

        canShoot = false;
        direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (canShoot)
            Shoot();
    }

    protected void EntranceFinish()
    {
        GetComponent<Animator>().SetBool("Entrance", false);
        GetComponent<Animator>().SetBool("RushClick", false);
        direction = Vector3.up;
        canShoot = true;
    }

    protected void Rush ()
    {
        canShoot = false;
        GetComponent<Animator>().SetBool("RushClick", true);
    }

    protected void Movement ()
    {
        transform.Translate(direction.normalized * Time.deltaTime * speed);
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

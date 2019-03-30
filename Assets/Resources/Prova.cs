using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prova : Enemy
{
    float time = 2f;
    float cooldown = 0;
    bool NextFire = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            Shoot();
        }
    }

    protected void Shoot()
    {
        if (Time.time > cooldown)
            NextFire = true;

        if (NextFire)
        {
            GetComponent<Animator>().SetBool("ShootClick", true);
            GetComponent<Animator>().Play("Shoot");
            GetComponent<Animator>().SetBool("ShootClick", false);

            cooldown = Time.time + time;
            NextFire = false;

            Instantiate(Resources.Load("Bullet/Bullet"), transform.GetChild(0).position, transform.GetChild(0).rotation);
        }
    }
}

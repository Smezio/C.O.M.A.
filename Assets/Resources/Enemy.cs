using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float healtPoint;
    private string enemyType;
    private float speed;
    private string bulletType;

    private bool NextFire;
    private float cooldown;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
            Shoot();
    }

    protected void Shoot ()
    {
        if (Time.time > cooldown)
            NextFire = true;

        if (NextFire)
        {
            GetComponent<Animator>().SetBool("ShootClick", true);
            GetComponent<Animator>().Play("Shoot");
            GetComponent<Animator>().SetBool("ShootClick", false);

            cooldown = Time.time + time;
        }
    }

    protected void InstantiateBullet ()
    {
       if (NextFire)
        {
            Instantiate(Resources.Load("Bullet/Bullet"), transform.GetChild(0).position, transform.GetChild(0).rotation);
            NextFire = false;
        }
    }

    protected float GetHealtPoint ()
    {
        return healtPoint;
    }

    protected string GetEnemyType ()
    {
        return enemyType;
    }

    protected float GetSpeed()
    {
        return speed;
    }

    protected string GetBulletType()
    {
        return bulletType;
    }

    protected void SetHealtPoint (float value)
    {
        healtPoint = value;
    }

    protected void SetEnemyType (string name)
    {
        enemyType = name;
    }

    protected void SetSpeed (float value)
    {
        speed = value;
    }

    protected void SetBulletType (string name)
    {
        bulletType = name;
    }
}

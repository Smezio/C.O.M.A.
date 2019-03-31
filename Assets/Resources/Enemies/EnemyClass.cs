using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    protected string enemyType;
    protected float healthPoint;
    protected float speed;
    protected string bulletType;
    protected bool immune;
    protected int score;

    protected bool nextFire;
    protected float resetTime;
    protected float cooldown;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void Shoot ()
    {
        if (Time.time > resetTime)
            nextFire = true;

        if (nextFire && Time.time > resetTime)
        {
            GetComponent<Animator>().SetBool("ShootClick", true);

            resetTime = Time.time + cooldown;
        }
    }

    protected void DisableShoot()
    {
        GetComponent<Animator>().SetBool("ShootClick", false);
    }


    public void TakeDamage (float damage)
    {
        if (!immune)
            healthPoint -= damage;

        Debug.Log(healthPoint);

        if (healthPoint <= 0)
            GetComponent<Animator>().SetBool("Death", true);
    }

    protected void DestroyEnemy ()
    {
        GameObject.Destroy(this.gameObject);
    }

    protected float GetHealtPoint ()
    {
        return healthPoint;
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

    public void SetHealtPoint (float value)
    {
        healthPoint = value;
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

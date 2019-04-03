using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletClass : MonoBehaviour
{
    protected string type;
    protected float speed;
    protected float damage;
    protected string target;
    protected Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals(target))
        {
            if (target.Equals("Enemy"))
            {
                if (col.gameObject.name.Contains("Line"))
                    col.gameObject.GetComponent<EnemyLineClass>().TakeDamage(damage);
                else if (col.gameObject.name.Contains("Dash"))
                    col.gameObject.GetComponent<EnemyDashClass>().TakeDamage(damage);
                else if (col.gameObject.name.Contains("Circle"))
                    col.gameObject.GetComponent<EnemyCircleClass>().TakeDamage(damage);

                GameObject.Destroy(this.gameObject);
            }
            else if (target.Equals("Player"))
            {
                col.gameObject.GetComponent<PlayerClass>().TakeDamage(damage);
                GameObject.Destroy(this.gameObject);
            }
        }
    }

    protected string GetBulletType ()
    {
        return type;
    }

    protected float GetSpeed()
    {
        return speed;
    }

    protected float GetDamage()
    {
        return damage;
    }

    protected Vector2 GetDirection()
    {
        return direction;
    }

    protected void SetBulletType(string name)
    {
        type = name;
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }

    protected void SetDamage(float value)
    {
        damage = value;
    }

    public void SetDirection(Vector2 values)
    {
        direction = values;
    }
}

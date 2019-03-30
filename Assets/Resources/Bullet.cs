using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private string type;
    private float speed= 0.2f;
    private float damage;
    private GameObject target;
    private Vector3 direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        direction = (GameObject.FindGameObjectWithTag("Enemy").transform.position - transform.position).normalized;

        damage = 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Enemy"))
        {
            col.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            GameObject.Destroy(this.gameObject);
        }
    }

    protected void HitCharacter()
    {

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

    protected void SetSpeed(float value)
    {
        speed = value;
    }

    protected void SetDamage(float value)
    {
        damage = value;
    }

    protected void SetDirection(Vector2 values)
    {
        direction = values;
    }
}

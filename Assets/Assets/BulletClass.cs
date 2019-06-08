using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletClass : MonoBehaviour
{
    protected string type;
    public float speed;
    public float damage;
    protected string target;
    protected GameObject shooter;
    protected Vector3 direction;

    protected bool canMove;

    // Start is called before the first frame update
    void Awake()
    {
        canMove = true;
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
                col.gameObject.GetComponent<EnemyClass>().TakeDamage(damage);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>().Score += col.gameObject.GetComponent<EnemyClass>().Score;
                GameObject.Destroy(this.gameObject);
            }
            else if (target.Equals("Player"))
            {
                col.gameObject.GetComponent<PlayerClass>().TakeDamage(damage);
                GameObject.Destroy(this.gameObject);
            }
        }
    }

    public void PauseOn()
    {
        canMove = false;
        GetComponent<Animator>().speed = 0f;
    }

    public void PauseOff()
    {
        canMove = true;
        GetComponent<Animator>().speed = 1f;
    }

    protected void CheckBounds()
    {
        var pos = transform.position;
        if (pos.x > 2.1f || pos.x < -2.1f || pos.y > 1.4f || pos.y < -1.4f)
            GameObject.Destroy(gameObject);
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public string Target
    {
        get { return target; }
        set { target = value; }
    }

    protected string Type
    {
        get { return type; }
        set { type = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    public GameObject Shooter
    {
        get { return shooter; }
        set { shooter = value; }
    }
}

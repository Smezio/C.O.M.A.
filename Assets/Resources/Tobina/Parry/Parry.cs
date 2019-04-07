using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour
{
    private bool canParry;
    private bool canShoot;

    private void Start()
    {
        canParry = GetComponentInParent<PlayerClass>().CanParry;
        canShoot = GetComponentInParent<PlayerClass>().CanShoot;
    }

    private void Update()
    {
        canParry = GetComponentInParent<PlayerClass>().CanParry;
        canShoot = GetComponentInParent<PlayerClass>().CanShoot;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((canParry && !canShoot) &&
                collision.gameObject.tag.Equals("Bullet") &&
                collision.gameObject.GetComponent<BulletClass>().Target.Equals("Player"))
        {
            BulletClass bullet = collision.gameObject.GetComponent<BulletClass>();
            Vector2 direction = (collision.gameObject.transform.position - transform.parent.position).normalized;
            bullet.Direction = direction;
            bullet.Speed = 1.5f;
            bullet.Target = "Enemy";
            bullet.Damage = 2;
        }
    }
}

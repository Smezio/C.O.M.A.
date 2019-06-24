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
            Vector2 direction = (((Vector2)bullet.transform.position + Vector2.one) - (Vector2)bullet.transform.position).normalized;

            if (bullet.Shooter != null)
                direction = bullet.Shooter.transform.position - bullet.transform.position;
            bullet.Direction = direction.normalized;

            Vector3 difference = transform.parent.position - transform.position;
            float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(bullet.transform.rotation.x, bullet.transform.rotation.y, rotationZ);

            bullet.Speed = 1.5f;
            bullet.Target = "Enemy";
            bullet.Damage = 2;
        }
    }
}

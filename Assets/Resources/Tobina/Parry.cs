using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour
{
    private CircleCollider2D parryCollider;
    private bool canParry;

    void Start()
    {
        parryCollider = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
            canParry = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (collision.gameObject.tag == "EnemyBullet")
            {
                Debug.Log("Parry!");
                GameObject obj = collision.gameObject;
                BulletClass bullet = obj.GetComponent<BulletClass>();

                Vector2 direction = (obj.transform.position - transform.parent.position).normalized;
                bullet.SetDirection(direction);
                bullet.SetSpeed(0.5f);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canParry = false;
    }
}

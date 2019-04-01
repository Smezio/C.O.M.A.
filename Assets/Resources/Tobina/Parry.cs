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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "EnemyBullet")
    //        canParry = true;
    //}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.parent.GetComponent<Animator>().SetBool("ParryClick", true);

            if (collision.gameObject.tag == "EnemyBullet")
            {
                Debug.Log("Parry!");
                GameObject obj = collision.gameObject;
                if (obj.GetComponent<Direct>() != null)
                {
                    Direct bullet = obj.GetComponent<Direct>();
                    Vector2 direction = (obj.transform.position - transform.parent.position).normalized;
                    bullet.SetDirection(direction);
                    bullet.SetSpeed(1.5f);
                }
                else if (obj.GetComponent<Inline>() != null)
                {
                    Inline bullet = obj.GetComponent<Inline>();
                    Vector2 direction = (obj.transform.position - transform.parent.position).normalized;
                    bullet.SetDirection(direction);
                    bullet.SetSpeed(1.5f);
                }
                else if (obj.GetComponent<Inline>() != null)
                {
                    AllDirections bullet = obj.GetComponent<AllDirections>();
                    Vector2 direction = (obj.transform.position - transform.parent.position).normalized;
                    bullet.SetDirection(direction);
                    bullet.SetSpeed(1.5f);
                }
            }
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    canParry = false;
    //}
}

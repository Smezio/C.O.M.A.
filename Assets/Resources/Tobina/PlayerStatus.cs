using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float maxhealthPoint = 4;
    private float healthPoint;

    private BoxCollider2D collider;

    private bool immune;
    public float immuneDuration = 10; // sec
    private float immuneTimer;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        healthPoint = maxhealthPoint;
        immuneTimer = immuneDuration;
    }

    void Update()
    {
        if (immune)
        {
            immuneTimer -= Time.deltaTime;
            if (immuneTimer <= 0)
                immune = false;
        }       
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "PowerUp")
            return;

        if (collision.gameObject.name == "Cookie")
        {
            Debug.Log("Cookie");

            healthPoint++;
            if (healthPoint > maxhealthPoint)
                healthPoint = maxhealthPoint;
        }
        else if (collision.gameObject.name == "TeddyBear")
        {
            Debug.Log("Teddy");
            immune = true;
        }

        Destroy(collision.gameObject);
    }

    public void TakeDamage(float damage)
    {
        if (immune)
            return;

        healthPoint -= damage;
        Debug.Log(healthPoint);

        if (healthPoint <= 0)
            GetComponent<Animator>().SetBool("Death", true);
    }
}

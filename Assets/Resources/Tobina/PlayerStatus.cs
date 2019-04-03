using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    /* Status properties */
    public float maxhealthPoint = 4;
    private float healthPoint;
    private bool immune;
    public float immuneDuration = 10; // sec
    private float immuneTimer;

    void Start()
    {
        healthPoint = maxhealthPoint;
        immuneTimer = immuneDuration;
    }

    void Update()
    {
        ImmuneControl();
    }

    /* Collisioni power up */
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

        if (healthPoint <= 0)
            GetComponent<Animator>().SetBool("Death", true);
    }

    public void ImmuneControl()
    {
        if (immune)
        {
            immuneTimer -= Time.deltaTime;
            if (immuneTimer <= 0)
                immune = false;
        }
    }

    protected void DestroyTobina()
    {
        GameObject.Destroy(this.gameObject);
    }

    public float GetHealthPoint()
    {
        return healthPoint;
    }
}

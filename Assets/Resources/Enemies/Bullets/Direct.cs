using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direct : BulletClass
{
    // Start is called before the first frame update
    void Start()
    {
        type = "Direct";
        speed = 0.7f;
        damage = 1;
        target = "Player";

        BulletRotation();
        direction = ((GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    protected void BulletRotation()
    {
        if ((GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x))
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}

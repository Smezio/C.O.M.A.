using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : BulletClass
{
    private PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        type = "Inline";
        speed = 0.5f;
        damage = 2;
        target = "Enemy";

        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        BulletDirection();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    void BulletDirection()
    {
        if (pm.GetFacing())
        {
            GetComponent<SpriteRenderer>().flipY = true;
            direction = Vector3.left;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipY = false;
            direction = Vector3.right;
        }
            
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : BulletClass
{
    private PlayerClass pm;

    // Start is called before the first frame update
    void Start()
    {
        type = "Inline";
        target = "Enemy";

        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>();
        BulletDirection();
    }

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
        if (canMove)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }
    }

    void BulletDirection()
    {
        if (pm.Facing)
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

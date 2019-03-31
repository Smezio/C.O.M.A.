using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inline : BulletClass
{
    // Start is called before the first frame update
    void Awake()
    {
        type = "Inline";
        speed = 0.5f;
        damage = 1;
        target = "Player";
        
        direction = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction.normalized * Time.deltaTime * speed);
    }
}

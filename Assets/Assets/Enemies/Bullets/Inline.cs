using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inline : BulletClass
{
    // Start is called before the first frame update
    void Start()
    {
        type = "Inline";
        target = "Player";
        
        direction = (Vector2.right).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
        if (canMove)
            transform.Translate(direction * Time.deltaTime * speed);
    }
}

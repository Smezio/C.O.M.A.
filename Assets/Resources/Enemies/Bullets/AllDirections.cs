using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDirections : BulletClass
{
    // Start is called before the first frame update
    void Start()
    {
        type = "AllDirections";
        target = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
        Movement();
    }

    private void Movement()
    {
        if (canMove)
        {
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
}

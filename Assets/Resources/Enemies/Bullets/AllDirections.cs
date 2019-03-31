using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDirections : BulletClass
{
    // Start is called before the first frame update
    void Start()
    {
        type = "AllDirections";
        speed = 0.7f;
        damage = 1;
        target = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).Translate(Vector3.left * Time.deltaTime * speed);
    }
}

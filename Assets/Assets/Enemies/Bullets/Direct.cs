using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direct : BulletClass
{
    // Start is called before the first frame update
    void Start()
    {
        type = "Direct";
        target = "Player";
        
        BulletRotation();
        direction = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
        if (canMove)
            transform.Translate(direction * Time.deltaTime * speed, Space.World);
    }

    protected void BulletRotation()
    {
        Vector3 difference = GameObject.FindWithTag("Player").transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotationZ);
    }
}

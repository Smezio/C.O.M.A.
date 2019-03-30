using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public Vector2 vel;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = vel;
    }

    // Update is called once per frame
    void Update()
    {
    }
}

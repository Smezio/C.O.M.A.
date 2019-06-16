using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    private BoxCollider2D collider;
    private float horizontalLength;
    private float longitudinalPosition;

    private Rigidbody2D rb2d;
    public Vector2 vel;

    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        horizontalLength = collider.size.x;
        longitudinalPosition = GetComponent<Transform>().position.z;

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = vel;

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -horizontalLength)
        {
            changePosition();
        }
    }

    private void changePosition()
    {
        Vector3 offset = new Vector3(horizontalLength * 2, 0, 0);
        transform.position = transform.position + offset;
    }

    public void PauseOn()
    {
        rb2d.velocity = Vector2.zero;
    }

    public void PauseOff()
    {
        rb2d.velocity = vel;
    }
}

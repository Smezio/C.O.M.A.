using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb2d;
    private BoxCollider2D collider;
    private SpriteRenderer renderer;
    private Vector2 vel;
    private bool facing;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        facing = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Move
        vel.x = Input.GetAxisRaw("Horizontal");
        vel.y = Input.GetAxisRaw("Vertical");
        rb2d.velocity = vel * speed * Time.deltaTime;

        // Clamp inside camera view
        var pos = rb2d.position;
        pos.x = Mathf.Clamp(pos.x, -1.6f, 1.6f);
        pos.y = Mathf.Clamp(pos.y, -0.9f, 0.9f);
        rb2d.position = pos;

        // Flip sprite according to velocity
        if (vel.x != 0)
            facing = vel.x < 0 ? true : false;

        renderer.flipX = facing;
    }

    public bool GetFacing()
    {
        return facing;
    }
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb2d;
    private BoxCollider2D collider;
    private Vector2 vel;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        vel.x = Input.GetAxisRaw("Horizontal");
        vel.y = Input.GetAxisRaw("Vertical");
      
        rb2d.velocity = vel * speed * Time.deltaTime;
        var pos = rb2d.position;
        pos.x = Mathf.Clamp(pos.x, -1.6f, 1.6f);
        pos.y = Mathf.Clamp(pos.y, -0.9f, 0.9f);
        rb2d.position = pos;

    }
}

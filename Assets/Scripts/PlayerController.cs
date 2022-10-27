using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 2f, jumpForce = 10f, maxVelocityX = 10;
    public LayerMask groundLayer;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //LayerMask groundLayer = 1 << LayerMask.NameToLayer("Everything");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.A))
        {
            rb.velocity += Vector2.left * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity += Vector2.right * speed;
        }
        if (rb.velocity.x > maxVelocityX)
        {
            rb.velocity = new Vector2 (maxVelocityX, rb.velocity.y);
        }
        else if (rb.velocity.x < -maxVelocityX)
        {
            rb.velocity = new Vector2(-maxVelocityX, rb.velocity.y);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity += Vector2.up * jumpForce;
        }
    }

    public bool IsGrounded()
    {
        if (Physics2D.OverlapCircle(transform.position + Vector3.down * .5f, .5f, groundLayer))//, -2f, -0.5f))
            return true;
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health = 100;
    [SerializeField]
    float speed = 2f, jumpForce = 10f, maxVelocityX = 10,ySpeedLimit = 10;
    public LayerMask groundLayer;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.A))
        {
            rb.velocity += Vector2.left * speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity += Vector2.right * speed;
        }
        else if (IsGrounded())
        {
            rb.velocity *= .75f;
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
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && rb.velocity.y <= ySpeedLimit)
        {
            rb.velocity += Vector2.up * jumpForce;
        }
    }

    public bool IsGrounded()
    {
        if (Physics2D.OverlapCircle(transform.position + Vector3.down * .6f, .5f, groundLayer))
            return true;
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + Vector3.down * .6f, .5f);
    }
}

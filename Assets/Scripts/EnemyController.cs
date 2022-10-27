using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    PlayerController playerMove;
    [SerializeField]
    GameObject player;
    [SerializeField]
    float Health, damage = 10, enemySpeed, enemyMaxVelocity, jumppower;
    [SerializeField]
    AIstate currentstate = AIstate.movingforward;
    [SerializeField]
    LayerMask groundLayer;
    Rigidbody2D rb;

    enum AIstate{
        movingforward,jumping,idle
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position + Vector3.down * .6f, .5f, groundLayer))
        {
            Jump();
        }
            if (Health <= 0 && gameObject != null)
        {
            Destroy(gameObject);
        }
        switch(currentstate)
        {
            case AIstate.movingforward:
                if (player.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(enemySpeed, rb.velocity.y);
                }
                if (player.transform.position.x < transform.position.x)
                {
                    rb.velocity = new Vector2(-enemySpeed, rb.velocity.y);
                }
                if (rb.velocity.x > enemyMaxVelocity)
                {
                    rb.velocity = new Vector2(enemyMaxVelocity, rb.velocity.y);
                }
                else if (rb.velocity.x < -enemyMaxVelocity)
                {
                    rb.velocity = new Vector2(-enemyMaxVelocity, rb.velocity.y);
                }
                break;
            case AIstate.jumping:
                break;
            case AIstate.idle:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == player)
        {
            playerMove.health -= damage;
        }
        if(collision.rigidbody != null)
        {
            Health -= 0.5f * collision.rigidbody.mass * Mathf.Pow(collision.rigidbody.velocity.magnitude, 2);
            Debug.Log(collision.gameObject.name);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + Vector3.right * .6f + Vector3.down * 0.5f, .1f);
        Gizmos.DrawSphere(transform.position + Vector3.left * .6f + Vector3.down * 0.5f, .1f);
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumppower);
    }
}

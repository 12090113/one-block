using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    PlayerController player;
    [SerializeField]
    float Health, damage = 10, enemySpeed, enemyMaxVelocity, jumpPower, fallDamage, blockDamage = 0.01f;
    [SerializeField]
    AIstate currentstate = AIstate.Chasing;
    [SerializeField]
    LayerMask groundLayer;
    Rigidbody2D rb;

    enum AIstate{
        Chasing,Idle
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position + Vector3.right * .6f + Vector3.down * 0.5f, .1f, player.groundLayer) && rb.velocity.y >= 0)
        {
            Jump();
        }
        if (Physics2D.OverlapCircle(transform.position + Vector3.left * .6f + Vector3.down * 0.5f, .1f, player.groundLayer) && rb.velocity.y >= 0)
        {
            Jump();
        }
        if (Health <= 0 && gameObject != null)
        {
            Destroy(gameObject);
        }
        switch(currentstate)
        {
            case AIstate.Chasing:
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
            case AIstate.Idle:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == player.gameObject)
        {
            player.health -= damage;
        }
        if(collision.rigidbody != null)
        {
            Health -= 0.5f * collision.rigidbody.mass * Mathf.Pow(collision.relativeVelocity.magnitude, 2) * blockDamage;
            Debug.Log(collision.gameObject.name);
        }
        else
        {
            float KE = 0.5f * rb.mass * Mathf.Pow(rb.velocity.magnitude, 2) * fallDamage;
            if ((KE) >= 0.1f)
            {
            Health -= KE;
            }
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
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
    }
}

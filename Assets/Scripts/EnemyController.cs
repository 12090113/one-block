using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemySpawn EC;
    [SerializeField]
    PlayerController player;
    [SerializeField]
    float Health, damage = 10, enemySpeed, enemyMaxVelocity, jumpPower, fallDamage, blockDamage = 0.01f;
    [SerializeField]
    AIstate currentstate = AIstate.Chasing;
    [SerializeField]
    LayerMask groundLayer;
    Rigidbody2D rb;
    float timer, hittime = 1;
    bool isTouching;
    enum AIstate{
        Chasing,Idle
    }
    // Start is called before the first frame update
    void Start()
    {
        EC = FindObjectOfType<EnemySpawn>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        Collider2D circle = Physics2D.OverlapCircle(transform.position + Vector3.right * .6f + Vector3.down * 0.5f, .1f, player.groundLayer);

        

        if (circle && (circle.gameObject.tag.Equals("TileMap") || circle.gameObject.tag.Equals("Block")) && rb.velocity.y >= 0)
        {
            Jump();
        }
        circle = Physics2D.OverlapCircle(transform.position + Vector3.left * .6f + Vector3.down * 0.5f, .1f, player.groundLayer);
        if (circle && (circle.gameObject.tag.Equals("TileMap") || circle.gameObject.tag.Equals("Block")) && rb.velocity.y >= 0)
        {
            Jump();
        }
        if (Health <= 0 && gameObject != null)
        {
            Die(true);
        }
        if(timer >= hittime && isTouching == true)
        {
            player.Damage(damage);
            timer = 0;
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
        float dist = Mathf.Abs(player.transform.position.x - transform.position.x);
        if (dist > 60 && rb.velocity.magnitude < 0.1)
        {
            Die();
        } else if (dist > 100)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null && player != null && collision.gameObject == player.gameObject)
        {
            isTouching = true;
        }
        if(collision.rigidbody != null)
        {
            Health -= 0.5f * collision.rigidbody.mass * Mathf.Pow(collision.relativeVelocity.magnitude, 2) * blockDamage;
        }
        else if (rb != null)
        {
            float KE = 0.5f * rb.mass * Mathf.Pow(rb.velocity.magnitude, 2) * fallDamage;
            if ((KE) >= 0.1f)
            {
            Health -= KE;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject == player.gameObject)
        {
            isTouching = false;
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

    void Die(bool byPlayer = false)
    {
        EC.enemieskilled++;
        transform.position = EC.SpawnPos();
        Health = 100f;
    }

    private void OnDestroy()
    {
        EC.enemies.Remove(gameObject);
    }
}

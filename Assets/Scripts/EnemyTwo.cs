using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwo : MonoBehaviour
{
    [SerializeField]
    EnemySpawn spawner;
    float blockDamage = 0.01f;
    [SerializeField]
    float health = 100;
    [SerializeField]
    int seaLevel = 0;
    [SerializeField]
    GameObject explodePrefab;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    float speed = 2, maxVelocityX = 5, hoverDistance = 7, timer, attacktimer, attackCooldown, explosionTimer;
    PlayerController playerController;
    TileController tileController;
    [SerializeField]
    Vector3 playerPos;
    Vector3 thisPos;
    RaycastHit2D ray;
    enum AIstate
    {
        Chasing, OverLeft, OverRight
    }
    [SerializeField]
    AIstate state;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<PlayerController>();
        spawner = FindObjectOfType<EnemySpawn>();
        tileController = FindObjectOfType<TileController>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        attacktimer += Time.deltaTime;

        if(health <= 0 && gameObject != null)
        {
            Die(true);
        }
        if(attacktimer >= attackCooldown)
        {
            Attack();
            attacktimer = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ray = Physics2D.Raycast((Vector2)transform.position + Vector2.down, Vector2.down, Mathf.Infinity);
        playerPos = playerController.gameObject.transform.position;
        thisPos = transform.position;

        if (rb.velocity.y < 0.1f && rb.velocity.y > -0.1f && rb.velocity.x < 0.1f && rb.velocity.x > -0.1f && state != AIstate.Chasing)
        {
            tileController.DestroyArea(thisPos);
        }

        //Defy Gravity mwhhahaha-
        rb.velocity = rb.velocity + Vector2.up * -Physics2D.gravity.y * Time.fixedDeltaTime;
        //rb.angularVelocity = 10000;

        //If y value close enough to ground, 
        if (thisPos.y - ray.point.y > hoverDistance + 3)
        {
            rb.velocity = rb.velocity + new Vector2(0, -speed);
            //Debug.Log("going down " + (thisPos.y - ray.point.y));
        }
        //if y value too close to ground, or below sea level
        if ((thisPos.y - ray.point.y < hoverDistance - 3 || thisPos.y <= seaLevel || thisPos.y <= playerPos.y) && state == AIstate.Chasing)
        {
            rb.velocity = rb.velocity + new Vector2(0, speed);
            //Debug.Log("going up " + (thisPos.y) + "    " + (ray.point.y));
        }

        //if velocity x is too big,
        if(rb.velocity.x > maxVelocityX)
        {
            //Debug.Log("Right");
            rb.velocity = new Vector2(maxVelocityX, rb.velocity.y);
        }
        //if velocity x is too small,
        if(rb.velocity.x < -maxVelocityX)
        {
            //Debug.Log("Left");
            rb.velocity = new Vector2(-maxVelocityX, rb.velocity.y);
        }


        //if position is right of player
        if (thisPos.x > playerPos.x + 1)
        {
            //set velocity to go left
            rb.velocity = rb.velocity + new Vector2(-speed , 0);
            RaycastHit2D leftray = Physics2D.Raycast((Vector2)transform.position + Vector2.left, Vector2.left, 5);
            if (leftray.collider != null && Mathf.Abs(playerPos.x - thisPos.x) > 3 && state != AIstate.OverRight)
            {
                state = AIstate.OverLeft;
                rb.velocity = rb.velocity + new Vector2(0, speed * 4);
            }
            else if (state == AIstate.OverLeft)
            {
                state = AIstate.Chasing;
            }
        }
        //if position is left of player
        else if (thisPos.x < playerPos.x - 1)
        {
            //set velocity to go right
            rb.velocity = rb.velocity + new Vector2(speed , 0);
            RaycastHit2D rightray = Physics2D.Raycast((Vector2)transform.position + Vector2.right, Vector2.right, 5);
            if (rightray.collider != null && Mathf.Abs(playerPos.x - thisPos.x) > 3 && state != AIstate.OverLeft)
            {
                state = AIstate.OverRight;
                rb.velocity = rb.velocity + new Vector2(0, speed * 4);
            }
            else if (state == AIstate.OverRight)
            {
                state = AIstate.Chasing;
            }
        }
        if (Mathf.Abs(playerController.transform.position.x - transform.position.x) > 60)
        {
            Die();
        }
    }

    public void Attack()
    {
        Instantiate(explodePrefab, transform.position + Vector3.down * 1.5f, Quaternion.identity);
    }

    public void UpRightLeftDown()
    {
        switch(Mathf.Round(timer))
        {
            case 1:
                transform.position = transform.position + Vector3.zero;
                break;
            case 2:
                transform.position = transform.position + Vector3.up;
                break;
            case 3:
                transform.position = transform.position + Vector3.up + Vector3.right;
                break;
            case 4:
                transform.position = transform.position + Vector3.right;
                break;
            case 5: 
                timer = 0;
                break;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody != null)
        {
            health -= 0.5f * collision.rigidbody.mass * Mathf.Pow(collision.relativeVelocity.magnitude, 2) * blockDamage;
        }
    }

    void Die(bool byPlayer = false)
    {
        if (byPlayer)
            spawner.enemieskilled++;
        transform.position = spawner.SpawnPos() + Vector3.up * 10f;
        health = 300f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class EnemyTwo : MonoBehaviour
{
    float blockDamage = 0.01f;
    [SerializeField]
    float health = 100;
    [SerializeField]
    int seaLevel = 0;
    [SerializeField]
    GameObject currentExplosion;
    [SerializeField]
    GameObject explodePrefab;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    float speed = 2, maxVelocityX = 5, hoverDistance = 7, timer, attacktimer, attackCooldown, explosionTimer;
    bool isExplosionTrue;
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    TileController tc;
    //[SerializeField]
    Vector3 playerPos;
    Vector3 thisPos;
    RaycastHit2D ray;
    //[SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tc = FindObjectOfType<TileController>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        attacktimer += Time.deltaTime;

        if(health <= 0 && gameObject != null)
        {
            Destroy(gameObject);
        }
        if(attacktimer >= attackCooldown)
        {
            Debug.Log("working");
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

        //Defy Gravity mwhhahaha-
        rb.velocity = rb.velocity + Vector2.up * -Physics2D.gravity.y * Time.fixedDeltaTime;
        //rb.angularVelocity = 10000;

        //If y value close enough to ground, 
        if (thisPos.y - ray.point.y > hoverDistance + 3)
        {
            rb.velocity = rb.velocity + new Vector2(0, -speed);
            Debug.Log("going down " + (thisPos.y - ray.point.y));
        }
        //if y value too close to ground, or below sea level
        if (thisPos.y - ray.point.y < hoverDistance - 3 || thisPos.y <= seaLevel || thisPos.y <= playerPos.y)
        {
                rb.velocity = rb.velocity + new Vector2(0, speed);
            Debug.Log("going up " + (thisPos.y) + "    " + (ray.point.y));
        }

        //if velocity x is too big,
        if(rb.velocity.x > maxVelocityX)
        {
            Debug.Log("Right");
            rb.velocity = new Vector2(maxVelocityX, rb.velocity.y);
            RaycastHit2D rightray = Physics2D.Raycast((Vector2)transform.position + Vector2.right, Vector2.down, 5);
            if(rightray.collider != null && rightray.collider.gameObject != null)
            {
                rb.velocity = rb.velocity + new Vector2(0, -speed);
            }
        }
        //if velocity x is too small,
        if(rb.velocity.x < -maxVelocityX)
        {
            Debug.Log("Left");
            rb.velocity = new Vector2(-maxVelocityX, rb.velocity.y);
            RaycastHit2D leftray = Physics2D.Raycast((Vector2)transform.position + Vector2.right, Vector2.down, 5);
            if (leftray.collider != null && leftray.collider.gameObject != null)
            {
                rb.velocity = rb.velocity + new Vector2(0, -speed);
            }
        }


        //if position is right of player
       if (thisPos.x > playerPos.x + 1)
        {
            //set velocity to go left
            rb.velocity = rb.velocity + new Vector2(-speed , 0);
        }
       //if position is left of player
        else if (thisPos.x < playerPos.x - 1)
        {
            //set velocity to go right
            rb.velocity = rb.velocity + new Vector2(speed , 0);
        }

    }

    public void Attack()
    {
        ray = Physics2D.Raycast((Vector2)transform.position + Vector2.down, Vector2.down, Mathf.Infinity);
        currentExplosion = Instantiate(explodePrefab, transform.position + Vector3.down * 1.5f, Quaternion.identity);
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
}

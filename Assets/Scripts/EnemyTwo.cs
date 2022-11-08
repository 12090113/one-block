using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class EnemyTwo : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    float speed = 2, maxVelocityX = 5, hoverDistance = 7;
    [SerializeField]
    PlayerController playerController;
    //[SerializeField]
    Vector3 playerPos;
    Vector3 thisPos;
    RaycastHit2D ray;
    //[SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<PlayerController>();
        playerPos = playerController.gameObject.transform.position;
        thisPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ray = Physics2D.Raycast((Vector2)transform.position + Vector2.down, Vector2.down, Mathf.Infinity);

        rb.velocity = rb.velocity + Vector2.up * -Physics2D.gravity.y * Time.fixedDeltaTime;
        //rb.angularVelocity = 10000;


        if (thisPos.y - ray.point.y > hoverDistance + 1)
        {
            rb.velocity = rb.velocity + new Vector2(0, -speed* .1f);
            Debug.Log("going down " + (thisPos.y - ray.point.y));
        }
        if (thisPos.y - ray.point.y < hoverDistance - 1)
        {
            rb.velocity = rb.velocity + new Vector2(0, speed* .1f);
            Debug.Log("going up " + (thisPos.y) + "    " + (ray.point.y));
        }

        if(rb.velocity.x > maxVelocityX)
        {
            Debug.Log("Right");
            rb.velocity = new Vector2(maxVelocityX, rb.velocity.y);
            //ray = Physics2D.Raycast((Vector2)transform.position + Vector2.right, Vector2.down, Mathf.Infinity);
        }
        if(rb.velocity.x < -maxVelocityX)
        {
            Debug.Log("Left");
            rb.velocity = new Vector2(-maxVelocityX, rb.velocity.y);
            //ray = Physics2D.Raycast((Vector2)transform.position + Vector2.right, Vector2.down, Mathf.Infinity);
        }



       if (thisPos.x > playerPos.x)
        {
            rb.velocity = rb.velocity + new Vector2(-speed , 0);
        }
        else
        {
            rb.velocity = rb.velocity + new Vector2(speed , 0);
        }

    }
}

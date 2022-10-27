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
    float Health, damage = 10;
    [SerializeField]
    AIstate currentstate = AIstate.movingforward;

    enum AIstate{
        movingforward,jumping,idle
    }
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentstate)
        {
            case AIstate.movingforward:
                if (player.transform.position.x > transform.position.x)
                {

                }
                if (player.transform.position.x < transform.position.x)
                {

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
}

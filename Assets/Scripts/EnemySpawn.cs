using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if(Physics2D.OverlapCircle(transform.position + Vector3.down * .6f, .5f))
        {
            transform.Translate(Vector2.up);
        }

    }
}

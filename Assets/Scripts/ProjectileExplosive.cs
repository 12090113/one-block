using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplosive : MonoBehaviour
{
    public TileController tc;
    public EnemyTwo ec;
    // Start is called before the first frame update
    void Start()
    {
        tc = FindObjectOfType<TileController>();
        ec = FindObjectOfType<EnemyTwo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Enemy")
        {
            tc.DestroyArea(transform.position);
            if(gameObject != null)
            Destroy(gameObject);
        }
    }
}

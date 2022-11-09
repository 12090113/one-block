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
        if(collision.gameObject != ec.gameObject)
        {
            tc.DestroyArea(transform.position + new Vector3(-4,4,0));
            if(gameObject != null)
            Destroy(gameObject);
        }
    }
}

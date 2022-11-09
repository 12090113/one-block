using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    float timer = 0;
    public float spawnInterval = 1;
    public int maxenemies = 3;
    [SerializeField]
    ReloadScene rs;
    public int enemieskilled;
    int wincondition = 10;
    [SerializeField]
    public List <GameObject> enemies;
    // Start is called before the first frame update
    void Start()
    {
        rs = FindObjectOfType<ReloadScene>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer < spawnInterval || enemies.Count >= maxenemies)
            return;
        var vertExtent = Camera.main.orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;
        float pos = Random.Range(horzExtent, horzExtent + 50) * (Random.Range(0,2)*2-1);

        RaycastHit2D ray = Physics2D.Raycast(new Vector2(pos, 10000) + (Vector2)transform.position, Vector2.down, Mathf.Infinity) ;
        if(ray.collider == null)
        {
            return;
        }

        if(ray.collider.tag == "TileMap")
        {
            if(timer > spawnInterval)
            {
                enemies.Add(Instantiate(enemy, ray.point + Vector2.up , Quaternion.identity));
                spawnInterval = 0;
                
            }
        }
    }
}

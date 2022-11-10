using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    float speed;
    public PlayerController pc;
    [SerializeField]
    float velx, vely;
    // Start is called before the first frame update
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        velx = pc.rb.velocity.x;
        vely = pc.rb.velocity.y;
        transform.Translate((velx * speed) / 50, (vely * speed) / 100 ,0);
    }
}

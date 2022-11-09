using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    PlayerController pc;
    ReloadScene rs;
    // Start is called before the first frame update
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        rs = FindObjectOfType<ReloadScene>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == pc.gameObject)
        {
            rs.win.SetActive(true);
            rs.canvas.SetActive(true);
            Time.timeScale = 0;
        }
    }
}

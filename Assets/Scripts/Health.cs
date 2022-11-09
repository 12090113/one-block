using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    float heal = 20;
    [SerializeField]
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == playerController.gameObject && playerController.health < 100)
        {
            playerController.health = Mathf.Clamp(playerController.health + heal, 0, 100);
            Destroy(gameObject);
        }
    }
}

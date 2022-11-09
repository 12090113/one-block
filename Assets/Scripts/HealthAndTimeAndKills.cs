using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndTimeAndKills : MonoBehaviour
{
    PlayerController pc;
    EnemySpawn es;

    [SerializeField]
    TextMeshProUGUI health,kills;

    // Start is called before the first frame update
    void Start()
    {
        es = FindObjectOfType<EnemySpawn>();
        pc = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        kills.text = "Kills: " + es.enemieskilled;
        health.text = "Health: " + pc.health;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    Tilemap tilemp;
    [SerializeField]
    PlayerController player;
    void Start() {
        tilemp = GetComponent<Tilemap>();
    }
    void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point = Physics2D.Raycast(player.transform.position, point, 5).point;
            Vector3Int selectedTile = tilemp.WorldToCell(point);
            Debug.Log("clicked " + selectedTile);
            tilemp.SetTile(selectedTile, null);
        }
    }
}

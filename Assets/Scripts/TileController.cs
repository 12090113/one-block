using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    Tilemap tilemp;
    void Start() {
        tilemp = GetComponent<Tilemap>();
    }
    void Update() {
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0)) {
            Vector3Int selectedTile = tilemp.WorldToCell(point);
            Debug.Log("clicked " + selectedTile);
            tilemp.SetTile(selectedTile, null);
        }
    }
}

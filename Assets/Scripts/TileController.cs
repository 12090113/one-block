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
            point = Physics2D.Raycast(player.transform.position, point, 5, player.groundLayer).point;
            //Gizmos.DrawLine(player.transform.position, point);
            Vector3Int selectedTile = tilemp.WorldToCell(point);
            Debug.Log("clicked " + selectedTile);
            tilemp.SetTile(selectedTile, null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Gizmos.DrawLine(player.transform.position, Physics2D.Raycast(player.transform.position, point, 5, player.groundLayer).point);
    }
}

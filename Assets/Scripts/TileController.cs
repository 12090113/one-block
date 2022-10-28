using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    bool blockInHand;
    TileBase currentBlock;
    GameObject heldBlock;
    [SerializeField]
    GameObject dirtblock;
    [SerializeField]
    RuleTile dirt;
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
            var ray = Physics2D.Raycast(player.transform.position, point-(Vector2)player.transform.position, 100, player.groundLayer);
            if (currentBlock == null)
                point = ray.point - ray.normal * 0.1f;
            else
                point = ray.point - ray.normal * -0.1f;
            Vector3Int selectedTile = tilemp.WorldToCell(point);
            Debug.Log("clicked " + selectedTile);
            if (currentBlock != null)
            {
                Debug.Log(currentBlock.name);
                tilemp.SetTile(selectedTile, currentBlock);
                currentBlock = null;
                Destroy(heldBlock);
            }
            else
            {
                currentBlock = tilemp.GetTile(selectedTile);
                tilemp.SetTile(selectedTile, null);
                heldBlock = Instantiate(dirtblock, player.transform.position + Vector3.up * 2, Quaternion.identity, player.gameObject.transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Gizmos.DrawLine(player.transform.position, Physics2D.Raycast(player.transform.position, point-(Vector2)player.transform.position, 100, player.groundLayer).point);
    }
}

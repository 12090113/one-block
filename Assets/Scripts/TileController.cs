using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    TileBase currentBlock;
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
            RaycastHit2D ray = Physics2D.Raycast(player.transform.position, point-(Vector2)player.transform.position, 100, player.groundLayer);
            if (ray.collider == null)
            {
                Debug.Log("hi");
                return;
            }

            if (currentBlock == null)
                point = ray.point - ray.normal * 0.1f;
            else
                point = ray.point + ray.normal * 0.1f;
            Vector3Int selectedTile = tilemp.WorldToCell(point);
            if (currentBlock != null)
            {
                if (Physics2D.OverlapPoint(tilemp.CellToWorld(selectedTile) + new Vector3(.5f, .5f)))
                {
                    return;
                }
                Debug.Log(currentBlock.name);
                tilemp.SetTile(selectedTile, currentBlock);
                currentBlock = null;
                Destroy(player.heldBlock);
            }
            else
            {
                currentBlock = tilemp.GetTile(selectedTile);
                tilemp.SetTile(selectedTile, null);
                if (currentBlock != null)
                player.heldBlock = Instantiate(dirtblock, player.transform.position + Vector3.up * 2, Quaternion.identity, player.gameObject.transform);
                if(player.heldBlock != null)
                player.blockrb = player.heldBlock.GetComponent<Rigidbody2D>();
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

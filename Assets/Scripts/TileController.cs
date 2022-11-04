using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    public TileBase currentBlock;
    [SerializeField]
    GameObject dirtblock;
    [SerializeField]
    RuleTile dirt;
    Tilemap tilemp;
    [SerializeField]
    PlayerController player;

    [SerializeField]
    public Dictionary<RuleTile, float> mass = new Dictionary<RuleTile, float>();

    void Start() {
        tilemp = GetComponent<Tilemap>();
    }
    void Update() {
        //if left click
        if (Input.GetMouseButtonDown(0))
        {
            //get the position of the mouse
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Ray for placing blocks
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

            //If current type of block exists
            if (currentBlock != null)
            {
                //mouse check where position. Set selection to center of block
                if (Physics2D.OverlapPoint(tilemp.CellToWorld(selectedTile) + new Vector3(.5f, .5f)))
                {
                    return;
                }
                Debug.Log(currentBlock.name);
                //set tile at location to currentBlock type.
                tilemp.SetTile(selectedTile, currentBlock);
                //set the blocktype to null.
                //set gameobject block to null.
                Destroy(player.heldBlock);
                currentBlock = null;
            }
            //if current type of block doesn't exist
            else
            {
                currentBlock = tilemp.GetTile(selectedTile);
                tilemp.SetTile(selectedTile, null);
                if (currentBlock != null)
                {
                    player.heldBlock = Instantiate(dirtblock, player.transform.position + Vector3.up * 2, Quaternion.identity);
                    player.heldBlock.GetComponent<SpriteRenderer>().sprite = ((RuleTile)currentBlock).m_DefaultSprite;
                    player.heldBlock.GetComponent<Block>().tile = currentBlock;
                    player.blockrb = player.heldBlock.GetComponent<Rigidbody2D>();
                    player.blockrb.velocity = player.rb.velocity;
                    player.joint.enabled = true;
                    player.joint.connectedBody = player.blockrb;
                }
                else if (ray.collider.tag == "Block")
                {
                    currentBlock = tilemp.GetTile(selectedTile);
                    ray.collider.gameObject.transform.position = player.transform.position + Vector3.up * 2;
                    ray.collider.gameObject.transform.rotation = player.transform.rotation;

                    player.heldBlock = ray.collider.gameObject;
                    currentBlock = ray.collider.gameObject.GetComponent<Block>().tile;
                    player.blockrb = player.heldBlock.GetComponent<Rigidbody2D>();
                    player.blockrb.velocity = player.rb.velocity;
                    player.joint.enabled = true;
                    player.joint.connectedBody = player.blockrb;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var ray = Physics2D.Raycast(player.transform.position, point - (Vector2)player.transform.position, 100, player.groundLayer);
        if (ray.collider != null)
            Gizmos.DrawLine(player.transform.position, ray.point);
    }
}

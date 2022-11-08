using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    public TileBase currentBlock;
    [SerializeField]
    GameObject dirtblock;
    DrawBox box;
    Tilemap tilemp;
    [SerializeField]
    PlayerController player;

    [Serializable]
    public class TileFloatDictionary : SerializableDictionary<TileBase, float> { }
    public TileFloatDictionary mass = new TileFloatDictionary {};

    [ExecuteInEditMode]
    private void Awake()
    {

    }

    void Start() {
        tilemp = GetComponent<Tilemap>();
        box = FindObjectOfType<DrawBox>();
    }
    void Update() {
        //get the position of the mouse
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Ray for placing blocks
        RaycastHit2D ray = Physics2D.Raycast(player.transform.position, point-(Vector2)player.transform.position, 100, player.groundLayer);

        if (ray.collider == null)
        {
            box.HideLines();
            return;
        }
        else
            box.Draw();

            if (currentBlock == null)
                point = ray.point - ray.normal * 0.1f;
            else
        {
                point = ray.point + ray.normal * 0.1f;
        }

            Vector3Int selectedTile = tilemp.WorldToCell(point);

        box.transform.position = selectedTile;

        //if left click
        if (Input.GetMouseButtonDown(0))
        {

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
                    GameObject block = Instantiate(dirtblock, player.transform.position + Vector3.up * 1.5f, Quaternion.identity);
                    block.GetComponent<SpriteRenderer>().sprite = ((RuleTile)currentBlock).m_DefaultSprite;
                    block.GetComponent<Block>().tile = currentBlock;
                    player.PickupBlock(block, true);
                }
                else if (ray.collider.tag == "Block")
                {
                    GameObject block = ray.collider.gameObject;
                    currentBlock = ray.collider.gameObject.GetComponent<Block>().tile;
                    player.PickupBlock(block, false);
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

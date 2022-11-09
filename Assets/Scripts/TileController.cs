using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    [SerializeField]
    EnemyTwo et;
    public TileBase currentBlock;
    [SerializeField]
    GameObject dirtblock, crumbs;
    DrawBox box;
    Tilemap tilemp;
    [SerializeField]
    PlayerController player;
    [SerializeField]
    Color badColor;
    [SerializeField]
    LayerMask placeLayers;
    Vector3[] explosion;

    [Serializable]
    public class TileFloatDictionary : SerializableDictionary<TileBase, float> { }
    public TileFloatDictionary mass = new TileFloatDictionary {};

    [ExecuteInEditMode]
    private void Awake()
    {

    }

    void Start() {
        et = FindObjectOfType<EnemyTwo>();
        explosion = new Vector3[64];
        tilemp = GetComponent<Tilemap>();
        box = FindObjectOfType<DrawBox>();
    }
    void Update() {
        //get the position of the mouse
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Ray for placing blocks
        RaycastHit2D ray = Physics2D.Raycast(player.transform.position, point-(Vector2)player.transform.position, 100, player.groundLayer);

        if (ray.collider == null || player.throwing)
        {
            box.HideLines();
            return;
        }

        if (currentBlock == null)
            point = ray.point - ray.normal * 0.1f;
        else
        {
            point = ray.point + ray.normal * 0.1f;
        }

        Vector3Int selectedTile = tilemp.WorldToCell(point);

        if (currentBlock != null)
        {
            box.transform.position = selectedTile;
            box.transform.rotation = Quaternion.identity;
            if (Physics2D.OverlapPoint(tilemp.CellToWorld(selectedTile) + new Vector3(.5f, .5f), placeLayers))
                box.Draw(badColor);
            else
                box.Draw(Color.black);
        }
        else if (ray.collider.tag == "Block")
        {
            box.transform.position = ray.transform.position - ray.transform.right * .5f - ray.transform.up * .5f;
            box.transform.rotation = ray.transform.rotation;
            box.Draw(Color.black);
        }
        else if (ray.collider.tag == "TileMap")
        {
            box.transform.position = selectedTile;
            box.transform.rotation = Quaternion.identity;
            box.Draw(Color.black);
        }
        else
        {
            box.HideLines();
        }

        //if left click
        if (Input.GetMouseButtonDown(0))
        {

            //If current type of block exists
            if (currentBlock != null)
            {
                //mouse check where position. Set selection to center of block
                if (Physics2D.OverlapPoint(tilemp.CellToWorld(selectedTile) + new Vector3(.5f, .5f), placeLayers))
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
                    StartCoroutine(player.PickupBlock(block, true));
                }
                else if (ray.collider.tag == "Block")
                {
                    GameObject block = ray.collider.gameObject;
                    currentBlock = ray.collider.gameObject.GetComponent<Block>().tile;
                    StartCoroutine(player.PickupBlock(block, false));
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

    public void DestroyArea(Vector3 hitLocation)
    {
        Vector3 Hit = hitLocation;
        Vector3Int selectedTile = tilemp.WorldToCell(Hit);
        Vector3Int TopLeft = tilemp.WorldToCell(Hit) + new Vector3Int(-4 , 4 , 0);

        for (int i = 0; i < Mathf.Sqrt(explosion.Length); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(explosion.Length); j++)
            {
                if(tilemp.GetTile(selectedTile + new Vector3Int(j, -i, 0)) != null)
                {
                    Instantiate(crumbs, selectedTile + new Vector3Int(j, -i, 0), Quaternion.identity);
                    tilemp.SetTile(selectedTile + new Vector3Int(j, -i, 0), null);
                }
            }
        }
    }
}

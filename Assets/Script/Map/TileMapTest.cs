using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileMapTest : MonoBehaviour
{

    public Tilemap tilemap;
    public TileBase tile;

    public Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        this.grid = GetComponent<Grid>();
        // 清空所有格子
        // tilemap.ClearAllTiles();
        TileBase tile1 = tilemap.GetTile(new Vector3Int(0, 12, 0));
        if (tile1 != null)
        {
            Debug.Log(tile1.ToString());
            tilemap.SetTile(Vector3Int.zero, tile1);

            //tilemap.SetTile(new Vector3Int(0,13,0),null);
            //tilemap.GetTilesBlock()
            //tilemap.SetTiles()
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Debug.LogFormat("鼠标位置：+{0},{1},{2}", Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.LogFormat("世界位置：+{0},{1},{2}", worldPos.x, worldPos.y, worldPos.z);

            Vector3Int cellPos = grid.WorldToCell(worldPos);
            this.ExplosionLogic(cellPos);
        }
    }

    void ExplosionLogic(Vector3Int cellPos)
    {
        tilemap.SetTile(cellPos, null);
    }
}

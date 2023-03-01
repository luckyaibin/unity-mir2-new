using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;
using System.IO;
using UnityEditor;
using System.Linq;



public class MapLoad : MonoBehaviour
{
    Tilemap ground, grass;

    [SerializeField]
    public string MapName = "???";

    public List<TileBase> tiles;

    public void Awake()
    {
        if (ground == null)
        {
            ground = GameObject.Find("Grid/Ground").GetComponent<Tilemap>();
        }
        if (grass == null)
        {
            grass = GameObject.Find("Grid/Grass").GetComponent<Tilemap>();
        }
    }
    [ContextMenu("SaveMap")]
    private void SaveMap()
    {
        Multiple multiple = new Multiple();

        SingleTilemap groud = new SingleTilemap();
        groud.tilemapName = "Ground";
        var v3size = ground.size;
        for (int i = 0; i < v3size.x; i++)
        {
            for (int j = 0; j < v3size.y; j++)
            {
                var pos = new Vector3Int(i, j, 0);
                var tileBase = ground.GetTile(pos);
                if (tileBase != null)
                {
                    var t = new SingleTile();
                    t.tileName = tileBase.name;
                    t.x = i;
                    t.y = j;
                    groud.singleTilemaps.Add(t);
                }
            }
        }


        SingleTilemap grass = new SingleTilemap();
        grass.tilemapName = "Grass";
        var grassV3size = ground.size;
        for (int i = 0; i < grassV3size.x; i++)
        {
            for (int j = 0; j < grassV3size.y; j++)
            {
                var pos = new Vector3Int(i, j, 0);
                var tileBase = ground.GetTile(pos);
                if (tileBase != null)
                {
                    var t = new SingleTile();
                    t.tileName = tileBase.name;
                    t.x = i;
                    t.y = j;
                    grass.singleTilemaps.Add(t);
                }
            }
        }

        multiple.Add(groud);
        multiple.Add(grass);

        // json
        var jsonData = JsonUtility.ToJson(multiple);
        var bytes = System.Text.Encoding.ASCII.GetBytes(jsonData);
        FileStream file = File.Create(Application.streamingAssetsPath + "/" + MapName + ".json");
        file.Dispose();
        file.Write(bytes, 0, bytes.Length);
    }

    [ContextMenu("LoadMap")]
    private void LoadMap()
    {
        Multiple multiple;
        multiple = JsonUtility.FromJson<Multiple>(File.ReadAllText(Application.streamingAssetsPath + "/" + MapName + ".json"));
        for (int m = 0; m < multiple.Count; m++)
        {
            var singleTilemap = multiple.Get(m);
            var tmc = GameObject.Find("Grid/" + singleTilemap.tilemapName).GetComponent<Tilemap>();

            for (int i = 0; i < singleTilemap.singleTilemaps.Count; i++)
            {
                var tileInfo = singleTilemap.singleTilemaps[i];
                YourTileClass tileInstance = ScriptableObject.CreateInstance<YourTileClass>();
                Texture2D tex = Resources.Load<Texture2D>("tileTexture") as Texture2D;
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, 400, 400), new Vector2(0.5f, 0.5f));
                tileInstance.sprite = sprite;
                tmc.SetTile(new Vector3Int(tileInfo.x,tileInfo.y, 0), tileInstance);
            }
        }
    }
}



/// <summary>
/// 多个瓦片地图的布局
/// </summary>
public class Multiple
{
    private List<SingleTilemap> singleTilemaps = new List<SingleTilemap>();
    public void Add(SingleTilemap singlemap)
    {
        this.singleTilemaps.Add(singlemap);
    }
    public int Count
    {
        get { return this.singleTilemaps.Count; }
    }
    public SingleTilemap Get(int i)
    {
        return this.singleTilemaps[i];
    }
}

public class SingleTilemap
{
    public string tilemapName;
    public List<SingleTile> singleTilemaps = new List<SingleTile>();
}

public class SingleTile
{
    public string tileName;
    public int x, y;
}
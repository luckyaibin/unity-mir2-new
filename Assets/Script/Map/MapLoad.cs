using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;
using System.IO;
using UnityEditor;
using System.Linq;


[Serializable]
public class MapLoad : MonoBehaviour
{
    [SerializeField]
    Tilemap ground, grass;

    [SerializeField]
    public string MapName = "???";

    public List<TileBase> tiles;

    public void Awake()
    {
        if (ground == null)
        {
            ground = GameObject.Find("Map/Ground").GetComponent<Tilemap>();
        }
        // if (grass == null)
        // {
        //     grass = GameObject.Find("Grid/Grass").GetComponent<Tilemap>();
        // }
    }
    [ContextMenu("SaveMap")]
    public void SaveMap()
    {
        Multiple multiple = new Multiple();

        SingleTilemap singleGround = new SingleTilemap();
        singleGround.tilemapName = "Ground";
        var v3size = this.ground.size;

        for (int i = this.ground.cellBounds.min.x; i < this.ground.cellBounds.max.x; i++)
        {
            for (int j = this.ground.cellBounds.min.y; j < this.ground.cellBounds.max.y; j++)
            {
                var pos = new Vector3Int(i, j, 0);
                var tileBase = ground.GetTile(pos);
                if (tileBase != null)
                {
                    var t = new SingleTile();
                   // t.n = tileBase.name;
                    t.x = i;
                    t.y = j;
                    singleGround.singleTilemaps.Add(t);
                }
            }
        }


        // SingleTilemap singleGrass = new SingleTilemap();
        // singleGrass.tilemapName = "Grass";
        // var grassV3size = this.grass.size;
        // for (int i = 0; i < grassV3size.x; i++)
        // {
        //     for (int j = 0; j < grassV3size.y; j++)
        //     {
        //         var pos = new Vector3Int(i, j, 0);
        //         var tileBase = ground.GetTile(pos);
        //         if (tileBase != null)
        //         {
        //             var t = new SingleTile();
        //             t.tileName = tileBase.name;
        //             t.x = i;
        //             t.y = j;
        //             singleGrass.singleTilemaps.Add(t);
        //         }
        //     }
        // }

        multiple.Add(singleGround);
        // multiple.Add(singleGrass);

        // json
        var jsonData = JsonUtility.ToJson(multiple);
        var bytes = System.Text.Encoding.ASCII.GetBytes(jsonData);
        FileStream file = File.Create(Application.streamingAssetsPath + "/" + MapName + ".json");
        file.Write(bytes, 0, bytes.Length);
        file.Dispose();
    }

    [ContextMenu("LoadMap")]
    private void LoadMap()
    {
        Multiple multiple;
        multiple = JsonUtility.FromJson<Multiple>(File.ReadAllText(Application.streamingAssetsPath + "/" + MapName + ".json"));
        for (int m = 0; m < multiple.Count; m++)
        {
            var singleTilemap = multiple.Get(m);
            var tmc = GameObject.Find("Map/" + singleTilemap.tilemapName+"Loaded").GetComponent<Tilemap>();
            tmc.ClearAllTiles();
            for (int i = 0; i < singleTilemap.singleTilemaps.Count; i++)
            {
                var tileInfo = singleTilemap.singleTilemaps[i];
                MyTileClass tileInstance = ScriptableObject.CreateInstance<MyTileClass>();
                //Texture2D tex = Resources.Load<Texture2D>("tileTexture") as Texture2D;
                // Texture2D tex = Resources.Load("mir/Data/Map/WemadeMir2/Tiles/0") as Texture2D;
                // Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.0f, 1.0f));
                // var ppu = sprite.pixelsPerUnit;
                // tileInstance.sprite = sprite;
                Sprite sprite = Resources.Load<Sprite>("mir/Data/Map/WemadeMir2/Tiles/0");
                tileInstance.sprite = sprite;

                tmc.SetTile(new Vector3Int(tileInfo.x, tileInfo.y, 0), tileInstance);
            }
        }
    }


    public T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        var c = go.GetComponent<T>();
        if (c == null)
        {
            c = go.AddComponent<T>();
        }
        return c;
    }
}




/// <summary>
/// 多个瓦片地图的布局
/// </summary>
[Serializable]
public class Multiple
{
    public List<SingleTilemap> singleTilemaps = new List<SingleTilemap>();
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
[Serializable]
public class SingleTilemap
{
    public string tilemapName;
    public List<SingleTile> singleTilemaps = new List<SingleTile>();
}
[Serializable]
public class SingleTile
{
    public int l;// tile所在库目录对应起来
    public int n;// tile索引(数字形式的名字)
    public int x, y;
    public int w;// walkable 1可行走
}
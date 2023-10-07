using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;
using System.IO;
using UnityEditor;
using System.Linq;

public class MyTileClass : Tile{
    public int l;// tile 所在库目录对应起来
    public int n;// tile 索引(数字形式的名字)
    public int x, y;
    public int w;// walkable 1可行走

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);
    }

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        return base.StartUp(position, tilemap, go);

    }
}

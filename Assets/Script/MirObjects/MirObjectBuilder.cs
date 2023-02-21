using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class MirObjectBuilder<T> where T : Packet
{
    private static GameObject prefab;
    public GameObject getPrefab(string prefabPath)
    {
        if (prefab == null)
        {
            prefab = Resources.Load<GameObject>(prefabPath);
        }
        return prefab;
    }

    public virtual GameObject gameObject(T Packet)
    {
        return null;
    }
    public virtual GameObject gameObject(T Packet, Transform parent)
    {
        return null;
    }
    // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    // public static List<Vector2Int> readOffsets(string offsetPath)
    // {
    //     var offsets = new List<Vector2Int>();
    //     var ppp = Application.dataPath + "/"+  offsetPath;
    //     var fileStream = File.Open(ppp, FileMode.Open);
    //     //var file =  Resources.Load(offsetPath,typeof(TextAsset)) as TextAsset;
    //     //var stream = new MemoryStream(file.bytes);
    //     BinaryReader reader = new BinaryReader(fileStream);
    //     //read count
    //     var count = reader.ReadInt32();
    //     for (var i = 0; i < count; i++)
    //     {
    //         var x = reader.ReadInt32();
    //         var y = reader.ReadInt32();
    //         offsets.Add(new Vector2Int(x, y));
    //     }
    //     reader.Close();
    //     fileStream.Close();
    //     return offsets;
    // }
     public static List<Vector2Int> readOffsets(string offsetPath)
    {
        var offsets = new List<Vector2Int>();
        //var ppp = Application.dataPath + "/"+  offsetPath;
         var fileStream = File.Open(offsetPath, FileMode.Open);
         BinaryReader reader = new BinaryReader(fileStream);
        //read count
        var count = reader.ReadInt32();
        for (var i = 0; i < count; i++)
        {
            var x = reader.ReadInt32();
            var y = reader.ReadInt32();
            offsets.Add(new Vector2Int(x, y));
        }
        reader.Close();
        return offsets;
    }
    public static Vector3 calcPosition(Vector2 npcPosition,Vector2 offset){
        // 动画图片设置成1ppu，也就是每个像素占用一个unity 单位长度
        // 动画图片的锚点设置为左上角（left top）
        // 这里需要想一下，图片的坐标原点和对齐方式，为什么x用原本值，y要取反
        return new Vector3(offset.x,-offset.y,0);
        var x = npcPosition.x * Config.MAP_TILE_WIDTH  + offset.x;
        var y = npcPosition.y * Config.MAP_TILE_HEIGHT + offset.y;
        return new Vector3(-x,-y,0);
    }
}
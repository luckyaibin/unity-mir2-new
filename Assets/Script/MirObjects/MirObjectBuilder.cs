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
        var x = npcPosition.x * Config.MAP_TILE_WIDTH  + offset.x;
        var y = npcPosition.y * Config.MAP_TILE_HEIGHT + offset.y;
        return new Vector3(x,-y,0);
    }
}
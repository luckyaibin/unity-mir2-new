
using System.IO;
using UnityEngine;
using  MLibraryUtils;
class MirCell
{
    public enum Type
    {
        tiles = 0, smtiles = 1, objects_layer = 2, objects_layer_ani = 3
    };
    //大地砖
    public GameObject tiles;
    //小地砖
    public GameObject smtiles;
    public GameObject objects;
    public static GameObject makeMapCell(GameObject parent ,GameObject mapCell, Type cellType, int mapX, int mapY, MImage imageInfo, MapCellControllerListener mapCellControllerListener)
    {
        var point = mapPointToWorldPoint(mapX, mapY, imageInfo, cellType);
        var tmp = UnityEngine.Object.Instantiate(mapCell,parent.transform);
        tmp.transform.localPosition = point;
        tmp.transform.localRotation = Quaternion.identity;

        var com = tmp.GetComponent<MapCell>();
        com.setMapCellControllerListener(mapX, mapY, mapCellControllerListener);
        com.imageInfo = imageInfo;
        tmp.name = cellType.ToString() + "(" + mapX + "," + mapY + ")";
        if (imageInfo==null){
            return tmp;
        }
        if (cellType == Type.tiles)
        {
            //tmp.GetComponent<SpriteRenderer>().sortingLayerName = "map_back";
            tmp.GetComponent<SpriteRenderer>().sortingOrder = -2;
        }
        else if (cellType == Type.smtiles)
        {
            //tmp.GetComponent<SpriteRenderer>().sortingLayerName = "map_middle";
            tmp.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }
        else
        {
            tmp.GetComponent<SpriteRenderer>().sortingLayerName = "map_front";
            tmp.GetComponent<SpriteRenderer>().sortingOrder = mapY;
        }
        tmp.isStatic = true;
        // tmp.GetComponent<SpriteRenderer>().sortingLayerName = "map";
        //tmp.GetComponent<SpriteRenderer>().sortingOrder = mapY * (int)cellType;
        tmp.layer = 8 + (int)cellType;
        if (cellType == Type.objects_layer_ani)
        {
            tmp.GetComponent<SpriteRenderer>().material = Resources.Load<Material>("materials/blend_add");
            var animator = tmp.AddComponent<Animator>();
            var animPath = Directory.GetParent(imageInfo.imagePath).ToString();
            animPath += "/anim/" + Path.GetFileName(imageInfo.imagePath);
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(animPath);
        }
        return tmp;
    }
    private static Vector3 mapPointToWorldPoint(int mapX, int mapY, MImage imageInfo, Type cellType)
    {
        var tmpX = MapConfigs.MAP_TILE_WIDTH * mapX - MapConfigs.OffSetX;
        var tmpY = MapConfigs.MAP_TILE_HEIGHT * mapY;
        if (cellType == Type.objects_layer || cellType == Type.objects_layer_ani)
        {
            tmpY = MapConfigs.MAP_TILE_HEIGHT * (mapY + 1) - imageInfo.Height;
        }
        if (cellType == Type.objects_layer_ani)
        {
            tmpX += imageInfo.X;
            tmpY += imageInfo.Y;
        }
        return new Vector3(tmpX, -tmpY, 0);
    }
}


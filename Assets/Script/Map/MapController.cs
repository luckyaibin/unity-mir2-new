

using UnityEngine;
using UnityEngine.UI;


public class MapController : MonoBehaviour, MapCellControllerListener
{
    public GameObject mapCell;
    // Start is called before the first frame update
    private MapReader mapReader;
    private MirCell[,] mapCells;
    //初始位置
    private int originX = 0;
    private int originY = 0;
    private string mapName;
    private Vector2Int palyerCurrent;//当前角色在地图的位置

    public GameObject player;

    public Text text;

    public void setMapInfo(float px, float py, string mapName)
    {
        var x = px * MapConfigs.MAP_TILE_WIDTH;
        var y = 0 - py * MapConfigs.MAP_TILE_HEIGHT;
        player.transform.position = new Vector3(x, y, 0);

        this.mapName = mapName;
        changeMapInfo();
    }

    private bool mapInitiate()
    {
        return mapReader != null;
    }

    private void Start()
    {
        palyerCurrent = new Vector2Int(-1, -1);
    }

    private void changeMapInfo()
    {
        destroyMirCell();
        mapReader = new MapReader(MapConfigs.MAP_DIR + mapName + ".map");
        mapCells = new MirCell[mapReader.Width, mapReader.Height];
    }

    private void destroyMirCell()
    {
        if (mapCells == null)
        {
            return;
        }
        foreach (var mirCell in mapCells)
        {
            if (mapCell == null)
            {
                continue;
            }
            if (mirCell.tiles != null)
            {
                Destroy(mirCell.tiles);
            }
            if (mirCell.tiles != null)
            {
                Destroy(mirCell.smtiles);
            }
            if (mirCell.tiles != null)
            {
                Destroy(mirCell.objects);
            }
        }
    }



    // Update is called once per frame
    private void Update()
    {
        if (!mapInitiate())
        {
            // setMapInfo(343, 335, "0");
            return;
        }
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, gameObject.transform.position.z);
        var tmpX = (int)(originX + gameObject.transform.position.x / MapConfigs.MAP_TILE_WIDTH);
        var tmpY = (int)(originY - gameObject.transform.position.y / MapConfigs.MAP_TILE_HEIGHT);
        if (tmpX == palyerCurrent.x && tmpY == palyerCurrent.y)
        {
            return;
        }
        palyerCurrent.x = tmpX;
        palyerCurrent.y = tmpY;
        text.text = "(" + tmpX + "," + tmpY + ")";
        for (var i = palyerCurrent.x - MapConfigs.ViewRangeX; i < palyerCurrent.x + MapConfigs.ViewRangeX; i++)
        {
            //行数
            for (var j = palyerCurrent.y - MapConfigs.ViewRangeY; j < palyerCurrent.y + MapConfigs.ViewRangeY + 15; j++)
            {
                if (mapCells[i, j] == null)
                {
                    mapCells[i, j] = new MirCell();
                }
                var mti = mapReader.MapCells[i, j];
                if (mapCells[i, j].tiles == null && j < palyerCurrent.y + MapConfigs.ViewRangeY)
                {
                    if (mti.hasBackImage(i, j))
                    {
                        var imageInfo = MapResInfoLoader.GetInstance().GetMImageInfo(MapConfigs.MAP_LIBS[mti.BackIndex], mti.getBackImageIndex());
                        var objectCell = MirCell.makeMapCell(mapCell, MirCell.Type.tiles, i, j, imageInfo, this);
                        mapCells[i, j].tiles = objectCell;
                    }
                }
                if (mapCells[i, j].smtiles == null && j < palyerCurrent.y + MapConfigs.ViewRangeY)
                {
                    if (mti.hasMiddleImage())
                    {
                        var imageInfo = MapResInfoLoader.GetInstance().GetMImageInfo(MapConfigs.MAP_LIBS[mti.MiddleIndex], mti.getMiddleImageIndex());
                        var objectCell = MirCell.makeMapCell(mapCell, MirCell.Type.smtiles, i, j, imageInfo, this);
                        mapCells[i, j].smtiles = objectCell;
                    }
                }

                if (mapCells[i, j].objects == null)
                {
                    if (mti.hasFrontAnimation())
                    {
                        var imageInfo = MapResInfoLoader.GetInstance().GetMImageInfo(MapConfigs.MAP_LIBS[mti.FrontIndex], mti.getFrontImageIndex());
                        var objectCell = MirCell.makeMapCell(mapCell, MirCell.Type.objects_layer_ani, i, j, imageInfo, this);
                        mapCells[i, j].objects = objectCell;
                    }
                    else if (mti.hasFrontImage())
                    {
                        var imageInfo = MapResInfoLoader.GetInstance().GetMImageInfo(MapConfigs.MAP_LIBS[mti.FrontIndex], mti.getFrontImageIndex());
                        var objectCell = MirCell.makeMapCell(mapCell, MirCell.Type.objects_layer, i, j, imageInfo, this);
                        mapCells[i, j].objects = objectCell;
                    }
                }

                //if (mapCells[i, j].objects == null)
                //{
                //    var libName = "Objects";
                //    if (mti.objFileIdx != 0)
                //    {
                //        libName += mti.objFileIdx;
                //    }
                //    if (mti.hasAni)
                //    {
                //        var imageInfo = TextureLoader.Instance.getImageInfoAsync(libName, mti.objImgIdx);
                //        var objectCell = MirCell.makeMapCell(mapCell, MirCell.Type.objects_layer_ani, x, y, i, j, imageInfo, this);
                //        mapCells[i, j].objects = objectCell;
                //    }
                //    else if (mti.hasObj)
                //    {
                //        var imageInfo = TextureLoader.Instance.getImageInfoAsync(libName, mti.objImgIdx);
                //        var objectCell = MirCell.makeMapCell(mapCell, MirCell.Type.objects_layer, x, y, i, j, imageInfo, this);
                //        mapCells[i, j].objects = objectCell;
                //    }
            }

        }
    }
    //绘制object


    //绘制地板
    private void drawFloor()
    {
        for (int y = palyerCurrent.y - MapConfigs.ViewRangeY; y <= palyerCurrent.y + MapConfigs.ViewRangeY; y++)
        {
            for (int x = palyerCurrent.x - MapConfigs.ViewRangeX; x <= palyerCurrent.x + MapConfigs.ViewRangeX; x++)
            {

            }
        }

    }

    //判断地砖是否在范围内
    private void checkGameObjectVisible(GameObject gameObject)
    {

    }

    public void update(int mapX, int mapY, GameObject gameObject)
    {
        // mapCells.Remove(key);
        //GameObject.Find(key);
        // mapCells[mapX, mapY] = null;
        // Destroy(gameObject);
        // gameObject.SetActive(false);
        // x = max -

        if ((palyerCurrent.x - MapConfigs.ViewRangeX <= mapX && mapX <= palyerCurrent.x + MapConfigs.ViewRangeX) &&
            (palyerCurrent.y - MapConfigs.ViewRangeY <= mapY && mapY <= palyerCurrent.y + MapConfigs.ViewRangeY + 15))
        {
            //在显示范围内
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

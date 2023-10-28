

using UnityEngine;
using UnityEngine.UI;


public class MapController : MonoBehaviour, MapCellControllerListener
{
    public GameObject mapCell;
    private MapResInfoLoader mapLoader;
    // Start is called before the first frame update
    private MapReader mapReader;
    private MirCell[,] mapCells;
    //初始位置
    private int originX = 0;
    private int originY = 0;
    private string mapName;
    private Vector2Int palyerCurrentGrid;//当前角色在地图的位置

    public GameObject player;

    public Text text;

    // 玩家初始所在的坐标,单位为格子宽高的float值.)
    // 促使px 和 py是相对地图左上角的坐标,需要转换
    public void setMapInfo(float px, float py, string mapName)
    {
        var x = px * MapConfigs.MAP_TILE_WIDTH;
        var y = 0 - py * MapConfigs.MAP_TILE_HEIGHT;
        player.transform.localPosition = new Vector3(x, y, 0);

        this.mapName = mapName;
        changeMapInfo();
    }

    private bool isMapInitiated()
    {
        return mapReader != null;
    }

    private void Start()
    {
        palyerCurrentGrid = new Vector2Int(-1, -1);
        mapLoader = MapResInfoLoader.GetInstance();
    }

    private void changeMapInfo()
    {
        destroyMirCell();
        mapReader = new MapReader(MapConfigs.MAP_DIR + mapName + ".map");
        mapReader.Initiate();
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
            if (mirCell == null)
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
        var speed = 10f;
        if (Input.GetKey(KeyCode.A))
        {
            var pos = player.transform.localPosition;
            var newPos = new Vector3(pos.x - speed, pos.y, pos.z);
            player.transform.localPosition = newPos;
        }
        if (Input.GetKey(KeyCode.D))
        {
            var pos = player.transform.localPosition;
            var newPos = new Vector3(pos.x + speed, pos.y, pos.z);
            player.transform.localPosition = newPos;
        }
        if (Input.GetKey(KeyCode.W))
        {
            var pos = player.transform.localPosition;
            var newPos = new Vector3(pos.x, pos.y + speed, pos.z);
            player.transform.localPosition = newPos;
        }
        if (Input.GetKey(KeyCode.S))
        {
            var pos = player.transform.localPosition;
            var newPos = new Vector3(pos.x, pos.y - speed, pos.z);
            player.transform.localPosition = newPos;
        }
        if (!isMapInitiated())
        {
            return;
        }
        // 把地图往角色相反方向移动,这样子就保证角色一直在0,0点
        gameObject.transform.localPosition = new Vector3(-player.transform.localPosition.x, -player.transform.localPosition.y, gameObject.transform.localPosition.z);
        var tmpX = (int)(originX + player.transform.localPosition.x / MapConfigs.MAP_TILE_WIDTH);
        var tmpY = (int)(originY - player.transform.localPosition.y / MapConfigs.MAP_TILE_HEIGHT);
        if (tmpX == palyerCurrentGrid.x && tmpY == palyerCurrentGrid.y)
        {
            return;
        }
        Logger.Debugf(" 渲染新格子......");
        // 更新角色所在 [格子] 坐标
        palyerCurrentGrid.x = tmpX;
        palyerCurrentGrid.y = tmpY;
        // 更新角色周围可视范围的地图格子
        text.text = "(" + tmpX + "," + tmpY + ")";
        for (var i = palyerCurrentGrid.x - MapConfigs.ViewRangeX; i < palyerCurrentGrid.x + MapConfigs.ViewRangeX; i++)
        {
            //行数
            for (var j = palyerCurrentGrid.y - MapConfigs.ViewRangeY; j < palyerCurrentGrid.y + MapConfigs.ViewRangeY + 15; j++)
            {
               
                if (i < 0 || i >= mapCells.GetLength(0))
                {
                    continue;
                }
                if (j < 0 || j >= mapCells.GetLength(1))
                {
                    continue;
                } 
                Logger.Debugf("更新格子坐标 i:%d,j:%d", i, j);
                if (mapCells[i, j] == null)
                {
                    mapCells[i, j] = new MirCell();
                }
                var mti = mapReader.MapCells[i, j];
                if (mapCells[i, j].tiles == null && j < palyerCurrentGrid.y + MapConfigs.ViewRangeY)
                {
                    if (mti.hasBackImage(i, j))
                    {
                        var imageInfo = mapLoader.GetMImageInfo(mti.BackIndex, mti.getBackImageIndex());
                        if (imageInfo == null)
                        {
                            continue;
                        }
                        var objectCell = MirCell.makeMapCell(this.gameObject, mapCell, MirCell.Type.tiles, i, j, imageInfo, this);
                        mapCells[i, j].tiles = objectCell;
                    }
                }
                if (mapCells[i, j].smtiles == null && j < palyerCurrentGrid.y + MapConfigs.ViewRangeY)
                {
                    if (mti.hasMiddleImage())
                    {
                        var imageInfo = mapLoader.GetMImageInfo(mti.MiddleIndex, mti.getMiddleImageIndex());
                        if (imageInfo == null)
                        {
                            continue;
                        }
                        var objectCell = MirCell.makeMapCell(this.gameObject, mapCell, MirCell.Type.smtiles, i, j, imageInfo, this);
                        mapCells[i, j].smtiles = objectCell;
                    }
                }

                if (mapCells[i, j].objects == null)
                {
                    if (mti.hasFrontAnimation())
                    {
                        var imageInfo = mapLoader.GetMImageInfo(mti.FrontIndex, mti.getFrontImageIndex());
                        if (imageInfo == null)
                        {
                            continue;
                        }
                        var objectCell = MirCell.makeMapCell(this.gameObject, mapCell, MirCell.Type.objects_layer_ani, i, j, imageInfo, this);
                        mapCells[i, j].objects = objectCell;
                    }
                    else if (mti.hasFrontImage())
                    {
                        var imageInfo = mapLoader.GetMImageInfo(mti.FrontIndex, mti.getFrontImageIndex());
                        if (imageInfo == null)
                        {
                            continue;
                        }
                        var objectCell = MirCell.makeMapCell(this.gameObject, mapCell, MirCell.Type.objects_layer, i, j, imageInfo, this);
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
        for (int y = palyerCurrentGrid.y - MapConfigs.ViewRangeY; y <= palyerCurrentGrid.y + MapConfigs.ViewRangeY; y++)
        {
            for (int x = palyerCurrentGrid.x - MapConfigs.ViewRangeX; x <= palyerCurrentGrid.x + MapConfigs.ViewRangeX; x++)
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

        if ((palyerCurrentGrid.x - MapConfigs.ViewRangeX <= mapX && mapX <= palyerCurrentGrid.x + MapConfigs.ViewRangeX) &&
            (palyerCurrentGrid.y - MapConfigs.ViewRangeY <= mapY && mapY <= palyerCurrentGrid.y + MapConfigs.ViewRangeY + 15))
        {
            //在显示范围内
        }
        else
        {
            Destroy(gameObject);
        }
    }
}


using UnityEngine;

using MLibraryUtils;
using QFramework;
public class MapCell : MonoBehaviour
{
    // Start is called before the first frame update

    public MImage imageInfo;
 
    private MapCellControllerListener mapCellDestoryListener;
    public int mapX;
    public int mapY;

    public void setMapCellControllerListener(int mapX, int mapY, MapCellControllerListener mapCellDestoryListener)
    {
        this.mapCellDestoryListener = mapCellDestoryListener;
        this.mapX = mapX;
        this.mapY = mapY;
    }
    private void Start()
    {
        var mySprite = Resources.Load<Sprite>(imageInfo.imagePath);
        if (mySprite==null){
            // Logger.Errorf("不能找到 sprite:%s, 尝试直接用图片创建",imageInfo.imagePath);
            var tex = Resources.Load<Texture2D>(imageInfo.imagePath);
            Rect r = new Rect(0,0,tex.width,tex.height);
            mySprite = Sprite.Create(tex,r,new Vector2(0,1),1);
            // Logger.Errorf("创建完成 sprite:%s",mySprite);
        }
        var spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = mySprite;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(imageInfo.Width, imageInfo.Height);
        var timer = QFramework.Timer.Instance.Post2Really(()=>{
            LogKit.E("die......");
        },10,-1);
    //    QFramework.TimeExtend.Timer.AddTimer(10,"mapcellCheck",true);
    //     timer.Delay(10,()=>{
    //         LogKit.E("die......");
    //     });
       
    }
    private void Update()
    {
        // Physics.Raycast(transform);
        if (mapCellDestoryListener != null)
        {
            mapCellDestoryListener.update(mapX, mapY, gameObject);
        }
    }
}
 
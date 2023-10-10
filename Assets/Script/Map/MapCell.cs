
using UnityEngine;

using MLibraryUtils;
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
        var spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = mySprite;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(imageInfo.Width, imageInfo.Height);
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
 
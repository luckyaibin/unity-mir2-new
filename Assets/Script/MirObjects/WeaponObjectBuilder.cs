
using System.Collections.Generic;
using ServerPackets;
using UnityEngine;


public class WeaponObjectBuilder:MirObjectBuilder<ObjectPlayer> {
private static readonly string RES_DIR =  "mir/Data/CWeapon/";
    private static readonly string OFFSET_INFO_PATH = "Assets/Resources/mir/Data/CWeapon/CWeapon.info";

    public static List<Vector2Int> offsets = readOffsets(OFFSET_INFO_PATH);

    public override GameObject gameObject(ObjectPlayer objectPlayer, Transform parent)
    {
        var npcPrefab = getPrefab("prefabs/npc");
        var anim = npcPrefab.GetComponent<Animator>();
        var resIndex = objectPlayer.Weapon.ToString("00");
        var runtimeAnimatorControllerPath = RES_DIR + resIndex + "/anim/" + resIndex;
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(runtimeAnimatorControllerPath);

        //TODO 先Instantiate实例化，再设置动画？
        var mirGameObject = UnityEngine.Object.Instantiate(npcPrefab, parent);
        var offset = offsets[objectPlayer.Weapon];

        mirGameObject.transform.position = calcPosition(objectPlayer.Location, offset);
        mirGameObject.name = "weapon";

        var spriteRenderer = mirGameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = calcSortingOrder((int)objectPlayer.Location.y + 1000,objectPlayer.Direction);
        return mirGameObject;
    }
    public int calcSortingOrder(int sortingOrderParent, MirDirection direction)
    {
        if (direction == MirDirection.DownLeft ||
            direction == MirDirection.Left ||
            direction == MirDirection.UpLeft)
        {
            return sortingOrderParent - 1;
        }
        else if (direction == MirDirection.DownRight ||
          direction == MirDirection.Right ||
          direction == MirDirection.UpRight)
        {
            return sortingOrderParent + 1;
        }
        return sortingOrderParent;
    }
}
using System;
using System.Collections.Generic;
using ServerPackets;
using UnityEngine;


public class HairObjectBuilder : MirObjectBuilder<ObjectPlayer>
{
    private static readonly string RES_DIR = "mir/Data/CHair/";
    private static readonly string OFFSET_INFO_PATH = "Assets/Resources/mir/Data/CHair/CHair.info";

    public static List<Vector2Int> offsets = readOffsets(OFFSET_INFO_PATH);

    public override GameObject gameObject(ObjectPlayer objectPlayer, Transform parent)
    {
        var npcPrefab = getPrefab("prefabs/npc");
        //TODO 先Instantiate实例化，再设置动画？
        var mirGameObject = UnityEngine.Object.Instantiate(npcPrefab, parent);
        var offset = offsets[objectPlayer.Hair];
        mirGameObject.transform.localPosition = calcPosition(objectPlayer.Location, offset);
        //mirGameObject.transform.localPosition = new Vector3(offset.x,0,0);
        mirGameObject.name = "hair";

        var anim = mirGameObject.GetComponent<Animator>();
        var resIndex = objectPlayer.Hair.ToString("00");
        var runtimeAnimatorControllerPath = RES_DIR + resIndex + "/anim/" + resIndex;
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(runtimeAnimatorControllerPath);


        var spriteRenderer = mirGameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = (int)objectPlayer.Location.y + 1000 + 1;
        return mirGameObject;
    }
}
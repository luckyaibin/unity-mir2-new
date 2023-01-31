using System;
using System.Collections.Generic;
using ServerPackets;
using UnityEngine;


public class PlayerObjectBuilder : MirObjectBuilder<ObjectPlayer>
{
    private static string RES_DIR = "mir/Data/CArmour/";
    private static string OFFSET_INFO_PATH = "Assets/Resources/mir/Data/CArmour/CArmour.info";
    public static List<Vector2Int> offsets = readOffsets(OFFSET_INFO_PATH);

    public override GameObject gameObject(ObjectPlayer objectPlayer)
    {
        var npcPrefab = getPrefab("prefabs/npc");
        var anim = npcPrefab.GetComponent<Animator>();
        var resIndex = objectPlayer.Armour.ToString("00");
        var runtimeAnimatorControllerPath = RES_DIR + resIndex + "/anim/" + resIndex;
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(runtimeAnimatorControllerPath);

        //TODO 先Instantiate实例化，再设置动画？
        var mirGameObject = UnityEngine.Object.Instantiate(npcPrefab);
        var offset = offsets[objectPlayer.Armour];

        mirGameObject.transform.position = calcPosition(objectPlayer.Location, offset);
        mirGameObject.transform.rotation = Quaternion.identity;
        mirGameObject.name = "objectPlayer(" + objectPlayer.Name + ")"; ;
        var animController = mirGameObject.AddComponent<PlayerController>();
        animController.objectPlayer = objectPlayer;
        var spriteRenderer = mirGameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "map_front";
        spriteRenderer.sortingOrder = (int)objectPlayer.Location.y + 1000;
        mirGameObject.layer = 10;
        return mirGameObject;
    }
}
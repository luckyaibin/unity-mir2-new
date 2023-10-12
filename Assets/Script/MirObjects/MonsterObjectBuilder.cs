using System;
using System.Collections.Generic;
using System.IO;
 
using ServerPackets;
using UnityEngine;

public class MonsterObjectBuilder : MirObjectBuilder<ObjectMonster>
{
    private static readonly string NPC_RES_DIR = "mir/Data/Monster/";
//    private static readonly string MONSTER_POFFSET_INFO_PATH = MapConfigs.Data_Dir + "Monster/monster.info";
    private static readonly string MONSTER_POFFSET_INFO_PATH ="Assets/Resources/mir/Data/Monster/CMonster.info";
    public static List<Vector2Int> monsterOffsets = readOffsets(MONSTER_POFFSET_INFO_PATH);
    public override GameObject gameObject(ObjectMonster monster)
    {
        var prefab = getPrefab("prefabs/npc");
        var anim = prefab.GetComponent<Animator>();
        int image = (int)monster.Image;

        var npcResIndex = image.ToString("000");
        var runtimeAnimatorControllerPath = NPC_RES_DIR + npcResIndex + "/anim/" + npcResIndex;
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(runtimeAnimatorControllerPath);
        if (anim.runtimeAnimatorController == null)
        {
            Logger.Debugf("runtimeAnimatorController %s", monster.Name);
        }

        prefab.GetComponent<SpriteRenderer>().sortingLayerName = "map_front";
        prefab.GetComponent<SpriteRenderer>().sortingOrder = (int)monster.Location.y + 1000;
        prefab.layer = 10;

        var imageIndex = (int)monster.Image;
        if (imageIndex < 0 || image >= monsterOffsets.Count)
        {
            Logger.Debugf("MonsterObjectBuilder %s",  monster.Image);
            Logger.Debugf("MonsterObjectBuilder %s", imageIndex);
            imageIndex = 0;
        }
        var offset = monsterOffsets[imageIndex];
        var mirGameObject = UnityEngine.GameObject.Instantiate(prefab, calcPosition(monster.Location, offset), Quaternion.identity);
        var controller = mirGameObject.AddComponent<MonsterController>();
        controller.objectMonster = monster;
        mirGameObject.name = "objectMonster(" + monster.Name + ")";
        return mirGameObject;
    }
}

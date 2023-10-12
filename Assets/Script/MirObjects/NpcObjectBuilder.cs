using System.Collections;
using System.Collections.Generic;
using System.IO;
 
using ServerPackets;
using UnityEngine;


public class NpcObjectBuilder : MirObjectBuilder<ObjectNPC>
{
    private static string NPC_DIR_PATH = MapConfigs.Data_Dir + "NPC";
    private static List<Vector2Int> npcOffsets;
    private static readonly string NPC_RES_DIR = "Assets/Resources/mir/Data/NPC";
    static NpcObjectBuilder()
    {
        var npcOffsetInfoPath = NPC_RES_DIR + "/CNPC.info";
        UnityEngine.Debug.Log("NpcObjectBuilder npcOffsetInfoPath ------>" + npcOffsetInfoPath);
        //UnityEngine.Debug.Log("NpcObjectBuilder NPC_DIR_PATH ------>" + NPC_DIR_PATH );
        npcOffsets = readOffsets(npcOffsetInfoPath);
    }
    public override GameObject gameObject(ObjectNPC npc)
    {
        var npcPrefab = getPrefab("prefabs/npc");
        var anim = npcPrefab.GetComponent<Animator>();
        var npcResIndex = npc.Image.ToString("00");
        var runtimeAnimatorControllerPath = NPC_RES_DIR + npcResIndex + "/anim/" + npcResIndex;
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(runtimeAnimatorControllerPath);
        npcPrefab.GetComponent<SpriteRenderer>().sortingLayerName = "map_front";
        npcPrefab.GetComponent<SpriteRenderer>().sortingOrder = (int)npc.Location.y + 1000;
        npcPrefab.layer = 10;

        var mirGameObject = UnityEngine.GameObject.Instantiate(npcPrefab, calcPosition(npc.Location, npcOffsets[npc.Image]), Quaternion.identity);
        var animController = mirGameObject.AddComponent<NPCController>();
        animController.npc = npc;
        mirGameObject.name = "ObjectNPC(" + npc.Name + ")";
        return mirGameObject;
    }
}


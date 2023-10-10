using System;
using System.Collections.Generic;
using ServerPackets;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    private const string TAG = "AnimController";
    private Animator animator;
    private DateTime switchActionTime;

    private List<Tuple<MirAction, MirDirection>> actions = new List<Tuple<MirAction, MirDirection>>();


    private Vector2 npcNameSize;
    private string npcName;
    private GUIStyle npcNameStyle;

    private Vector2 npcVocationSize;
    private string npcVocation;

    private Vector2 npcPosition;

    public ObjectNPC npc;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        var clips = animator.runtimeAnimatorController.animationClips;

        foreach (var clip in clips)
        {
            var actionDirection = clip.name.Split('_');
            var mirAction = (MirAction)Enum.Parse(typeof(MirAction), actionDirection[0]);
            var mirDirection = (MirDirection)Enum.Parse(typeof(MirDirection), actionDirection[1]);
            actions.Add(Tuple.Create<MirAction, MirDirection>(mirAction, mirDirection));

            var animevent = new AnimationEvent();
            animevent.time = clip.length;
            animevent.functionName = "clipCallback";
            animevent.stringParameter = clip.name;
            clip.AddEvent(animevent);
        }
    }


    private void Start()
    {

    }


    private void OnGUI()
    {
        //绘制怪物名称
        drawNpcName();
    }

    private void drawNpcName()
    {
        var x = npc.Location.x * MapConfigs.MAP_TILE_WIDTH;
        var y = 0 - npc.Location.y * MapConfigs.MAP_TILE_HEIGHT;
        npcPosition = Camera.main.WorldToScreenPoint(new Vector3(x, y, 0));
        //得到真实怪物头顶的2D坐标
        npcPosition = new Vector2(npcPosition.x, Screen.height - npcPosition.y);
        calcNameSize();
        if (npcVocationSize == null)
        {

        }
        else
        {
            npcNameStyle.normal.textColor = npc.NameColour;
            GUI.Label(new Rect(npcPosition.x + (48 - npcVocationSize.x) / 2,
                npcPosition.y - (32 - npcVocationSize.y / 2) + 3,
                npcVocationSize.x,
                npcVocationSize.y), npcVocation, npcNameStyle);

            npcNameStyle.normal.textColor = Color.white;
            GUI.Label(new Rect(npcPosition.x + (48 - npcNameSize.x) / 2,
                npcPosition.y - (32 - npcNameSize.y / 2) + 15,
                npcNameSize.x,
                npcNameSize.y), npcName, npcNameStyle);

        }
    }
    private void calcNameSize()
    {
        if (npcNameStyle != null)
        {
            return;
        }
        npcNameStyle = new GUIStyle
        {
            fontSize = 12
        };

        if (npc.Name.Contains("_"))
        {
            string[] splitName = npc.Name.Split('_');
            npcVocation = splitName[0];
            npcName = splitName[1];
            npcVocationSize = npcNameStyle.CalcSize(new GUIContent(splitName[0]));
            npcNameSize = npcNameStyle.CalcSize(new GUIContent(splitName[1]));
        }
        else
        {
            npcNameSize = npcNameStyle.CalcSize(new GUIContent(npc.Name));
        }
    }
    public void clipCallback(string input)
    {
        if (switchActionTime == null)
        {
            switchActionTime = DateTime.Now + TimeSpan.FromSeconds(60);
            return;
        }

        if (DateTime.Now < switchActionTime)
        {
            return;
        }
        switchActionTime = DateTime.Now + TimeSpan.FromSeconds(60);

        var index = UnityEngine.Random.Range(0, actions.Count);
        var aciton = actions[index];
        if (animator.GetInteger("MirDirection") != (int)aciton.Item2 || animator.GetInteger("MirAction") != (int)aciton.Item1)
        {
            animator.SetInteger("MirDirection", (int)aciton.Item2);
            animator.SetInteger("MirAction", (int)aciton.Item1);
        }
        //int num = r.NextInteger(1, 101);
        Logger.Debugf(TAG, "input " + input + " " + aciton);

    }
}

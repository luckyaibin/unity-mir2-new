using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class testScene : MonoBehaviour
{
    public TestPlayerObject testPlayerObject;

    public GameObject itemPrefab;

    private List<Tuple<MirAction, string>> mirAction;
    private List<Tuple<MirDirection, string>> mirDirections;

    private ScrollRect svAct;
    private Transform svContentAct;

    private ScrollRect svDir;
    private Transform svContentDir;

    void Awake()
    {
        this.mirAction = getMirActionForPlayer();
        this.mirDirections = new List<Tuple<MirDirection, string>>
        {
           Tuple.Create(MirDirection.UpLeft, "↖"),
           Tuple.Create(MirDirection.Up, "↑"),
           Tuple.Create(MirDirection.UpRight, "↗"),
           Tuple.Create(MirDirection.Left,  "←"),
           //Tuple.Create(MirDirection.UpLeft, "+"),
           Tuple.Create(MirDirection.Right, "→"),
           Tuple.Create(MirDirection.DownLeft,  "↙"),
           Tuple.Create(MirDirection.Down, "↓"),
           Tuple.Create(MirDirection.DownRight, "↘")
        };
        var svobj1 = this.getSiblingOf(this.transform, "Canvas/Scroll View1");
        this.svContentAct = svobj1.Find("Viewport/Content");
        this.svAct = svobj1.GetComponent<ScrollRect>();

        var svobj2 = this.getSiblingOf(this.transform, "Canvas/Scroll View2");
        this.svContentDir = svobj2.Find("Viewport/Content");
        this.svDir = svobj2.GetComponent<ScrollRect>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //加载列表元素prefab
        var itemPrefab = Resources.Load<GameObject>("prefabs/item");
        foreach (var act in this.mirAction)
        {
            var itemInstance = UnityEngine.Object.Instantiate(itemPrefab);
            itemInstance.transform.SetParent(this.svContentAct);
            var itemComponent = itemInstance.GetComponent<Item>();
            //设置元素属性
            itemComponent.Init(act.Item2);
            var btn = itemInstance.GetComponent<UnityEngine.UI.Button>();
            btn.onClick.AddListener(delegate
            {
                this.onClickAct(act);
            });
        }
        foreach (var dir in this.mirDirections)
        {
            var itemInstance = UnityEngine.Object.Instantiate(itemPrefab);
            itemInstance.transform.SetParent(this.svContentDir);
            var itemComponent = itemInstance.GetComponent<Item>();
            //设置元素属性
            itemComponent.Init(dir.Item2);
            var btn = itemInstance.GetComponent<UnityEngine.UI.Button>();
            btn.onClick.AddListener(delegate
            {
                this.onClickDir(dir);
            });
        }
    }

    void onClickAct(Tuple<MirAction, string> act)
    {
        testPlayerObject.onActionClick(act.Item1);
    }

    void onClickDir(Tuple<MirDirection, string> dir)
    {
        testPlayerObject.onDirectionClick(dir.Item1);
    }

    // Update is called once per frame
    void Update()
    {

    }



    private List<Tuple<MirAction, string>> getMirActionForPlayer()
    {

        return new List<Tuple<MirAction, string>>
        {
        Tuple.Create(MirAction.Standing,MirAction.Standing.ToString() ),
        Tuple.Create(MirAction.Walking, MirAction.Walking.ToString() ),
        Tuple.Create(MirAction.Running, MirAction.Running.ToString() ),
        Tuple.Create(MirAction.Stance,MirAction.Stance.ToString() ),
        Tuple.Create(MirAction.Stance2,MirAction.Stance2.ToString() ),
        Tuple.Create(MirAction.Attack1,MirAction.Attack1.ToString() ),
        Tuple.Create(MirAction.Attack2,MirAction.Attack2.ToString() ),
        Tuple.Create(MirAction.Attack3, MirAction.Attack3.ToString() ),
        Tuple.Create(MirAction.Attack4, MirAction.Attack4.ToString() ),
        Tuple.Create(MirAction.Spell,MirAction.Spell.ToString() ),
        Tuple.Create(MirAction.Harvest, MirAction.Harvest.ToString() ),
        Tuple.Create(MirAction.Struck, MirAction.Struck.ToString() ),
        Tuple.Create(MirAction.Die, MirAction.Die.ToString() ),
        Tuple.Create(MirAction.Dead, MirAction.Dead.ToString() ),
        Tuple.Create(MirAction.Revive, MirAction.Revive.ToString() ),
        Tuple.Create(MirAction.Mine, MirAction.Mine.ToString() ),
        Tuple.Create(MirAction.Lunge,MirAction.Lunge.ToString() )
     };
    }

    Transform getSiblingOf(Transform transform, string name)
    {
        var parent = transform.parent;
        //parent is scene
        if (parent == null)
        {
            var pathTo = name.Split('/');
            var s = this.gameObject.scene;
            var rootObjects = s.GetRootGameObjects();

            for (var j = 0; j < rootObjects.Length; j++)
            {
                if (rootObjects[j].name == pathTo[0])
                {
                    parent = rootObjects[j].transform;
                    break;
                }
            }
            List<string> leftPathTo = new List<string>();

            for (var i = 1; i < pathTo.Length; i++)
            {
                leftPathTo.Add(pathTo[i]);
            }
            //剩余部分的名字，接下来使用Find查找
            name = string.Join("/", leftPathTo);
        }
        if (parent == null)
        {
            return null;
        }
        if (name == "")
        {
            return parent;
        }
        var res = parent.Find(name);
        return res;

    }
}

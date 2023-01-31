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

    private ScrollRect sv;
    private Transform svContent;
    void Awake()
    {
        mirAction = getMirActionForPlayer();
        var svobj = this.getSiblingOf(this.transform, "Canvas/Scroll View");

        //var svobj = parent.Find("Canvas/ScrollView");
        this.svContent = svobj.Find("Viewport/Content");
        this.sv = svobj.GetComponent<ScrollRect>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //加载列表元素prefab
        var itemPrefab = Resources.Load<GameObject>("prefabs/item");
        foreach (var act in this.mirAction)
        {
            var itemInstance = UnityEngine.Object.Instantiate(itemPrefab);
            itemInstance.transform.parent = this.svContent;
            var itemComponent = itemInstance.GetComponent<Item>();
            //设置元素属性
            itemComponent.Init(act.Item2);
        }
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

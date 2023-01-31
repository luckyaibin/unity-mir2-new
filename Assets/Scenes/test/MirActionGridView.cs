using System;
using System.Collections;
using System.Collections.Generic;
using frame8.ScrollRectItemsAdapter.Classic;
using UnityEngine;
using UnityEngine.UI;
using static MirActionGridView;

public class MirActionGridView : ClassicSRIA<CellViewsHolder>
{

    public TestPlayerObject testPlayerObject;


    public GameObject itemPrefab;

    private List<Tuple<MirAction, string>> mirAction;

    protected override void Awake()
    {
        base.Awake();
        mirAction = getMirActionForPlayer();
    }


    protected override void Start()
    {
        base.Start();
        ResetItems(mirAction.Count);
    }

    protected override CellViewsHolder CreateViewsHolder(int itemIndex)
    {
        var instance = new CellViewsHolder();
        instance.Init(itemPrefab, itemIndex);
        instance.button.onClick.AddListener(delegate
        {
            onClick(mirAction[itemIndex].Item1);
        });
        return instance;
    }

    protected override void UpdateViewsHolder(CellViewsHolder vh)
    {
        vh.action.text = mirAction[vh.ItemIndex].Item2;
    }




    public class CellViewsHolder : CAbstractViewsHolder
    {
        public Text action;

        public Button button;

        public override void CollectViews()
        {
            base.CollectViews();
            var go = this.root.Find("mir_action");
            action = go.GetComponent<Text>();
            action.fontSize = 25;
            button = root.gameObject.GetComponent<Button>();
        }
    }

    private void onClick(MirAction mirAction)
    {
        testPlayerObject.onActionClick(mirAction);
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
}

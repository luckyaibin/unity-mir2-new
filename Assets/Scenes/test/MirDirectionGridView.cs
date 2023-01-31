using System;
using System.Collections;
using System.Collections.Generic;
using frame8.ScrollRectItemsAdapter.Classic;
using UnityEngine;
using UnityEngine.UI;
using static MirDirectionGridView;

public class MirDirectionGridView : ClassicSRIA<CellViewsHolder>
{
    public TestPlayerObject testPlayerObject;
    public GameObject itemPrefab;

    private List<Tuple<MirDirection, string>> mirDirections;

    protected override void Awake()
    {
        base.Awake();
        mirDirections = new List<Tuple<MirDirection, string>>
        {
           Tuple.Create(MirDirection.UpLeft, "↖"),
           Tuple.Create(MirDirection.Up, "↑"),
           Tuple.Create(MirDirection.UpRight, "↗"),
          Tuple.Create(MirDirection.Left,  "←"),
           Tuple.Create(MirDirection.UpLeft, "+"),
           Tuple.Create(MirDirection.Right, "→"),
          Tuple.Create(MirDirection.DownLeft,  "↙"),
           Tuple.Create(MirDirection.Down, "↓"),
           Tuple.Create(MirDirection.DownRight, "↘")
        };
    }
    protected override void Start()
    {
        base.Start();
        ResetItems(mirDirections.Count);
    }

    protected override CellViewsHolder CreateViewsHolder(int itemIndex)
    {
        var instance = new CellViewsHolder();
        instance.Init(itemPrefab, itemIndex);
        instance.button.onClick.AddListener(delegate
        {
            onClick(mirDirections[itemIndex].Item1);
        });
        return instance;
    }

    protected override void UpdateViewsHolder(CellViewsHolder vh)
    {
        vh.action.text = mirDirections[vh.ItemIndex].Item2;
    }
    public class CellViewsHolder : CAbstractViewsHolder
    {
        public Text action;

        public Button button;

        public override void CollectViews()
        {
            base.CollectViews();

            action = root.Find("mir_action").GetComponent<Text>();
            button = root.gameObject.GetComponent<Button>();
        }
    }
    private void onClick(MirDirection mirDirection)
    {
        testPlayerObject.onDirectionClick(mirDirection);
    }
}

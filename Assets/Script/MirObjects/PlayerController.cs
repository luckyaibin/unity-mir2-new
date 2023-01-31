using System;
using DG.Tweening;
using ServerPackets;
using UnityEngine;

public class PlayerController : MirBaseController
{
    private static HairObjectBuilder hairObjectBuilder = new HairObjectBuilder();
    private static WeaponObjectBuilder weaponObjectBuilder = new WeaponObjectBuilder();

    // 玩家的数据(放在网络数据包里)
    public ObjectPlayer objectPlayer;

    private Animator hairAnimator;
    private Animator weaponAnimator;

    protected override void onStart()
    {
        var hairGameObject = hairObjectBuilder.gameObject(objectPlayer, this.gameObject.transform);
        this.hairAnimator = hairGameObject.GetComponent<Animator>();
        var weaponGameObject = weaponObjectBuilder.gameObject(objectPlayer, this.gameObject.transform);
        this.weaponAnimator = weaponGameObject.GetComponent<Animator>();

        playAnim(this.animator, MirAction.Standing, objectPlayer.Direction);
        playAnim(this.hairAnimator, MirAction.Standing, objectPlayer.Direction);
        playAnim(this.weaponAnimator, MirAction.Standing, objectPlayer.Direction);
    }

    public void playAnim(Animator animator, MirAction mirAction, MirDirection mirDirection)
    {
        animator.SetInteger(Mir_Action, (int)mirAction);
        animator.SetInteger(Mir_Direction, (int)mirDirection);
    }

    public void playAnimAll(MirAction mirAction, MirDirection mirDirection)
    {
        playAnim(this.hairAnimator, MirAction.Standing, objectPlayer.Direction);
        playAnim(this.weaponAnimator, MirAction.Standing, objectPlayer.Direction);

        var index = this.animator.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        weaponAnimator.gameObject.GetComponent<SpriteRenderer>().sortingOrder = weaponObjectBuilder.calcSortingOrder(index, mirDirection);
        playAnim(this.animator, MirAction.Standing, objectPlayer.Direction);
    }

    protected override void calcNameSize()
    {
        if (this.objectNameStyle != null)
        {
            return;
        }
        this.objectNameStyle = new GUIStyle();
        this.objectNameStyle.fontSize = 12;
        this.objectNameSize = this.objectNameStyle.CalcSize(new GUIContent(this.objectPlayer.Name));
    }

    protected override void drawObjectName(float x, float y)
    {
        this.objectNameStyle.normal.textColor = Color.white;
        var rect = new Rect(
            x + (48 - this.objectNameSize.x) / 2,
            y - (32 - this.objectNameSize.y) / 2,
            this.objectNameSize.x,
            this.objectNameSize.y);

        GUI.Label(rect, this.objectPlayer.Name, this.objectNameStyle);
    }

    protected override Vector2 getObjectOffset()
    {
        throw new NotImplementedException();
    }

    public void objectRun(ObjectRun objectRun)
    {
        var offset = PlayerObjectBuilder.offsets[this.objectPlayer.Armour];
        var targetPosition = PlayerObjectBuilder.calcPosition(objectRun.Location, offset);
        this.gameObject.transform.DOMove(targetPosition, 0.6f)
        .SetUpdate(true)
        .SetEase(Ease.Linear);
        playAnimAll(MirAction.Running,objectRun.Direction);
    }
    public void objectWalk(ObjectRun objectRun)
    {
        var offset = PlayerObjectBuilder.offsets[this.objectPlayer.Armour];
        var targetPosition = PlayerObjectBuilder.calcPosition(objectRun.Location, offset);
        this.gameObject.transform.DOMove(targetPosition, 0.6f)
        .SetUpdate(true)
        .SetEase(Ease.Linear);
        playAnimAll(MirAction.Walking,objectRun.Direction);
    }
}

using System;
using DG.Tweening;
using ServerPackets;
using UnityEngine;

public class PlayerController : MirBaseController
{
    private static PlayerObjectBuilder playerObjectBuilder = new PlayerObjectBuilder();
    private static HairObjectBuilder hairObjectBuilder = new HairObjectBuilder();
    private static WeaponObjectBuilder weaponObjectBuilder = new WeaponObjectBuilder();

    // 玩家的数据(放在网络数据包里)
    public ObjectPlayer objectPlayerData;

    private Animator playerAnimator;
    private Animator hairAnimator;
    private Animator weaponAnimator;

    public override void onInit(System.Object data)
    {

        this.objectPlayerData = (ObjectPlayer)data;

        this.transform.position = new Vector3(0,0,0);

        var playerGameObject = playerObjectBuilder.gameObject(this.objectPlayerData,this.transform);
        this.playerAnimator = playerGameObject.GetComponent<Animator>();

        var hairGameObject = hairObjectBuilder.gameObject(objectPlayerData, this.transform);
        this.hairAnimator = hairGameObject.GetComponent<Animator>();

        var weaponGameObject = weaponObjectBuilder.gameObject(objectPlayerData, this.transform);
        this.weaponAnimator = weaponGameObject.GetComponent<Animator>();

        // playAnim(this.animator, MirAction.Standing, objectPlayerData.Direction);
        // playAnim(this.hairAnimator, MirAction.Standing, objectPlayerData.Direction);
        // playAnim(this.weaponAnimator, MirAction.Standing, objectPlayerData.Direction);
        this.playAnimArmourHairWeapon(MirAction.Standing,objectPlayerData.Direction);
    }

    public void playAnim(Animator animator, MirAction mirAction, MirDirection mirDirection)
    {
        animator.SetInteger(Mir_Action, (int)mirAction);
        animator.SetInteger(Mir_Direction, (int)mirDirection);
    }

    public void playAnimArmourHairWeapon(MirAction mirAction, MirDirection mirDirection)
    {
        playAnim(this.hairAnimator, mirAction, mirDirection);
        playAnim(this.weaponAnimator, mirAction, mirDirection);

        var index = this.playerAnimator.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        weaponAnimator.gameObject.GetComponent<SpriteRenderer>().sortingOrder = weaponObjectBuilder.calcSortingOrder(index, mirDirection);
        playAnim(this.playerAnimator, mirAction, mirDirection);
    }

    protected override void calcNameSize()
    {
        if (this.objectNameStyle != null)
        {
            return;
        }
        this.objectNameStyle = new GUIStyle();
        this.objectNameStyle.fontSize = 12;
        this.objectNameSize = this.objectNameStyle.CalcSize(new GUIContent(this.objectPlayerData.Name));
    }

    protected override void drawObjectName(float x, float y)
    {
        this.objectNameStyle.normal.textColor = Color.white;
        var rect = new Rect(
            x + (48 - this.objectNameSize.x) / 2,
            y - (32 - this.objectNameSize.y) / 2,
            this.objectNameSize.x,
            this.objectNameSize.y);

        GUI.Label(rect, this.objectPlayerData.Name, this.objectNameStyle);
    }

    protected override Vector2 getObjectOffset()
    {
        return PlayerObjectBuilder.offsets[this.objectPlayerData.Armour];
        //throw new NotImplementedException();
    }

    public void objectRun(ObjectRun objectRun)
    {
        var offset = PlayerObjectBuilder.offsets[this.objectPlayerData.Armour];
        var targetPosition = PlayerObjectBuilder.calcPosition(objectRun.Location, offset);
        this.gameObject.transform.DOMove(targetPosition, 0.6f)
        .SetUpdate(true)
        .SetEase(Ease.Linear);
        playAnimArmourHairWeapon(MirAction.Running,objectRun.Direction);
    }
    public void objectWalk(ObjectRun objectRun)
    {
        var offset = PlayerObjectBuilder.offsets[this.objectPlayerData.Armour];
        var targetPosition = PlayerObjectBuilder.calcPosition(objectRun.Location, offset);
        this.gameObject.transform.DOMove(targetPosition, 0.6f)
        .SetUpdate(true)
        .SetEase(Ease.Linear);
        playAnimArmourHairWeapon(MirAction.Walking,objectRun.Direction);
    }
}

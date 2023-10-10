using System.Collections;
using System.Collections.Generic;

using DG.Tweening;
using ServerPackets;
using UnityEngine;

public class MonsterController : MirBaseController
{

    public ObjectMonster objectMonster;
    private static MonsterObjectBuilder monsterObjectBuilder = new MonsterObjectBuilder();
    private Animator monsterAnimator;
    public override void onInit(System.Object data)
    {
        this.objectMonster = (ObjectMonster)data;
        var playerGameObject = monsterObjectBuilder.gameObject(this.objectMonster, this.transform);
        this.monsterAnimator = playerGameObject.GetComponent<Animator>();

        monsterAnimator.SetInteger(Mir_Direction, (int)objectMonster.Direction);
        monsterAnimator.SetInteger(Mir_Action, (int)MirAction.Standing);
        var stateName = MirAction.Standing.ToString() + "_" + objectMonster.Direction.ToString();
        monsterAnimator.Play(stateName, 0);
        // dealEffect();
    }

    protected override Vector2 getObjectOffset()
    {
        return MonsterObjectBuilder.monsterOffsets[(int)objectMonster.Image];
    }

    protected override void drawObjectName(float x, float y)
    {
        objectNameStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(x + (48 - objectNameSize.x) / 2,
              y - (32 - objectNameSize.y) / 2,
              objectNameSize.x,
              objectNameSize.y), objectMonster.Name, objectNameStyle);
    }

    protected override void calcNameSize()
    {
        if (objectNameStyle != null)
        {
            return;
        }
        objectNameStyle = new GUIStyle()
        {
            fontSize = 12
        };
        objectNameSize = objectNameStyle.CalcSize(new GUIContent(objectMonster.Name));
    }

    public void objectWalk(ObjectWalk objectWalk, MonsterObjectBuilder monsterObjectBuilder)
    {
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)objectWalk.Location.y + 1000;
        monsterAnimator.SetInteger(Mir_Direction, (int)objectWalk.Direction);
        monsterAnimator.SetInteger(Mir_Action, (int)MirAction.Standing);
        var targetPosition = MonsterObjectBuilder.calcPosition(objectWalk.Location, getObjectOffset());
        this.gameObject.transform.DOMove(targetPosition, 0.7f, true);
        var stateName = MirAction.Walking.ToString() + "_" + objectWalk.Direction.ToString();
        monsterAnimator.Play(stateName, 0);
    }
    public void objectRun(ObjectRun objectRun)
    {

    }
    public void objectAttack(ObjectAttack p)
    {
        monsterAnimator.SetInteger(Mir_Direction, (int)p.Direction);
        monsterAnimator.SetInteger(Mir_Action, (int)MirAction.Standing);
        var action = MirAction.Attack1;
        switch (p.Type)
        {
            case 1:
                action = MirAction.Attack2;
                break;
            case 2:
                action = MirAction.Attack3;
                break;
            case 3:
                action = MirAction.Attack4;
                break;
        }
        //animator.SetInteger(Mir_Direction, (int)objectMonster.Direction);
        //animator.SetInteger(Mir_Action, (int)action);
        //LogUtil.log("MonsterController", "" + action + " " + objectMonster.Name);
        var stateName = action.ToString() + "_" + p.Direction.ToString();
        monsterAnimator.Play(stateName, 0);
        // dealEffect();
    }

    public override void clipCallback(string input)
    {
        //animator.SetInteger(Mir_Direction, (int)objectMonster.Direction);
        //animator.SetInteger(Mir_Action, (int)MirAction.Standing);
        //var stateName = MirAction.Standing.ToString() + "_" + objectMonster.Direction.ToString();
        //animator.Play(stateName, 0);
    }



    private GameObject effect;



    private void dealEffect()
    {
        if (objectMonster.Image == Monster.Scarecrow && effect == null)
        {

            var go = Resources.Load<GameObject>("prefabs/npc");

            effect = Instantiate(go, this.gameObject.transform);

            effect.transform.localPosition = new Vector3(0, 0, 0);
            var animator = effect.GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("mir/Data/Monster/005/anim/005_effect");
            effect.GetComponent<SpriteRenderer>().material = Resources.Load<Material>("materials/blend_add");
            animator.SetInteger(Mir_Direction, 1);
            animator.SetInteger(Mir_Action, 1);
        }

    }
}

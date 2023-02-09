using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MirBaseController : MonoBehaviour
{
    protected static readonly string Mir_Direction = "MirDirection";
    protected static readonly string Mir_Action = "MirAction";

    //protected Animator animator;

    //protected List<Tuple<MirAction, MirDirection>> actions = new List<Tuple<MirAction, MirDirection>>();

    protected Vector2 objectNameSize;
    protected GUIStyle objectNameStyle;

    private void Awake()
    {
        // animator = GetComponent<Animator>();
        // AnimationClip[] clips = new AnimationClip[0];
        // if (animator != null)
        // {
        //     clips = animator?.runtimeAnimatorController?.animationClips;
        // }
        // if (clips == null || clips.Length == 0) return;

        // foreach (var clip in clips)
        // {
        //     var actionDirection = clip.name.Split('_');
        //     var mirAction = (MirAction)Enum.Parse(typeof(MirAction), actionDirection[0]);
        //     var mirDirection = (MirDirection)Enum.Parse(typeof(MirDirection), actionDirection[1]);
        //     this.actions.Add(Tuple.Create(mirAction, mirDirection));


        //     var animEvent = new AnimationEvent();
        //     animEvent.time = clip.length;
        //     animEvent.functionName = "clipCallback";
        //     animEvent.stringParameter = clip.name;

        //     clip.AddEvent(animEvent);
        // }
    }
    private void Start()
    {

    }

   public abstract void onInit(System.Object data);

    protected abstract Vector2 getObjectOffset();

    protected abstract void calcNameSize();

    protected abstract void drawObjectName( float x,float y);
    private void OnGUI()
    {
        this.calcNameSize();
        var offset  = getObjectOffset();
        var x = this.gameObject.transform.position.x - offset.x;
        var y = this.gameObject.transform.position.y + offset.y;

        var objectPosition = Camera.main.WorldToScreenPoint(new Vector3(x,y,0));
        drawObjectName(objectPosition.x,Screen.height - objectPosition.y);
    }

    public virtual void clipCallback(string input)
    {

    }
}
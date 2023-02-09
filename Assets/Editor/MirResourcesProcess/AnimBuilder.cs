using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using MLibraryUtils;


public static class AnimBuilder
{
    public static AnimationClip CreateOneFrameClip(string clipName, int interval, string[] imagePaths)
    {
        AnimationClip clip = new AnimationClip();
        clip.name = clipName;
        EditorCurveBinding curveBinding = new EditorCurveBinding
        {
            type = typeof(SpriteRenderer),
            path = "",
            propertyName = "m_Sprite"
        };
        ObjectReferenceKeyframe[] keyFrames = new ObjectReferenceKeyframe[imagePaths.Length + 1];
        //动画长度是按秒为单位，1/10就表示1秒切10张图片，根据项目的情况可以自己调节
        //动画间隔计算有问题
        float frameTime = interval / 1000f;
        for (int i = 0; i < imagePaths.Length; i++)
        {
            // String assetPath = imagePaths[i];
            // //var sprite0 = AssetDatabase.LoadAllAssetsAtPath(assetPath);
            // Texture2D sprite = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);

            // keyFrames[i] = new ObjectReferenceKeyframe();
            // keyFrames[i].time = frameTime * i;
            // keyFrames[i].value = sprite;

            String assetPath = imagePaths[i];
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);

            keyFrames[i] = new ObjectReferenceKeyframe();
            keyFrames[i].time = frameTime * i;
            keyFrames[i].value = sprite;
        }
        keyFrames[keyFrames.Length - 1] = new ObjectReferenceKeyframe()
        {
            time = frameTime * (keyFrames.Length - 1),
            value = keyFrames[keyFrames.Length - 2].value
        };
        //动画帧率，30比较合适
        clip.frameRate = 30;
        var clipSetting = AnimationUtility.GetAnimationClipSettings(clip);
        clipSetting.loopTime = true;
        clipSetting.loopBlend = true;
        clipSetting.cycleOffset = 0;
        clipSetting.orientationOffsetY = 0;

        AnimationUtility.SetAnimationClipSettings(clip, clipSetting);
        AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, keyFrames);
        return clip;
    }
    public static AnimatorController BuildAnimationController(List<Tuple<MirAction, MirDirection, AnimationClip>> clips, string controllerFileName)
    {
        AnimatorController animatorController = AnimatorController.CreateAnimatorControllerAtPath(controllerFileName);
        AnimatorControllerLayer layer = animatorController.layers[0];
        animatorController.AddParameter("MirAction", AnimatorControllerParameterType.Int);
        animatorController.AddParameter("MirDirection", AnimatorControllerParameterType.Int);
        AnimatorStateMachine stateMachine = layer.stateMachine;

        foreach (var tuple in clips)
        {
            var mirAction = tuple.Item1;
            var mirDirection = tuple.Item2;
            var newClip = tuple.Item3;
            AnimatorState state = stateMachine.AddState(newClip.name);
            state.motion = newClip;

            AnimatorStateTransition trans = stateMachine.AddAnyStateTransition(state);

            trans.hasExitTime = false;
            trans.hasFixedDuration = false;
            trans.duration = 0;
            trans.offset = 0;
            trans.canTransitionToSelf = false;
            trans.exitTime = 1;
            trans.AddCondition(AnimatorConditionMode.Equals, (float)mirAction, "MirAction");
            trans.AddCondition(AnimatorConditionMode.Equals, (float)mirDirection, "MirDirection");


            var transEntry = stateMachine.AddEntryTransition(state);

            transEntry.AddCondition(AnimatorConditionMode.Equals, (float)mirAction, "MirAction");
            transEntry.AddCondition(AnimatorConditionMode.Equals, (float)mirDirection, "MirDirection");
        }
        AssetDatabase.SaveAssets();
        return animatorController;
    }
}
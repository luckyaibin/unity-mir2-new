using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using MLibraryUtils;


public static class AnimBuilder
{
    public static AnimationClip CreateFrameClip(string clipName, int interval, string[] imagePaths )
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

}
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.Animations;
using MLibraryUtils;
using System;

public class MirResourcesProcess : EditorWindow
{
    //输出的目录 衣服,头发,武器,武器效果,受伤效果(巫师,战士,道士)
    const string resOutWizWarTaonDefault = "./Assets/Resources/mir/Data/NPC/";
    string resOutresOutWizWarTaon = "";


    public static string mirDataPath = "";
    public static string mirMapPath = "";

    [MenuItem("工具/处理mir2资源")]
    private static void ShowWindow()
    {
        var window = GetWindow<MirResourcesProcess>();
        window.titleContent = new GUIContent("处理资源");
        window.Show();
    }

    private void OnGUI()
    {
        // Data
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Data的资源路径", EditorStyles.boldLabel);
        mirDataPath = EditorGUILayout.TextField(mirDataPath);
        if (GUILayout.Button("浏览"))
        {
            EditorApplication.delayCall += () =>
            {
                mirDataPath = EditorUtility.OpenFolderPanel("请选择", mirDataPath, "");
            };
        }
        EditorGUILayout.EndHorizontal();

        // Map
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Map的资源路径", EditorStyles.boldLabel);
        mirMapPath = EditorGUILayout.TextField(mirMapPath);
        if (GUILayout.Button("浏览"))
        {
            EditorApplication.delayCall += () =>
            {
                mirMapPath = EditorUtility.OpenFolderPanel("请选择", mirMapPath, "");
            };
        }
        EditorGUILayout.EndHorizontal();

        //-----------------------------------------
        GUILayout.Label("--------------------------------------------------------------------------------------");
        GUILayout.Label("衣服,头发,武器,武器效果,受伤效果(巫师,战士,道士)");
        //衣服,头发,武器,武器效果,受伤效果(巫师,战士,道士)
        EditorGUILayout.BeginHorizontal();
        if (resOutresOutWizWarTaon == "")
        {
            resOutresOutWizWarTaon = resOutWizWarTaonDefault;
        }
        GUILayout.Label("默认输出路径", EditorStyles.boldLabel);
        resOutresOutWizWarTaon = EditorGUILayout.TextField(resOutresOutWizWarTaon);
        if (GUILayout.Button("浏览"))
        {
            EditorApplication.delayCall += () =>
            {
                resOutresOutWizWarTaon = EditorUtility.OpenFolderPanel("请选择", mirDataPath, "");
            };
        }
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("导出"))
        {
            EditorApplication.delayCall += exportWizWarTaonRes;
        }
        //-----------------------------------------

        if (GUILayout.Button("导出怪物动画资源"))
        {
            EditorApplication.delayCall += exportMonsterRes;
        }
        if (GUILayout.Button("导出NPC动画资源"))
        {
            EditorApplication.delayCall += exportNpcRes;
        }
        //导出地图资源(导出的不是某个.map文件的每个格子的图片，而是每个格子对应的 libIndex 和imageIndex 对应的 图片。以及动画)
        if (GUILayout.Button("导出某个地图所引用到资源"))
        {
            EditorApplication.delayCall += exportMapRes;
        }
    }

    void exportWizWarTaonRes()
    {
        // alignOffsets 所有lib的偏移
        var alignOffsets = new List<Vector2Int>();
        var libs = new Libraries();
        libs.InitAllLibraries();
        var count = 1;
        for (int i = 0; i < count; i++)
        {
            var armour = libs.CArmours[i];
            // XXX/01.Lib -> XXX/01/661.png
            var libPath = armour.getLibPath();
            // XXX/01
            var path = libPath.Replace(MLibrary.Extention, "");
            var dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            // TODO 导出这个库里的动画

            // 导出一个库，并返回这个库的偏移
            var offset = exportOneLib(armour, path);
            alignOffsets.Add(offset);
        }

        var first = libs.CArmours[0].getLibPath();
        var dir = new DirectoryInfo(first);
        var offsetPath = dir.Parent + "CArmours.info";
        //保存所有库对应的偏移数组
        saveOffsets(alignOffsets, offsetPath);
    }


    void exportMonsterRes()
    {
        var toStringValue = " s";
        var res = 1.ToString(toStringValue);
        System.Console.Write(res);
    }
    void exportNpcRes()
    {

    }
    void exportMapRes()
    {
        var libs = new Libraries();
        libs.InitAllLibraries();
    }
    Vector2Int exportOneLib(MLibrary library, string outDir)
    {
        library.Initialize();
        Vector2Int alignOffset = new Vector2Int(0, 0);
        // 找出所有图片的偏移
        for (int i = 0; i < library.getCount(); i++)
        {
            var image = library.getMImageInfo(i);
            if (image == null || image.Width == 0 || image.Height == 0)
            {
                continue;
            }
            if (image.X < alignOffset.x)
            {
                alignOffset.x = image.X;
            }
            if (image.Y < alignOffset.y)
            {
                alignOffset.y = image.Y;
            }
        }


        // 遍历图片，并按照偏移加载里面的texture，然后存储
        for (int imageIndex = 0; imageIndex < library.getCount(); imageIndex++)
        {
            var textureOKImage = library.LoadTextureWithOffsetXY(imageIndex, alignOffset.x, alignOffset.y);
            if (textureOKImage == null)
            {
                continue;
            }
            var bytes = textureOKImage.Image.EncodeToPNG();

            var pngFilename = outDir + "/" + imageIndex + ".png";
            var saveFile = File.Open(pngFilename, FileMode.OpenOrCreate);
            saveFile.Write(bytes, 0, bytes.Length);
            saveFile.Close();
        }
        //返回当前Lib的所有图片的偏移
        return alignOffset;
    }
    public const int NPC = 0;
    public const int MONSTER = 1;
    void buildMirAnimation(MLibrary library, int animType, string imagePath,string aniOutPath)
    {
        // 方向
        List<MirDirection> directions = new List<MirDirection>();
        if (animType == MONSTER)
        {
            for (var i = MirDirection.Up; i <= MirDirection.UpLeft; i++)
                directions.Add(i);
        }
        else if (animType == NPC)
        {
            for (var i = MirDirection.Up; i <= MirDirection.Right; i++)
                directions.Add(i);
        }
        //遍历Lib库里的所有帧动画
        var frameSets = library.FrameSets;
        var clips = new List<Tuple<MirAction, MirDirection, AnimationClip>>();
        foreach (var frameSet in frameSets)
        {
            var act = frameSet.Key;
            var f = frameSet.Value;
            var createdClips = createFrameClipAndSave(act, f, directions, imagePath);
            foreach (var t in createdClips)
            {
                var clip = t.Item3;
                AssetDatabase.CreateAsset(clip, aniOutPath + "/"+  clip.name + ".anim");
                AssetDatabase.SaveAssets();
            }
            clips.AddRange(createdClips);
        }
    }

    List<Tuple<MirAction, MirDirection, AnimationClip>> createFrameClipAndSave(MirAction action, Frame frame, List<MirDirection> directions, string imagePath)
    {
        // action(standing,walk ...),dir(up,upright ...),clip 的数组
        var actdirclips = new List<Tuple<MirAction, MirDirection, AnimationClip>>();
        int offset = frame.Count + frame.Skip;
        //遍历所有方向
        foreach (var dir in directions)
        {
            int startIndex = frame.Start + offset * (int)dir;
            var imagePaths = new string[frame.Count];
            for (int j = 0; j < frame.Count; j++)
            {
                imagePaths[j] = imagePath + "/" + (startIndex + j) + ".png";
            }
            var clipName = action + "_" + dir;
            var clip = AnimBuilder.CreateFrameClip(clipName, frame.Interval, imagePaths);
            actdirclips.Add(Tuple.Create(action, dir, clip));
            // //保存
            // AssetDatabase.CreateAsset(clip, imagePath + "/" + clipName + ".anim");
            // AssetDatabase.SaveAssets();
        }
        return actdirclips;
    }


    void saveOffsets(List<Vector2Int> offsets, string savePath)
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
        var file = File.Open(savePath, FileMode.CreateNew);
        BinaryWriter writer = new BinaryWriter(file);
        writer.Write(offsets.Count);
        foreach (var offset in offsets)
        {
            writer.Write(offset.x);
            writer.Write(offset.y);
        }
        writer.Flush();
        writer.Close();
        file.Close();
    }
}

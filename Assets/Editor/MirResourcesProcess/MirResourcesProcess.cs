using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.Animations;
using MLibraryUtils;
using System;

public class MirResourcesProcess : EditorWindow
{
    public string mirRootPathDefault = @"E:/exp/mir2-2022.06.12.00/Build/Client";
    public string mirRootPath = "";
    public string resOutRootPathDefault = "Assets/Resources/mir";
    //输出的目录 衣服,头发,武器,武器效果,受伤效果(巫师,战士,道士)

    [MenuItem("工具/处理mir2资源")]
    private static void ShowWindow()
    {
        var window = GetWindow<MirResourcesProcess>();
        window.titleContent = new GUIContent("处理资源");
        window.Show();
    }

    private void OnGUI()
    {
        Settings.InitSettings(mirRootPathDefault);
        // Data
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("资源根路径", EditorStyles.boldLabel);
        if (mirRootPath == "")
        {
            mirRootPath = Settings.resRootPath;
        }
        mirRootPath = EditorGUILayout.TextField(mirRootPath);
        if (GUILayout.Button("浏览"))
        {
            EditorApplication.delayCall += () =>
            {
                mirRootPath = EditorUtility.OpenFolderPanel("请选择", mirRootPath, "");
            };
        }
        EditorGUILayout.EndHorizontal();
        //-----------------------------------------
        GUILayout.Label("--------------------------------------------------------------------------------------");
        GUILayout.Label("衣服,头发,武器,武器效果,受伤效果(巫师,战士,道士)");
        //衣服,头发,武器,武器效果,受伤效果(巫师,战士,道士)
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("默认输出路径", EditorStyles.boldLabel);
        GUILayout.Label(resOutRootPathDefault);
        // if (GUILayout.Button("浏览"))
        // {
        //     EditorApplication.delayCall += () =>
        //     {
        //         resOutWizWarTaon = EditorUtility.OpenFolderPanel("请选择", mirDataPath, "");
        //     };
        // }
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("导出"))
        {
            EditorApplication.delayCall += () =>
            {
                exportWizWarTaonRes(this);
            };
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

    void exportWizWarTaonRes(MirResourcesProcess editorInstance)
    {
        Settings.InitSettings(editorInstance.mirRootPath);

        var animType = PLAYER;
        // alignOffsets 所有lib的偏移
        var alignOffsets = new List<Vector2Int>();
        var libs = new Libraries();
        libs.InitAllLibraries();
        var count = 1;

        for (int i = 0; i < count; i++)
        {
            var lib = libs.CArmours[i];
            var libPath = lib.getLibPath();
            var libName = Path.GetFileNameWithoutExtension(libPath);
            // E:/exp/mir2-2022.06.12.00/Build/Client/Data/XXX/01
            var path = libPath.Replace(MLibrary.Extention, "");
            // 替换成为 Asset下的相对路径
            // E:/exp/mir2-2022.06.12.00/Build/Client/Data/XXX/01 -> XXX/01
            var finalPath = path.Replace(Settings.resRootPath, resOutRootPathDefault);
            System.Console.WriteLine("write file {0} to {1}", libPath, finalPath);
            var dirInfo = new DirectoryInfo(finalPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            // 导出一个库，并返回这个库的偏移
            var offset = exportOneLibImages(lib, finalPath);
            alignOffsets.Add(offset);

            // TODO 导出这个库里的动画

            // 方向
            List<MirDirection> directions = new List<MirDirection>();
            if (animType == MONSTER || animType == PLAYER)
            {
                for (var ii = MirDirection.Up; ii <= MirDirection.UpLeft; ii++)
                    directions.Add(ii);
            }
            else if (animType == NPC)
            {
                for (var ii = MirDirection.Up; ii <= MirDirection.Right; ii++)
                    directions.Add(ii);
            }

            //遍历Lib库里的所有帧动画(玩家的帧动画不是保存在文件里的，是单独固定的)
            var frameSets = lib.FrameSets;
            if (animType == PLAYER)
            {
                frameSets = FrameSet.Player;
            }
            if (frameSets != null)
            {
                // 创建动画clip并保存
                var clips = buildMirAnimationAndSave(lib, frameSets, directions, finalPath, finalPath + "/ani");
                // 创建控制器
                var controllerFilename = finalPath + "/ani" + "/" + libName + ".controller";
                AnimBuilder.BuildAnimationController(clips, controllerFilename);
            }
        }
        var offsetPath = Settings.CArmourPath + "/" + "CArmours.info";
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
    Vector2Int exportOneLibImages(MLibrary library, string outDir)
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
            var pngFilename = outDir + "/" + imageIndex + ".png";
            if (new DirectoryInfo(pngFilename).Exists)
            {
                continue;
            }
            var textureOKImage = library.LoadTextureWithOffsetXY(imageIndex, alignOffset.x, alignOffset.y);
            if (textureOKImage == null)
            {
                continue;
            }
            var bytes = textureOKImage.Image.EncodeToPNG();


            var saveFile = File.Open(pngFilename, FileMode.OpenOrCreate);
            saveFile.Write(bytes, 0, bytes.Length);
            saveFile.Close();
        }
        //返回当前Lib的所有图片的偏移
        return alignOffset;
    }
    public const int NPC = 0;
    public const int MONSTER = 1;

    public const int PLAYER = 2;
    List<Tuple<MirAction, MirDirection, AnimationClip>> buildMirAnimationAndSave(MLibrary library, FrameSet frameSets, List<MirDirection> directions, string imagePath, string aniOutPath)
    {
        if (File.Exists(aniOutPath))
        {
            File.Delete(aniOutPath);
        }
        var dirInfo = new DirectoryInfo(aniOutPath);
        if (!dirInfo.Exists)
        {
            dirInfo.Create();
        }
        //aniOutPath = "Assets/Resources/mir/Data/CArmour/00/ani";
        var clips = new List<Tuple<MirAction, MirDirection, AnimationClip>>();
        foreach (var frameSet in frameSets) //遍历每个动作
        {
            var action = frameSet.Key;
            var frame = frameSet.Value;
            int offset = frame.Count + frame.Skip;
            //遍历每个方向
            foreach (var dir in directions)
            {
                int startIndex = frame.Start + offset * (int)dir;
                var imagePaths = new string[frame.Count];
                for (int j = 0; j < frame.Count; j++)
                {
                    imagePaths[j] = imagePath + "/" + (startIndex + j) + ".png";
                }
                var clipName = action + "_" + dir;
                var clip = AnimBuilder.CreateOneFrameClip(clipName, frame.Interval, imagePaths);
                clips.Add(Tuple.Create(action, dir, clip));
                // //保存
                AssetDatabase.CreateAsset(clip, imagePath + "/" + clipName + ".anim");
                AssetDatabase.SaveAssets();
            }
        }
        return clips;
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

using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.Animations;
using MLibraryUtils;
using System;

public class MirResourcesProcess : EditorWindow
{
    public static string mirRootPathDefault = @"E:/exp/mir2-2022.06.12.00/Build/Client";
    public string mirRootPath = mirRootPathDefault;
    public string resOutRootPathDefault = "Assets/Resources/mir/";
    public string guiLibName = "";
    //输出的目录 衣服,头发,武器,武器效果,受伤效果(巫师,战士,道士)
    static Libraries allLibs;

    [MenuItem("工具/处理mir2资源")]
    private static void ShowWindow()
    {
        var window = GetWindow<MirResourcesProcess>();
        window.titleContent = new GUIContent("处理资源");
        window.Show();
        Settings.InitSettings(mirRootPathDefault);
        allLibs = new Libraries();
        allLibs.InitAllLibraries();
    }
    private static void HideWindow()
    {
        System.Console.Write("sss");
    }
    private void OnGUI()
    {
        // Data
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("资源根路径", EditorStyles.boldLabel);
        if (mirRootPath == "")
        {
            mirRootPath = mirRootPathDefault;
            //路径变了，需要重新生成资源
            Settings.InitSettings(mirRootPathDefault);
            allLibs = new Libraries();
            allLibs.InitAllLibraries();
        }
        mirRootPath = EditorGUILayout.TextField(mirRootPath);
        if (GUILayout.Button("浏览"))
        {
            EditorApplication.delayCall += () =>
            {
                mirRootPath = EditorUtility.OpenFolderPanel("请选择", mirRootPath, "");
                Settings.InitSettings(mirRootPathDefault);
                allLibs = new Libraries();
                allLibs.InitAllLibraries();
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
        if (GUILayout.Button("导出衣服"))
        {
            EditorApplication.delayCall += () =>
            {
                Settings.InitSettings(mirRootPath);
                //var offsetPath = Settings.CArmourPath + "/" + "CArmour.info";
                var dst = Settings.CArmourPath.Replace(mirRootPath, resOutRootPathDefault);
                var offsetPath = dst + "CArmour.info";
                exportWizWarTaonAnimations(allLibs.CArmours, 1, PLAYER, offsetPath);
            };
        }
        if (GUILayout.Button("导出头发"))
        {
            EditorApplication.delayCall += () =>
            {
                var dst = Settings.CHairPath.Replace(mirRootPath, resOutRootPathDefault);
                var offsetPath = dst + "CHair.info";
                exportWizWarTaonAnimations(allLibs.CHair, 1, PLAYER, offsetPath);
            };
        }
        if (GUILayout.Button("导出武器"))
        {
            EditorApplication.delayCall += () =>
            {
                var dst = Settings.CWeaponPath.Replace(mirRootPath, resOutRootPathDefault);
                var offsetPath = dst + "CWeapon.info";
                exportWizWarTaonAnimations(allLibs.CWeapons, 1, PLAYER, offsetPath);
            };
        }
        if (GUILayout.Button("导出武器效果"))
        {
            EditorApplication.delayCall += () =>
            {
                var dst = Settings.CWeaponEffectPath.Replace(mirRootPath, resOutRootPathDefault);
                var offsetPath = dst + "CWeaponEffect.info";
                exportWizWarTaonAnimations(allLibs.CWeaponEffect, 1, PLAYER, offsetPath);
            };
        }
        if (GUILayout.Button("导出受伤效果"))
        {
            EditorApplication.delayCall += () =>
            {
                var dst = Settings.CHumEffectPath.Replace(mirRootPath, resOutRootPathDefault);
                var offsetPath = dst + "CHumEffect.info";
                exportWizWarTaonAnimations(allLibs.CHumEffect, 1, PLAYER, offsetPath);
            };
        }
        //-----------------------------------------
        GUILayout.Label("--------------------------------------------------------------------------------------");
        if (GUILayout.Button("导出怪物动画资源"))
        {
            EditorApplication.delayCall += () =>
            {
                var dst = Settings.MonsterPath.Replace(mirRootPath, resOutRootPathDefault);
                var offsetPath = dst + "CMonster.info";
                exportWizWarTaonAnimations(allLibs.Monsters, 2, MONSTER, offsetPath);
            };
        }
        if (GUILayout.Button("导出NPC动画资源"))
        {
            EditorApplication.delayCall += () =>
            {
                var dst = Settings.NPCPath.Replace(mirRootPath, resOutRootPathDefault);
                var offsetPath = dst + "CNPC.info";
                exportWizWarTaonAnimations(allLibs.NPCs, 2, NPC, offsetPath);
            };
        }
        GUILayout.Label("--------------------------------------------------------------------------------------");
        GUILayout.BeginHorizontal();
        GUILayout.Label("UI图片名称:");
        guiLibName = GUILayout.TextField(guiLibName);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("导出UI图片(ChrSel,Prguse等)"))
        {
            EditorApplication.delayCall += () =>
            {
                switch (guiLibName)
                {
                    case "ChrSel":
                        var dst = (mirRootPath + "/Data/ChrSel").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.ChrSel, dst, false);
                        break;
                    case "Prguse":
                        dst = (mirRootPath + "/Data/Prguse").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.Prguse, dst, false);
                        break;
                    case "Prguse2":
                        dst = (mirRootPath + "/Data/Prguse2").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.Prguse2, dst, false);
                        break;
                    case "Prguse3":
                        dst = (mirRootPath + "/Data/Prguse3").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.Prguse3, dst, false);
                        break;


                    case "BuffIcon":
                        dst = (mirRootPath + "/Data/BuffIcon").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.BuffIcon, dst, false);
                        break;
                    case "Help":
                        dst = (mirRootPath + "/Data/Help").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.Help, dst, false);
                        break;
                    case "MiniMap":
                        dst = (mirRootPath + "/Data/MiniMap").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.MiniMap, dst, false);
                        break;
                     case "Title":
                        dst = (mirRootPath + "/Data/Title").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.Title, dst, false);
                        break;
                    case "MagIcon":
                        dst = (mirRootPath + "/Data/MagIcon").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.MagIcon, dst, false);
                        break;
                    case "MagIcon2":
                        dst = (mirRootPath + "/Data/MagIcon2").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.MagIcon2, dst, false);
                        break;
                    case "Magic":// 应该导出 magic animation,not ui.
                        dst = (mirRootPath + "/Data/Magic").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.Magic, dst, false);
                        break;
                    case "Effect":
                        dst = (mirRootPath + "/Data/Effect").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.Effect, dst, false);
                        break;
                    case "MagicC":
                        dst = (mirRootPath + "/Data/MagicC").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.MagicC, dst, false);
                        break;
                    case "GuildSkill":
                        dst = (mirRootPath + "/Data/GuildSkill").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.GuildSkill, dst, false);
                        break;
                    case "Background":
                        dst = (mirRootPath + "/Data/Background").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.Background, dst, false);
                        break;
                    case "Dragon":
                        dst = (mirRootPath + "/Data/Dragon").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.Dragon, dst, false);
                        break;
                    case "Items":
                        dst = (mirRootPath + "/Data/Items").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.Items, dst, false);
                        break;
                    case "StateItems":
                        dst = (mirRootPath + "/Data/StateItems").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.StateItems, dst, false);
                        break;
                    case "FloorItems":
                        dst = (mirRootPath + "/Data/FloorItems").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.FloorItems, dst, false);
                        break;
                    case "Deco":
                        dst = (mirRootPath + "/Data/Deco").Replace(mirRootPath, resOutRootPathDefault);
                        exportOneLibImages(allLibs.Deco, dst, false);
                        break;
                    default:
                        Debug.Log("invalid resource" + guiLibName);
                        break;
                }

            };
        }
        GUILayout.Label("--------------------------------------------------------------------------------------");
        //导出地图资源(导出的不是某个.map文件的每个格子的图片，而是每个格子对应的 libIndex 和imageIndex 对应的 图片。以及动画)
        if (GUILayout.Button("导出某个地图所引用到资源"))
        {
            EditorApplication.delayCall += exportMapRes;
        }
    }

    void exportWizWarTaonAnimations(MLibrary[] libs, int count, int animType, string offsetPath)
    {
        var alignOffsets = new List<Vector2Int>();
        for (int i = 0; i < count && i < libs.Length; i++)
        {
            var lib = libs[i];
            var libPath = lib.getLibPath();
            var libName = Path.GetFileNameWithoutExtension(libPath);
            // E:/exp/mir2-2022.06.12.00/Build/Client/Data/XXX/01
            var path = libPath.Replace(MLibrary.Extention, "");
            // 替换成为 Asset下的相对路径
            // E:/exp/mir2-2022.06.12.00/Build/Client/Data/XXX/01 -> XXX/01
            var dst = Settings.resRootPath;
            var finalPath = path.Replace(dst, resOutRootPathDefault);
            System.Console.WriteLine("write file {0} to {1}", libPath, finalPath);
            var dirInfo = new DirectoryInfo(finalPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            // 导出一个库，并返回这个库的偏移
            var offset = exportOneLibImages(lib, finalPath);
            alignOffsets.Add(offset);
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
                var clips = buildMirAnimationAndSave(lib, frameSets, directions, finalPath, finalPath + "/anim");
                // 创建控制器
                var controllerFilename = finalPath + "/anim" + "/" + libName + ".controller";
                AnimBuilder.BuildAnimationController(clips, controllerFilename);
            }
        }
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

    // 导出一个库，并返回这个库的偏移
    Vector2Int exportOneLibImages(MLibrary library, string outDir, bool withOffset = true)
    {
        var dirInfo = new DirectoryInfo(outDir);
        if (!dirInfo.Exists)
        {
            dirInfo.Create();
        }
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
            MImage textureOKImage = null;
            if (withOffset)
            {
                //带偏移的话是把所有图片按照左下角对齐，方便生成帧动画
                textureOKImage = library.LoadTextureWithOffsetXY(imageIndex, alignOffset.x, alignOffset.y);
            }
            else
            {
                //不带偏移，直接就是ui的图片
                textureOKImage = library.LoadTextureNoOffsetXY(imageIndex);
            }
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
        //aniOutPath = "Assets/Resources/mir/Data/CArmour/00/anim";
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
                AssetDatabase.CreateAsset(clip, aniOutPath + "/" + clipName + ".anim");
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


using System.IO;
using UnityEngine;

namespace MLibraryUtils
{
    public class Settings
    {
        //  private static bool initialized = false;
        public static string resRootPath = @"E:\exp\mir2-2022.06.12.00\Build\Client";
        public static string DataPath,
                            MapPath,
                            SoundPath,
                            ExtraDataPath,
                            ShadersPath,
                            MonsterPath,
                            GatePath,
                            FlagPath,
                            NPCPath,
                            CArmourPath,
                            CWeaponPath,
                            CWeaponEffectPath,
                            CHairPath,
                            AArmourPath,
                            AWeaponPath,
                            AHairPath,
                            ARArmourPath,
                            ARWeaponPath,
                            ARHairPath,
                            CHumEffectPath,
                            AHumEffectPath,
                            ARHumEffectPath,
                            MountPath,
                            FishingPath,
                            PetsPath,
                            TransformPath,
                            TransformMountsPath,
                            TransformEffectPath,
                            TransformWeaponEffectPath;
        internal static readonly string IPAddress = "127.0.0.1";
        internal static readonly int Port = 7000;
        internal static readonly int TimeOut = 5000;

        public static void InitSettings(string resRootPath)
        {
            var lst = resRootPath[resRootPath.Length - 1];
            if (lst == '/')
            {
                resRootPath = resRootPath.Substring(0, resRootPath.Length - 1);
            }
            resRootPath += "/";
            Settings.resRootPath = resRootPath;
            DataPath = Path.Combine(resRootPath, "Data/");
            Debug.Log(resRootPath);
            Debug.Log(DataPath);
            MapPath = Path.Combine(resRootPath + "Map/");
            SoundPath = Path.Combine(resRootPath + "Sound/");
            ExtraDataPath = Path.Combine(resRootPath + "Data/Extra/");
            ShadersPath = Path.Combine(resRootPath + "Data/Shaders/");
            MonsterPath = Path.Combine(resRootPath + "Data/Monster/");
            GatePath = Path.Combine(resRootPath + "Data/Gate/");
            FlagPath = Path.Combine(resRootPath + "Data/Flag/");
            NPCPath = Path.Combine(resRootPath + "Data/NPC/");
            CArmourPath = Path.Combine(resRootPath + "Data/CArmour/");
            CWeaponPath = Path.Combine(resRootPath + "Data/CWeapon/");
            CWeaponEffectPath = Path.Combine(resRootPath + "Data/CWeaponEffect/");
            CHairPath = Path.Combine(resRootPath + "Data/CHair/");
            AArmourPath = Path.Combine(resRootPath + "Data/AArmour/");
            AWeaponPath = Path.Combine(resRootPath + "Data/AWeapon/");
            AHairPath = Path.Combine(resRootPath + "Data/AHair/");
            ARArmourPath = Path.Combine(resRootPath + "Data/ARArmour/");
            ARWeaponPath = Path.Combine(resRootPath + "Data/ARWeapon/");
            ARHairPath = Path.Combine(resRootPath + "Data/ARHair/");
            CHumEffectPath = Path.Combine(resRootPath + "Data/CHumEffect/");
            AHumEffectPath = Path.Combine(resRootPath + "Data/AHumEffect/");
            ARHumEffectPath = Path.Combine(resRootPath + "Data/ARHumEffect/");
            MountPath = Path.Combine(resRootPath + "Data/Mount/");
            FishingPath = Path.Combine(resRootPath + "Data/Fishing/");
            PetsPath = Path.Combine(resRootPath + "Data/Pet/");
            TransformPath = Path.Combine(resRootPath + "Data/Transform/");
            TransformMountsPath = Path.Combine(resRootPath + "Data/TransformRide2/");
            TransformEffectPath = Path.Combine(resRootPath + "Data/TransformEffect/");
            TransformWeaponEffectPath = Path.Combine(resRootPath + "Data/TransformWeaponEffect/");
        }
    }
}
using System;
using System.Collections.Generic;
using MLibraryUtils;
using UnityEngine;



public class MapResInfoLoader
{

    private static MapResInfoLoader _instance = null;
    private static readonly object _syslock = new object();
    private MapResInfoLoader()
    {
    }

    public static MapResInfoLoader GetInstance()
    {
        // 最开始判断不存在的时候，该类从来未被实例化过 //
        if (_instance == null)
        {
            // 锁定状态，继续搜索是否存在该类的实例 //
            lock (_syslock)
            {
                // 如果不存在，在锁定状态下实例化出一个实例 //
                if (_instance == null)
                {
                    _instance = new MapResInfoLoader();

                }
            }
        }
        // 该实例本身就已经存在了，直接返回 //
        return _instance;
    }

    private Dictionary<String, MInfoLibrary> mlibs = new Dictionary<String, MInfoLibrary>();
    public MImage GetMImageInfo(string libName, int imageIndex)
    {
        lock (_syslock)
        {
            if (!mlibs.ContainsKey(libName))
            {
                mlibs.Add(libName, new MInfoLibrary(libName + ".info"));
            }
            var info = mlibs[libName].getMImageInfo(imageIndex);
            info.imagePath = libName + "/" + imageIndex;
            info.imagePath = info.imagePath.Replace(MapConfigs.MIR_RES_PATH, "");
            return info;
        }
    }
}


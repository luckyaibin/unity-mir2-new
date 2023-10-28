using System;
using System.IO;
using System.IO.Compression;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MLibraryUtils
{
    public class Libraries
    {
        public static bool Loaded;
        public static int Count, Progress;

        //单独的Lib
        public MLibrary
            ChrSel,
            Prguse,
            Prguse2,
            Prguse3,
            BuffIcon,
            Help,
            MiniMap,
            Title,
            MagIcon,
            MagIcon2,
            Magic,
            Magic2,
            Magic3,
            Effect,
            MagicC,
            GuildSkill,
            Background,
            Dragon,
            //Items
            Items,
            StateItems,
            FloorItems,

            //Deco
            Deco;

        // 多个的lib数组
        public MLibrary[]
            CArmours,
            CWeapons,
            CWeaponEffect,
            CHair,
            CHumEffect,
            AArmours,
            AWeaponsL,
            AWeaponsR,
            AHair,
            AHumEffect,
            ARArmours,
            ARWeapons,
            ARWeaponsS,
            ARHair,
            ARHumEffect,
            Monsters,
            Gates,
            Flags,
            Mounts,
            NPCs,
            Fishing,
            Pets,
            Transform,
            TransformMounts,
            TransformEffect,
            TransformWeaponEffect;
        //Map
        public readonly MLibrary[] MapLibs = new MLibrary[400];

        public void InitAllLibraries()
        {
            //=================基础的UI库========================
            ChrSel = new MLibrary(Settings.DataPath + "ChrSel");
            Prguse = new MLibrary(Settings.DataPath + "Prguse");
            Prguse2 = new MLibrary(Settings.DataPath + "Prguse2");
            Prguse3 = new MLibrary(Settings.DataPath + "Prguse3");
            BuffIcon = new MLibrary(Settings.DataPath + "BuffIcon");
            Help = new MLibrary(Settings.DataPath + "Help");
            MiniMap = new MLibrary(Settings.DataPath + "MMap");
            Title = new MLibrary(Settings.DataPath + "Title");
            MagIcon = new MLibrary(Settings.DataPath + "MagIcon");
            MagIcon2 = new MLibrary(Settings.DataPath + "MagIcon2");
            Magic = new MLibrary(Settings.DataPath + "Magic");
            Magic2 = new MLibrary(Settings.DataPath + "Magic2");
            Magic3 = new MLibrary(Settings.DataPath + "Magic3");
            Effect = new MLibrary(Settings.DataPath + "Effect");
            MagicC = new MLibrary(Settings.DataPath + "MagicC");
            GuildSkill = new MLibrary(Settings.DataPath + "GuildSkill");
            Background = new MLibrary(Settings.DataPath + "Background");
            Dragon = new MLibrary(Settings.DataPath + "Dragon");
            //Items
            Items = new MLibrary(Settings.DataPath + "Items");
            StateItems = new MLibrary(Settings.DataPath + "StateItem");
            FloorItems = new MLibrary(Settings.DataPath + "DNItems");

            //Deco
            Deco = new MLibrary(Settings.DataPath + "Deco");

            //====================其他动画库=================

            //Wiz/War/Taon 巫师，战士，道士
            CreateLibraryList(ref CArmours, Settings.CArmourPath, "00");
            CreateLibraryList(ref CHair, Settings.CHairPath, "00");
            CreateLibraryList(ref CWeapons, Settings.CWeaponPath, "00");
            CreateLibraryList(ref CWeaponEffect, Settings.CWeaponEffectPath, "00");
            CreateLibraryList(ref CHumEffect, Settings.CHumEffectPath, "00");
            //Assassin 刺客
            CreateLibraryList(ref AArmours, Settings.AArmourPath, "00");
            CreateLibraryList(ref AHair, Settings.AHairPath, "00");
            CreateLibraryList(ref AWeaponsL, Settings.AWeaponPath, "00", " L");
            CreateLibraryList(ref AWeaponsR, Settings.AWeaponPath, "00", " R");
            CreateLibraryList(ref AHumEffect, Settings.AHumEffectPath, "00");
            //Archer 弓箭手
            CreateLibraryList(ref ARArmours, Settings.ARArmourPath, "00");
            CreateLibraryList(ref ARHair, Settings.ARHairPath, "00");
            CreateLibraryList(ref ARWeapons, Settings.ARWeaponPath, "00");
            CreateLibraryList(ref ARWeaponsS, Settings.ARWeaponPath, "00", " S");
            CreateLibraryList(ref ARHumEffect, Settings.ARHumEffectPath, "00");
            //Other 其他
            CreateLibraryList(ref Monsters, Settings.MonsterPath, "000");
            CreateLibraryList(ref Gates, Settings.GatePath, "00");
            CreateLibraryList(ref Flags, Settings.FlagPath, "00");
            CreateLibraryList(ref NPCs, Settings.NPCPath, "00");
            CreateLibraryList(ref Mounts, Settings.MountPath, "00");
            CreateLibraryList(ref Fishing, Settings.FishingPath, "00");
            CreateLibraryList(ref Pets, Settings.PetsPath, "00");
            CreateLibraryList(ref Transform, Settings.TransformPath, "00");
            CreateLibraryList(ref TransformMounts, Settings.TransformMountsPath, "00");
            CreateLibraryList(ref TransformEffect, Settings.TransformEffectPath, "00");
            CreateLibraryList(ref TransformWeaponEffect, Settings.TransformWeaponEffectPath, "00");

            #region Maplibs
            //wemade mir2 (allowed from 0-99)
            MapLibs[0] = new MLibrary(Settings.DataPath + "Map/WemadeMir2/Tiles");
            MapLibs[1] = new MLibrary(Settings.DataPath + "Map/WemadeMir2/Smtiles");
            MapLibs[2] = new MLibrary(Settings.DataPath + "Map/WemadeMir2/Objects");
            for (int i = 2; i < 27; i++)
            {
                MapLibs[i + 1] = new MLibrary(Settings.DataPath + "Map/WemadeMir2/Objects" + i.ToString());
            }
            //shanda mir2 (allowed from 100-199)
            MapLibs[100] = new MLibrary(Settings.DataPath + "Map/ShandaMir2/Tiles");
            for (int i = 1; i < 10; i++)
            {
                MapLibs[100 + i] = new MLibrary(Settings.DataPath + "Map/ShandaMir2/Tiles" + (i + 1));
            }
            MapLibs[110] = new MLibrary(Settings.DataPath + "Map/ShandaMir2/SmTiles");
            for (int i = 1; i < 10; i++)
            {
                MapLibs[110 + i] = new MLibrary(Settings.DataPath + "Map/ShandaMir2/SmTiles" + (i + 1));
            }
            MapLibs[120] = new MLibrary(Settings.DataPath + "Map/ShandaMir2/Objects");
            for (int i = 1; i < 31; i++)
            {
                MapLibs[120 + i] = new MLibrary(Settings.DataPath + "Map/ShandaMir2/Objects" + (i + 1));
            }
            MapLibs[190] = new MLibrary(Settings.DataPath + "Map/ShandaMir2/AniTiles1");
            //wemade mir3 (allowed from 200-299)
            string[] Mapstate = { "", "wood/", "sand/", "snow/", "forest/" };
            for (int i = 0; i < Mapstate.Length; i++)
            {
                MapLibs[200 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/WemadeMir3/" + Mapstate[i] + "Tilesc");
                MapLibs[201 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/WemadeMir3/" + Mapstate[i] + "Tiles30c");
                MapLibs[202 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/WemadeMir3/" + Mapstate[i] + "Tiles5c");
                MapLibs[203 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/WemadeMir3/" + Mapstate[i] + "Smtilesc");
                MapLibs[204 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/WemadeMir3/" + Mapstate[i] + "Housesc");
                MapLibs[205 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/WemadeMir3/" + Mapstate[i] + "Cliffsc");
                MapLibs[206 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/WemadeMir3/" + Mapstate[i] + "Dungeonsc");
                MapLibs[207 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/WemadeMir3/" + Mapstate[i] + "Innersc");
                MapLibs[208 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/WemadeMir3/" + Mapstate[i] + "Furnituresc");
                MapLibs[209 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/WemadeMir3/" + Mapstate[i] + "Wallsc");
                MapLibs[210 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/WemadeMir3/" + Mapstate[i] + "smObjectsc");
                MapLibs[211 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/WemadeMir3/" + Mapstate[i] + "Animationsc");
                MapLibs[212 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/WemadeMir3/" + Mapstate[i] + "Object1c");
                MapLibs[213 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/WemadeMir3/" + Mapstate[i] + "Object2c");
            }
            Mapstate = new string[] { "", "wood", "sand", "snow", "forest" };
            //shanda mir3 (allowed from 300-399)
            for (int i = 0; i < Mapstate.Length; i++)
            {
                MapLibs[300 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/ShandaMir3/" + "Tilesc" + Mapstate[i]);
                MapLibs[301 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/ShandaMir3/" + "Tiles30c" + Mapstate[i]);
                MapLibs[302 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/ShandaMir3/" + "Tiles5c" + Mapstate[i]);
                MapLibs[303 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/ShandaMir3/" + "Smtilesc" + Mapstate[i]);
                MapLibs[304 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/ShandaMir3/" + "Housesc" + Mapstate[i]);
                MapLibs[305 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/ShandaMir3/" + "Cliffsc" + Mapstate[i]);
                MapLibs[306 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/ShandaMir3/" + "Dungeonsc" + Mapstate[i]);
                MapLibs[307 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/ShandaMir3/" + "Innersc" + Mapstate[i]);
                MapLibs[308 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/ShandaMir3/" + "Furnituresc" + Mapstate[i]);
                MapLibs[309 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/ShandaMir3/" + "Wallsc" + Mapstate[i]);
                MapLibs[310 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/ShandaMir3/" + "smObjectsc" + Mapstate[i]);
                MapLibs[311 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/ShandaMir3/" + "Animationsc" + Mapstate[i]);
                MapLibs[312 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/ShandaMir3/" + "Object1c" + Mapstate[i]);
                MapLibs[313 + (i * 15)] = new MLibrary(Settings.DataPath + "Map/ShandaMir3/" + "Object2c" + Mapstate[i]);
            }
            #endregion
            this.LoadUILibraries();
            this.LoadGameLibraries(); //crystal mir2中是用单独线程加载的
        }
        public void DeinitAllLibraries()
        {
            foreach (var lib in MapLibs)
            {
                if (lib != null)
                {
                    lib.close();
                }
            }
        }
        // 先叫这个名字，不确定都是干什么的Lib
        private void LoadUILibraries()
        {
            ChrSel.Initialize();
            Progress++;

            Prguse.Initialize();
            Progress++;

            Prguse2.Initialize();
            Progress++;

            Prguse3.Initialize();
            Progress++;

            Title.Initialize();
            Progress++;
        }

        private void LoadGameLibraries()
        {
            Count = MapLibs.Length + Monsters.Length + Gates.Length + NPCs.Length + CArmours.Length +
                CHair.Length + CWeapons.Length + CWeaponEffect.Length + AArmours.Length + AHair.Length + AWeaponsL.Length + AWeaponsR.Length +
                ARArmours.Length + ARHair.Length + ARWeapons.Length + ARWeaponsS.Length +
                CHumEffect.Length + AHumEffect.Length + ARHumEffect.Length + Mounts.Length + Fishing.Length + Pets.Length +
                Transform.Length + TransformMounts.Length + TransformEffect.Length + TransformWeaponEffect.Length + 17;

            Dragon.Initialize();
            Progress++;

            BuffIcon.Initialize();
            Progress++;

            Help.Initialize();
            Progress++;

            MiniMap.Initialize();
            Progress++;

            MagIcon.Initialize();
            Progress++;
            MagIcon2.Initialize();
            Progress++;

            Magic.Initialize();
            Progress++;
            Magic2.Initialize();
            Progress++;
            Magic3.Initialize();
            Progress++;
            MagicC.Initialize();
            Progress++;

            Effect.Initialize();
            Progress++;

            GuildSkill.Initialize();
            Progress++;

            Background.Initialize();
            Progress++;

            Deco.Initialize();
            Progress++;

            Items.Initialize();
            Progress++;
            StateItems.Initialize();
            Progress++;
            FloorItems.Initialize();
            Progress++;

            for (int i = 0; i < MapLibs.Length; i++)
            {
                if (MapLibs[i] == null)
                    MapLibs[i] = new MLibrary("");
                else
                    MapLibs[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < Monsters.Length; i++)
            {
                Monsters[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < Gates.Length; i++)
            {
                Gates[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < NPCs.Length; i++)
            {
                NPCs[i].Initialize();
                Progress++;
            }


            for (int i = 0; i < CArmours.Length; i++)
            {
                CArmours[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < CHair.Length; i++)
            {
                CHair[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < CWeapons.Length; i++)
            {
                CWeapons[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < CWeaponEffect.Length; i++)
            {
                CWeaponEffect[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < AArmours.Length; i++)
            {
                AArmours[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < AHair.Length; i++)
            {
                AHair[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < AWeaponsL.Length; i++)
            {
                AWeaponsL[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < AWeaponsR.Length; i++)
            {
                AWeaponsR[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < ARArmours.Length; i++)
            {
                ARArmours[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < ARHair.Length; i++)
            {
                ARHair[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < ARWeapons.Length; i++)
            {
                ARWeapons[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < ARWeaponsS.Length; i++)
            {
                ARWeaponsS[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < CHumEffect.Length; i++)
            {
                CHumEffect[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < AHumEffect.Length; i++)
            {
                AHumEffect[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < ARHumEffect.Length; i++)
            {
                ARHumEffect[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < Mounts.Length; i++)
            {
                Mounts[i].Initialize();
                Progress++;
            }


            for (int i = 0; i < Fishing.Length; i++)
            {
                Fishing[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < Pets.Length; i++)
            {
                Pets[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < Transform.Length; i++)
            {
                Transform[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < TransformEffect.Length; i++)
            {
                TransformEffect[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < TransformWeaponEffect.Length; i++)
            {
                TransformWeaponEffect[i].Initialize();
                Progress++;
            }

            for (int i = 0; i < TransformMounts.Length; i++)
            {
                TransformMounts[i].Initialize();
                Progress++;
            }
            Loaded = true;
        }

        public void close()
        {
            foreach (var lib in MapLibs)
            {
                if (lib != null)
                {
                    lib.close();
                }
            }
        }

        private void CreateLibraryList(ref MLibrary[] library, string path, string toStringValue, string suffix = "")
        {
            var allFiles = Directory.GetFiles(path, "*" + suffix + MLibrary.Extention, SearchOption.TopDirectoryOnly);
            //Array.Sort(allFiles, new FileNameComparer());
            var lastFile = allFiles.Length > 0 ? Path.GetFileName(allFiles[allFiles.Length - 1]) : "0";
            var count = int.Parse(Regex.Match(lastFile, @"\d+").Value) + 1;
            library = new MLibrary[count];
            for (int i = 0; i < count; i++)
            {
                library[i] = new MLibrary(path + i.ToString(toStringValue) + suffix);
            }
        }
    }

    public sealed class MLibrary
    {
        public MLibrary(string filename)
        {
            _fileName = Path.ChangeExtension(filename, Extention);
        }
        public const string Extention = ".Lib";
        public const int LibVersion = 3;

        private string _fileName;

        private MImage[] _images;
        private FrameSet _frames;
        private int[] _indexList;
        private int _count;
        private bool _initialized;

        private BinaryReader _reader;
        private FileStream _fStream;

        public FrameSet FrameSets
        {
            get { return _frames; }
        }
        public void Initialize()
        {
            _initialized = true;
            if (!File.Exists(_fileName))
                return;
            try
            {
                _fStream = new FileStream(_fileName, FileMode.Open, FileAccess.Read);
                _reader = new BinaryReader(_fStream);
                int currentVersion = _reader.ReadInt32();
                if (currentVersion < 2)
                {
                    return;
                }
                _count = _reader.ReadInt32();

                int frameSeek = 0;
                if (currentVersion >= 3)
                {
                    frameSeek = _reader.ReadInt32();
                }

                _images = new MImage[_count];
                _indexList = new int[_count];

                for (int i = 0; i < _count; i++)
                    _indexList[i] = _reader.ReadInt32();

                if (currentVersion >= 3)
                {
                    _fStream.Seek(frameSeek, SeekOrigin.Begin);

                    var frameCount = _reader.ReadInt32();

                    if (frameCount > 0)
                    {
                        _frames = new FrameSet();
                        for (int i = 0; i < frameCount; i++)
                        {
                            _frames.Add((MirAction)_reader.ReadByte(), new Frame(_reader));
                        }
                    }
                }
            }
            catch (Exception)
            {
                _initialized = false;
                throw;
            }
        }

        public Vector2Int GetSize(int index)
        {
            MImage mImage = getMImageInfo(index);
            if (mImage != null)
            {
                return new Vector2Int(mImage.Width, mImage.Height);
            }
            return Vector2Int.zero;
        }

        // 只加载图片的信息，不加载图片的 texture
        public MImage getMImageInfo(int index)
        {
            if (!_initialized) Initialize();
            if (_images == null || index < 0 || index >= _images.Length)
                return null;

            if (_images[index] == null)
            {
                _fStream.Seek(_indexList[index], SeekOrigin.Begin);
                _images[index] = new MImage(_reader);
            }

            return _images[index];
        }

        //一般是绘制之前调用，加载texture
        public MImage CheckImage(int index)
        {
            if (!_initialized)
                Initialize();

            if (_images == null || index < 0 || index >= _images.Length)
                return null;

            if (_images[index] == null)
            {
                _fStream.Position = _indexList[index];
                _fStream.Seek(_indexList[index], SeekOrigin.Begin);
                _images[index] = new MImage(_reader);
            }
            MImage mi = _images[index];
            if (!mi.TextureValid)
            {
                if ((mi.Width == 0) || (mi.Height == 0))
                    return null;
                _fStream.Seek(_indexList[index] + 17, SeekOrigin.Begin);
                mi.CreateTexture(_reader);
            }

            return _images[index];
        }

        // LoadTextureWithOffsetXY 按照偏移，加载其中的图片
        public MImage LoadTextureWithOffsetXY(int index, int alignX, int alignY)
        {
            if (!_initialized)
                Initialize();
            if (_images == null || index < 0 || index >= _images.Length)
                return null;
            if (_images[index] == null)
            {
                _fStream.Position = _indexList[index];
                _fStream.Seek(_indexList[index], SeekOrigin.Begin);
                _images[index] = new MImage(_reader);
            }
            MImage mi = _images[index];
            if (!mi.TextureValid)
            {
                if ((mi.Width == 0) || (mi.Height == 0))
                    return null;
                _fStream.Seek(_indexList[index] + 17, SeekOrigin.Begin);
                mi.CreateTexture(_reader, alignX, alignY);
            }
            return _images[index];
        }
        public MImage LoadTextureNoOffsetXY(int index)
        {
            if (!_initialized)
                Initialize();
            if (_images == null || index < 0 || index >= _images.Length)
                return null;
            if (_images[index] == null)
            {
                _fStream.Position = _indexList[index];
                _fStream.Seek(_indexList[index], SeekOrigin.Begin);
                _images[index] = new MImage(_reader);
            }
            MImage mi = _images[index];
            if (!mi.TextureValid)
            {
                if ((mi.Width == 0) || (mi.Height == 0))
                    return null;
                _fStream.Seek(_indexList[index] + 17, SeekOrigin.Begin);
                mi.CreateTexture(_reader);
            }
            return _images[index];
        }

        // public MImage CheckImageWithOffsetXY(int index, int offsetX, int offsetY)
        // {
        //     if (!_initialized)
        //         Initialize();

        //     if (_images == null || index < 0 || index >= _images.Length)
        //         return null;

        //     if (_images[index] == null)
        //     {
        //         _fStream.Position = _indexList[index];
        //         _fStream.Seek(_indexList[index], SeekOrigin.Begin);
        //         _images[index] = new MImage(_reader);
        //     }
        //     MImage mi = _images[index];
        //     if (!mi.TextureValid)
        //     {
        //         if ((mi.Width == 0) || (mi.Height == 0))
        //             return null;
        //         _fStream.Seek(_indexList[index] + 17, SeekOrigin.Begin);
        //         mi.CreateTexture(_reader);
        //     }

        //     return _images[index];
        // }

        public int getCount()
        {
            return _count;
        }

        public string getLibPath()
        {
            return _fileName;
        }

        public void close()
        {
            if (_initialized)
            {
                if (_reader != null)
                    _reader.Close();
                if (_fStream != null)
                    _fStream.Close();
                _initialized = false;
                _count = 0;
                _images = null;
                _frames = null;
                _indexList = null;
            }
        }

        public void clearImage(int index)
        {
            if (_images != null)
            {
                _images[index].TextureValid = false;
                _images[index] = null;
            }
        }

        private MImage[] getImageInfos()
        {
            if (!_initialized)
                Initialize();
            var imageInfos = new MImage[_count];
            for (int i = 0; i < _count; i++)
            {
                imageInfos[i] = getMImageInfo(i);
            }
            return imageInfos;
        }

        // alignmentOffset 对齐图集，新的参照点(找出最小的 x y ????)
        public Vector2Int alignmentOffset(MImage[] mImages)
        {
            var outResult = new Vector2Int(0, 0);
            foreach (var tmp in mImages)
            {
                if ((tmp.Width == 0) || (tmp.Height == 0))
                    continue;
                if (outResult.x > tmp.X)
                {
                    outResult.x = tmp.X;
                }
                if (outResult.y > tmp.Y)
                {
                    outResult.y = tmp.Y;
                }
            }
            return outResult;
        }

        public MImage[] checkImageAlignmentOffset(out Vector2Int alignOffset)
        {
            alignOffset = new Vector2Int();
            var imageInfos = getImageInfos();
            var alignOffsetTmp = alignmentOffset(imageInfos);
            alignOffset.x = alignOffsetTmp.x;
            alignOffset.y = alignOffsetTmp.y;
            var resOut = new MImage[imageInfos.Length];
            for (int i = 0; i < resOut.Length; i++)
            {
                var tmp = imageInfos[i];
                if (!tmp.TextureValid)
                {
                    if ((tmp.Width == 0) || (tmp.Height == 0))
                        continue;

                    _fStream.Seek(_indexList[i] + 17, SeekOrigin.Begin);
                    resOut[i] = tmp.CreateTexture(_reader, alignOffset.x, alignOffset.y);
                }
            }
            return resOut;
        }

        public MImage checkImageAlignmentOffset(Vector2Int alignOffset, int imageIndex, MImage tmp)
        {
            if (!tmp.TextureValid)
            {
                if ((tmp.Width == 0) || (tmp.Height == 0))
                    return tmp;
                _fStream.Seek(_indexList[imageIndex] + 17, SeekOrigin.Begin);
                return tmp.CreateTexture(_reader, alignOffset.x, alignOffset.y);
            }
            return tmp;
        }

        public void writeInfo(string infoPath)
        {
            if (File.Exists(infoPath))
            {
                return;
            }
            if (!_initialized)
                Initialize();
            var fileStream = File.OpenWrite(infoPath);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(_count);
            for (int i = 0; i < _count; i++)
            {
                binaryWriter.Write(0);
            }
            var positions = new int[_count];
            for (int i = 0; i < _count; i++)
            {
                var info = getMImageInfo(i);
                positions[i] = (int)binaryWriter.BaseStream.Position;
                if (info != null)
                {
                    info.writeImageInfo(binaryWriter);
                }
            }
            binaryWriter.Seek(4, SeekOrigin.Begin);
            for (int i = 0; i < _count; i++)
            {
                binaryWriter.Write(positions[i]);
            }
            binaryWriter.Flush();
            binaryWriter.Close();
            fileStream.Close();
        }
    }
    // 可以在编辑器里看到这个属性
    [System.Serializable]
    public sealed class MImage
    {
        public short Width, Height, X, Y, ShadowX, ShadowY;
        public byte Shadow;
        public int Length;

        public bool TextureValid;
        public Texture2D Image;

        //layer 2:
        public short MaskWidth, MaskHeight, MaskX, MaskY;
        public int MaskLength;

        public Boolean HasMask;

        public Texture2D MaskImage;

        // use for unity
        public string imagePath;
        public MImage()
        {
        }
        public MImage(BinaryReader reader)
        {
            //read layer 1
            Width = reader.ReadInt16();
            Height = reader.ReadInt16();
            X = reader.ReadInt16();
            Y = reader.ReadInt16();
            ShadowX = reader.ReadInt16();
            ShadowY = reader.ReadInt16();
            Shadow = reader.ReadByte();
            Length = reader.ReadInt32();

            //check if there's a second layer and read it
            HasMask = ((Shadow >> 7) == 1) ? true : false;
            if (HasMask)
            {
                reader.ReadBytes(Length);
                MaskWidth = reader.ReadInt16();
                MaskHeight = reader.ReadInt16();
                MaskX = reader.ReadInt16();
                MaskY = reader.ReadInt16();
                MaskLength = reader.ReadInt32();
            }
        }

        public void writeImageInfo(BinaryWriter writer)
        {
            writer.Write(Width);
            writer.Write(Height);
            writer.Write(X);
            writer.Write(Y);
            //writer.Write(ShadowX);
            //writer.Write(ShadowY);
            //writer.Write(Shadow);
            //writer.Write(Length);

            //if (HasMask)
            //{
            //    //writer.write(Length);
            //    writer.Write(MaskWidth);
            //    writer.Write(MaskHeight);
            //    writer.Write(MaskX);
            //    writer.Write(MaskY);
            //    writer.Write(MaskLength);
            //}
        }

        private static byte[] DecompressImage(byte[] image)
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(image), CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }

        private Color32[] covert(byte[] decomp, int width, int heigth)
        {
            int length = decomp.Length / 4;
            Color32[] resout = new Color32[length];
            int index = 0;
            //行数
            for (int h = heigth - 1; h >= 0; h--)
            {
                for (int w = 0; w < width; w++)
                {
                    int j = w * 4 + (h * 4 * width);
                    resout[index++] = new Color32(decomp[j + 2], decomp[j + 1], decomp[j], decomp[j + 3]);
                }
            }
            return resout;
        }

        private Color32[,] covertMonster(byte[] decomp, int width, int heigth)
        {
            int length = decomp.Length / 4;
            Color32[,] resout = new Color32[width, heigth];

            //行数
            for (int h = heigth - 1; h >= 0; h--)
            {
                for (int w = 0; w < width; w++)
                {
                    int j = w * 4 + (h * 4 * width);
                    try
                    {
                        resout[w, heigth - h - 1] = new Color32(decomp[j + 2], decomp[j + 1], decomp[j], decomp[j + 3]);
                    }
                    catch (Exception e)
                    {
                        var errorString = e.ToString();
                    }
                }
            }
            return resout;
        }

        public MImage CreateTexture(BinaryReader reader, int alignX, int alignY)
        {
            int w = Width;// + (4 - Width % 4) % 4;
            int h = Height;// + (4 - Height % 4) % 4;

            byte[] decomp = DecompressImage(reader.ReadBytes(Length));
            var colors = covert(decomp, w, h);
            //新参考点对齐
            var alw = X - alignX;
            var alh = Y - alignY;
            w += alw;
            h += alh;
            Image = new Texture2D(w, h);
            var colorsAlign = new Color32[w * h];
            for (int i = 0; i < w * h; i++)
            {
                colorsAlign[i] = new Color32(0, 0, 0, 0);
            }
            Image.SetPixels32(colorsAlign);
            Image.SetPixels32(alw, 0, Width, Height, colors);
            Image.Apply();
            if (HasMask)
            {
                reader.ReadBytes(12);
                //width, height, TextureFormat.RGBA32, Texture.GenerateAllMips, linear: false, IntPtr.Zero
                MaskImage = new Texture2D(Width, Width);
                decomp = DecompressImage(reader.ReadBytes(Length));

                MaskImage.SetPixels32(covert(decomp, Width, Width));
                MaskImage.Apply();
            }
            return this;
        }

        public void CreateTexture(BinaryReader reader)
        {
            // int w = Width;// + (4 - Width % 4) % 4;
            // int h = Height;// + (4 - Height % 4) % 4;
            int w = Width  + (4 - Width % 4) % 4;
            int h = Height  + (4 - Height % 4) % 4;
            Image = new Texture2D(w, h);
            byte[] decomp = DecompressImage(reader.ReadBytes(Length));

            try
            {
                Image.SetPixels32(covert(decomp, w, h));
                //Image.SetPixelData(decomp, 0);
                Image.Apply();
            }
            catch (Exception e)
            {
                Logger.Errorf("导出出错了{%s},宽:%d,高:%d ,异常信息:{%s}", this.Length,Width,Height, e.ToString());
            }

            if (HasMask)
            {
                reader.ReadBytes(12);
                //width, height, TextureFormat.RGBA32, Texture.GenerateAllMips, linear: false, IntPtr.Zero
                MaskImage = new Texture2D(w, h);
                decomp = DecompressImage(reader.ReadBytes(Length));

                MaskImage.SetPixels32(covert(decomp, w, h));
                MaskImage.Apply();
            }
        }
    }

    public sealed class FileNameComparer : IComparer<string>
    {
        int IComparer<string>.Compare(String x, string y)
        {
            throw new NotImplementedException();
        }
    }
}
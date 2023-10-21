
using System;
using System.IO;

namespace MLibraryUtils
{
    public class MInfoLibrary
    {
        private MImage[] mImage;
        private string fileName;
        public MInfoLibrary(string fileName)
        {
            this.fileName = fileName;

        }
        public void LoadMapInfo()
        {
            try
            {
                var _fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                var _reader = new BinaryReader(_fStream);
                var count = _reader.ReadInt32();
                mImage = new MImage[count];
                Logger.Debugf("总数量:%d", count);
                for (int i = 0; i < count; i++)
                {
                    mImage[i] = new MImage();
                    mImage[i].X = _reader.ReadInt16();
                    mImage[i].Y = _reader.ReadInt16();
                    mImage[i].Width = _reader.ReadInt16();
                    mImage[i].Height = _reader.ReadInt16();
                }
                _reader.Close();
                _fStream.Close();
            }
            catch (Exception e)
            {
                Logger.Errorf("read file error:%s", e.ToString());
            }
        }

        public MImage getMImageInfo(int i)
        {
            try
            {
                if (mImage == null)
                {
                    return null;
                }
                if (i < 0 || i >= mImage.Length)
                {
                    return null;
                }
                return mImage[i];
            }
            catch (Exception e)
            {
                Logger.Errorf("++++++++++++");
                return null;
            }
        }
    }
}

// namespace MLibraryUtils
// {
//     public class MInfoLibrary
//     {
//         private MImage[] mImage;
//         public MInfoLibrary(string fileName)
//         {
//             var _fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
//             var _reader = new BinaryReader(_fStream);
//             var count = _reader.ReadInt32();
//             mImage = new MImage[count];
//             int[] offsets = new int[count];
//             for (int i = 0; i < count; i++)
//             {
//                 offsets[i] = _reader.ReadInt32();
//             }
//             for (int i = 0; i < count; i++)
//             {
//                 if (0 != offsets[i])
//                 {
//                     mImage[i] = new MImage();
//                     mImage[i].Width = _reader.ReadInt16();
//                     mImage[i].Height = _reader.ReadInt16();
//                     mImage[i].X = _reader.ReadInt16();
//                     mImage[i].Y = _reader.ReadInt16();
//                 }
//             }
//             _reader.Close();
//             _fStream.Close();
//         }

//         public MImage getMImageInfo(int i)
//         {
//             return mImage[i];
//         }
//     }
// }

using System.IO;

namespace MLibraryUtils
{
    public class MInfoLibrary
    {
        private MImage[] mImage;
        public MInfoLibrary(string fileName)
        {
            var _fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            var _reader = new BinaryReader(_fStream);
            var count = _reader.ReadInt32();
            mImage = new MImage[count];
            int[] offsets = new int[count];
            for (int i = 0; i < count; i++)
            {
                offsets[i] = _reader.ReadInt32();
            }
            for (int i = 0; i < count; i++)
            {
                if (0 != offsets[i])
                {
                    mImage[i] = new MImage();
                    mImage[i].Width = _reader.ReadInt16();
                    mImage[i].Height = _reader.ReadInt16();
                    mImage[i].X = _reader.ReadInt16();
                    mImage[i].Y = _reader.ReadInt16();
                }
            }
            _reader.Close();
            _fStream.Close();
        }

        public MImage getMImageInfo(int i)
        {
            return mImage[i];
        }
    }
}
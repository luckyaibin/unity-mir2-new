using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
 

public class ChatItem
{
    public ulong UniqueID;
    public string Title;
    public MirGridType Grid;

    public string RegexInternalName
    {
        get { return $"<{Title.Replace("(", "\\(").Replace(")", "\\)")}>"; }
    }

    public string InternalName
    {
        get { return $"<{Title}/{UniqueID}>"; }
    }

    public ChatItem() { }

    public ChatItem(BinaryReader reader)
    {
        UniqueID = reader.ReadUInt64();
        Title = reader.ReadString();
        Grid = (MirGridType)reader.ReadByte();
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(UniqueID);
        writer.Write(Title);
        writer.Write((byte)Grid);
    }
}
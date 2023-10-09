using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace ServerPackets
{
    public sealed class KeepAlive : Packet
    {
        public override short Index
        {
            get { return (short)ServerPacketIds.KeepAlive; }
        }
        public long Time;

        protected override void ReadPacket(BinaryReader reader)
        {
            Time = reader.ReadInt64();
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(Time);
        }
    }
    public sealed class Connected : Packet
    {
        public override short Index
        {
            get { return (short)ServerPacketIds.Connected; }
        }
        public long Time;

        protected override void ReadPacket(BinaryReader reader)
        {
            Time = reader.ReadInt64();
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(Time);
        }
    }


    public sealed class ClientVersion : Packet
    {
        public override short Index
        {
            get { return (short)ServerPacketIds.ClientVersion; }
        }

        public byte Result;
        /*
         * 0: Wrong Version
         * 1: Correct Version
         */

        protected override void ReadPacket(BinaryReader reader)
        {
            Result = reader.ReadByte();
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(Result);
        }
    }
    public sealed class Disconnect : Packet
    {
        public override short Index
        {
            get { return (short)ServerPacketIds.Disconnect; }
        }

        public byte Reason;

        /*
         * 0: Server Closing.
         * 1: Another User.
         * 2: Packet Error.
         * 3: Server Crashed.
         */

        protected override void ReadPacket(BinaryReader reader)
        {
            Reason = reader.ReadByte();
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(Reason);
        }
    }
    public sealed class NewAccount : Packet
    {
        public override short Index
        {
            get { return (short)ServerPacketIds.NewAccount; }
        }

        public byte Result;
        /*
         * 0: Disabled
         * 1: Bad AccountID
         * 2: Bad Password
         * 3: Bad Email
         * 4: Bad Name
         * 5: Bad Question
         * 6: Bad Answer
         * 7: Account Exists.
         * 8: Success
         */

        protected override void ReadPacket(BinaryReader reader)
        {
            Result = reader.ReadByte();
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(Result);
        }
    }
    public sealed class ChangePassword : Packet
    {
        public override short Index
        {
            get { return (short)ServerPacketIds.ChangePassword; }
        }

        public byte Result;
        /*
         * 0: Disabled
         * 1: Bad AccountID
         * 2: Bad Current Password
         * 3: Bad New Password
         * 4: Account Not Exist
         * 5: Wrong Password
         * 6: Success
         */
        protected override void ReadPacket(BinaryReader reader)
        {
            Result = reader.ReadByte();
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(Result);
        }
    }
    public sealed class ChangePasswordBanned : Packet
    {
        public override short Index
        {
            get { return (short)ServerPacketIds.ChangePasswordBanned; }
        }

        public string Reason = string.Empty;
        public DateTime ExpiryDate;

        protected override void ReadPacket(BinaryReader reader)
        {
            Reason = reader.ReadString();
            ExpiryDate = DateTime.FromBinary(reader.ReadInt64());
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(Reason);
            writer.Write(ExpiryDate.ToBinary());
        }
    }
    public sealed class Login : Packet
    {
        public override short Index
        {
            get { return (short)ServerPacketIds.Login; }
        }

        public byte Result;
        /*
         * 0: Disabled
         * 1: Bad AccountID
         * 2: Bad Password
         * 3: Account Not Exist
         * 4: Wrong Password
         */

        protected override void ReadPacket(BinaryReader reader)
        {
            Result = reader.ReadByte();
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(Result);
        }
    }
    public sealed class LoginBanned : Packet
    {
        public override short Index
        {
            get { return (short)ServerPacketIds.LoginBanned; }
        }

        public string Reason = string.Empty;
        public DateTime ExpiryDate;

        protected override void ReadPacket(BinaryReader reader)
        {
            Reason = reader.ReadString();
            ExpiryDate = DateTime.FromBinary(reader.ReadInt64());
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(Reason);
            writer.Write(ExpiryDate.ToBinary());
        }
    }
    public sealed class LoginSuccess : Packet
    {
        public override short Index
        {
            get { return (short)ServerPacketIds.LoginSuccess; }
        }

        public List<SelectInfo> Characters = new List<SelectInfo>();

        protected override void ReadPacket(BinaryReader reader)
        {
            int count = reader.ReadInt32();

            for (int i = 0; i < count; i++)
                Characters.Add(new SelectInfo(reader));
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(Characters.Count);

            for (int i = 0; i < Characters.Count; i++)
                Characters[i].Save(writer);
        }
    }

    public sealed class StartGame : Packet
    {
        public override short Index
        {
            get { return (short)ServerPacketIds.StartGame; }
        }

        public byte Result;
        public int Resolution;

        /*
         * 0: Disabled.
         * 1: Not logged in
         * 2: Character not found.
         * 3: Start Game Error
         * */

        protected override void ReadPacket(BinaryReader reader)
        {
            Result = reader.ReadByte();
            Resolution = reader.ReadInt32();
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(Result);
            writer.Write(Resolution);
        }
    }

    public sealed class ObjectPlayer : Packet
    {
        public override short Index
        {
            get
            {
                return (short)ServerPacketIds.ObjectPlayer;
            }
        }

        public uint ObjectID;
        public string Name = string.Empty;
        public string GuildName = string.Empty;
        public string GuildRankName = string.Empty;
        public Color32 NameColour;
        public MirClass Class;
        public MirGender Gender;
        public ushort Level;
        public Vector2 Location;
        public MirDirection Direction;

        public byte Hair;
        public byte Light;
        public short Weapon, WeaponEffect, Armour;
        //public PoisionType Poision;
        public bool Dead, Hidden;
        // public SpellEffect Effect;

        public byte WingEffect;
        public bool Extra;

        public short MountType;
        public bool RidingMount;
        public bool Fishing;
        public short TransformType;
        public uint ElementOrbEffect;
        public uint ElementOrbLvl;
        public uint ElementOrbMax;

        // public List<BuffType> Buffs = new List<BuffType>();

        // public LevelEffects LevelEffects;

        protected override void ReadPacket(BinaryReader reader)
        {
            ObjectID = reader.ReadUInt32();
            Name = reader.ReadString();
            GuildName = reader.ReadString();
            GuildRankName = reader.ReadString();
            NameColour = CommonUtils.argb2Color32(reader.ReadInt32());
            Class = (MirClass)reader.ReadByte();
            Gender = (MirGender)reader.ReadByte();
            Level = reader.ReadUInt16();
            Location = new Vector2(reader.ReadInt32(), reader.ReadInt32());
            Direction = (MirDirection)reader.ReadByte();
            Hair = reader.ReadByte();
            Light = reader.ReadByte();
            Weapon = reader.ReadInt16();
            WeaponEffect = reader.ReadInt16();
            Armour = reader.ReadInt16();
            // Poison = (PoisonType)reader.ReadUInt16();
            Dead = reader.ReadBoolean();
            Hidden = reader.ReadBoolean();
            // Effect = (SpellEffect)reader.ReadByte();
            WingEffect = reader.ReadByte();
            Extra = reader.ReadBoolean();
            MountType = reader.ReadInt16();
            RidingMount = reader.ReadBoolean();
            Fishing = reader.ReadBoolean();

            TransformType = reader.ReadInt16();

            ElementOrbEffect = reader.ReadUInt32();
            ElementOrbLvl = reader.ReadUInt32();
            ElementOrbMax = reader.ReadUInt32();

            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                //Buffs.Add((BuffType)reader.ReadByte());
            }

            // LevelEffects = (LevelEffects)reader.ReadByte();
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
    public sealed class ObjectWalk : Packet
    {
        public override short Index
        {
            get { return (short)ServerPacketIds.ObjectWalk; }
        }

        public uint ObjectID;
        public Vector2 Location;
        public MirDirection Direction;

        protected override void ReadPacket(BinaryReader reader)
        {
            ObjectID = reader.ReadUInt32();
            Location = new Vector2(reader.ReadInt32(), reader.ReadInt32());
            Direction = (MirDirection)reader.ReadByte();
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(ObjectID);
            writer.Write(Location.x);
            writer.Write(Location.y);
            writer.Write((byte)Direction);
        }
    }
    public sealed class ObjectRun : Packet
    {
        public override short Index
        {
            get { return (short)ServerPacketIds.ObjectRun; }
        }

        public uint ObjectID;
        public Vector2 Location;
        public MirDirection Direction;

        protected override void ReadPacket(BinaryReader reader)
        {
            ObjectID = reader.ReadUInt32();
            Location = new Vector2(reader.ReadInt32(), reader.ReadInt32());
            Direction = (MirDirection)reader.ReadByte();
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(ObjectID);
            writer.Write(Location.x);
            writer.Write(Location.y);
            writer.Write((byte)Direction);
        }
    }
}
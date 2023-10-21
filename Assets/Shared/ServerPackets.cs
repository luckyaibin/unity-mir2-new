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

    public sealed class MapInformation : Packet
    {
        public override short Index
        {
            get { return (short)ServerPacketIds.MapInformation; }
        }
        public Int32 MapIndex = 0;
        public string FileName = string.Empty;
        public string Title = string.Empty;
        public ushort MiniMap, BigMap, Music;
        public LightSetting Lights;
        public bool Lightning, Fire;
        public byte MapDarkLight;

        protected override void ReadPacket(BinaryReader reader)
        {
            MapIndex = reader.ReadInt32();
            FileName = reader.ReadString();
            Title = reader.ReadString();
            MiniMap = reader.ReadUInt16();
            BigMap = reader.ReadUInt16();
            Lights = (LightSetting)reader.ReadByte();
            byte bools = reader.ReadByte();
            if ((bools & 0x01) == 0x01) Lightning = true;
            if ((bools & 0x02) == 0x02) Fire = true;
            MapDarkLight = reader.ReadByte();
            Music = reader.ReadUInt16();
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(FileName);
            writer.Write(Title);
            writer.Write(MiniMap);
            writer.Write(BigMap);
            writer.Write((byte)Lights);
            byte bools = 0;
            bools |= (byte)(Lightning ? 0x01 : 0);
            bools |= (byte)(Fire ? 0x02 : 0);
            writer.Write(bools);
            writer.Write(MapDarkLight);
            writer.Write(Music);
        }
    }
    public sealed class UserInformation : Packet
    {
        public override short Index
        {
            get { return (short)ServerPacketIds.UserInformation; }
        }

        public uint ObjectID;
        public uint RealId;
        public string Name = string.Empty;
        public string GuildName = string.Empty;
        public string GuildRank = string.Empty;
        public Color32 NameColour;
        public MirClass Class;
        public MirGender Gender;
        public ushort Level;
        public Vector2 Location;
        public MirDirection Direction;
        public byte Hair;
        public ushort HP, MP;
        public long Experience, MaxExperience;

        public LevelEffects LevelEffects;

        public UserItem[] Inventory, Equipment, QuestInventory;
        public uint Gold, Credit;

        public bool HasExpandedStorage;
        public DateTime ExpandedStorageExpiryTime;

        public List<ClientMagic> Magics = new List<ClientMagic>();

        public List<ClientIntelligentCreature> IntelligentCreatures = new List<ClientIntelligentCreature>();//IntelligentCreature
        public IntelligentCreatureType SummonedCreatureType = IntelligentCreatureType.None;//IntelligentCreature
        public bool CreatureSummoned;//IntelligentCreature



        protected override void ReadPacket(BinaryReader reader)
        {
            ObjectID = reader.ReadUInt32();
            RealId = reader.ReadUInt32();
            Name = reader.ReadString();
            GuildName = reader.ReadString();
            GuildRank = reader.ReadString();
            NameColour = CommonUtils.argb2Color32(reader.ReadInt32());
            Class = (MirClass)reader.ReadByte();
            Gender = (MirGender)reader.ReadByte();
            Level = reader.ReadUInt16();
            Location = new Vector2(reader.ReadInt32(), reader.ReadInt32());
            Direction = (MirDirection)reader.ReadByte();
            Hair = reader.ReadByte();
            HP = reader.ReadUInt16();
            MP = reader.ReadUInt16();

            Experience = reader.ReadInt64();
            MaxExperience = reader.ReadInt64();

            // LevelEffects = (LevelEffects)reader.ReadByte();

            // if (reader.ReadBoolean())
            // {
            //     Inventory = new UserItem[reader.ReadInt32()];
            //     for (int i = 0; i < Inventory.Length; i++)
            //     {
            //         if (!reader.ReadBoolean()) continue;
            //         Inventory[i] = new UserItem(reader);
            //     }
            // }

            // if (reader.ReadBoolean())
            // {
            //     Equipment = new UserItem[reader.ReadInt32()];
            //     for (int i = 0; i < Equipment.Length; i++)
            //     {
            //         if (!reader.ReadBoolean()) continue;
            //         Equipment[i] = new UserItem(reader);
            //     }
            // }

            // if (reader.ReadBoolean())
            // {
            //     QuestInventory = new UserItem[reader.ReadInt32()];
            //     for (int i = 0; i < QuestInventory.Length; i++)
            //     {
            //         if (!reader.ReadBoolean()) continue;
            //         QuestInventory[i] = new UserItem(reader);
            //     }
            // }

            Gold = reader.ReadUInt32();
            Credit = reader.ReadUInt32();

            HasExpandedStorage = reader.ReadBoolean();
            ExpandedStorageExpiryTime = DateTime.FromBinary(reader.ReadInt64());

            int count = reader.ReadInt32();

            for (int i = 0; i < count; i++)
                Magics.Add(new ClientMagic(reader));

            //IntelligentCreature
            count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
                IntelligentCreatures.Add(new ClientIntelligentCreature(reader));
            SummonedCreatureType = (IntelligentCreatureType)reader.ReadByte();
            CreatureSummoned = reader.ReadBoolean();
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(ObjectID);
            writer.Write(RealId);
            writer.Write(Name);
            writer.Write(GuildName);
            writer.Write(GuildRank);
            writer.Write(CommonUtils.color32toArgb(NameColour));
            writer.Write((byte)Class);
            writer.Write((byte)Gender);
            writer.Write(Level);
            writer.Write(Location.x);
            writer.Write(Location.y);
            writer.Write((byte)Direction);
            writer.Write(Hair);
            writer.Write(HP);
            writer.Write(MP);

            writer.Write(Experience);
            writer.Write(MaxExperience);

            writer.Write((byte)LevelEffects);

            writer.Write(Inventory != null);
            if (Inventory != null)
            {
                writer.Write(Inventory.Length);
                for (int i = 0; i < Inventory.Length; i++)
                {
                    writer.Write(Inventory[i] != null);
                    if (Inventory[i] == null) continue;

                    Inventory[i].Save(writer);
                }

            }

            writer.Write(Equipment != null);
            if (Equipment != null)
            {
                writer.Write(Equipment.Length);
                for (int i = 0; i < Equipment.Length; i++)
                {
                    writer.Write(Equipment[i] != null);
                    if (Equipment[i] == null) continue;

                    Equipment[i].Save(writer);
                }
            }

            writer.Write(QuestInventory != null);
            if (QuestInventory != null)
            {
                writer.Write(QuestInventory.Length);
                for (int i = 0; i < QuestInventory.Length; i++)
                {
                    writer.Write(QuestInventory[i] != null);
                    if (QuestInventory[i] == null) continue;

                    QuestInventory[i].Save(writer);
                }
            }

            writer.Write(Gold);
            writer.Write(Credit);

            writer.Write(HasExpandedStorage);
            writer.Write(ExpandedStorageExpiryTime.ToBinary());

            writer.Write(Magics.Count);
            for (int i = 0; i < Magics.Count; i++)
                Magics[i].Save(writer);

            //IntelligentCreature
            writer.Write(IntelligentCreatures.Count);
            for (int i = 0; i < IntelligentCreatures.Count; i++)
                IntelligentCreatures[i].Save(writer);
            writer.Write((byte)SummonedCreatureType);
            writer.Write(CreatureSummoned);
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

    public sealed class ObjectMonster : Packet
    {
        public override short Index
        {
            get { return (short)ServerPacketIds.ObjectMonster; }
        }

        public uint ObjectID;
        public string Name = string.Empty;
        public Color32 NameColour;
        public Vector2 Location;
        public Monster Image;
        public MirDirection Direction;
        public byte Effect, AI, Light;
        public bool Dead, Skeleton;
        public PoisonType Poison;
        public bool Hidden, Extra;
        public byte ExtraByte;
        public long ShockTime;
        public bool BindingShotCenter;

        public List<BuffType> Buffs = new List<BuffType>();

        protected override void ReadPacket(BinaryReader reader)
        {
            ObjectID = reader.ReadUInt32();
            Name = reader.ReadString();
            NameColour = CommonUtils.argb2Color32(reader.ReadInt32());
            Location = new Vector2(reader.ReadInt32(), reader.ReadInt32());
            Image = (Monster)reader.ReadUInt16();
            Direction = (MirDirection)reader.ReadByte();
            Effect = reader.ReadByte();
            AI = reader.ReadByte();
            Light = reader.ReadByte();
            Dead = reader.ReadBoolean();
            Skeleton = reader.ReadBoolean();
            Poison = (PoisonType)reader.ReadUInt16();
            Hidden = reader.ReadBoolean();
            ShockTime = reader.ReadInt64();
            BindingShotCenter = reader.ReadBoolean();
            Extra = reader.ReadBoolean();
            ExtraByte = reader.ReadByte();

            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                Buffs.Add((BuffType)reader.ReadByte());
            }
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(ObjectID);
            writer.Write(Name);
            writer.Write(CommonUtils.color32toArgb(NameColour));
            writer.Write(Location.x);
            writer.Write(Location.y);
            writer.Write((ushort)Image);
            writer.Write((byte)Direction);
            writer.Write(Effect);
            writer.Write(AI);
            writer.Write(Light);
            writer.Write(Dead);
            writer.Write(Skeleton);
            writer.Write((ushort)Poison);
            writer.Write(Hidden);
            writer.Write(ShockTime);
            writer.Write(BindingShotCenter);
            writer.Write(Extra);
            writer.Write((byte)ExtraByte);

            writer.Write(Buffs.Count);
            for (int i = 0; i < Buffs.Count; i++)
            {
                writer.Write((byte)Buffs[i]);
            }
        }

    }
    public sealed class ObjectNPC : Packet
    {
        public override short Index
        {
            get { return (short)ServerPacketIds.ObjectNpc; }
        }

        public uint ObjectID;
        public string Name = string.Empty;

        public Color32 NameColour;
        public ushort Image;
        public Color32 Colour;
        public Vector2 Location;
        public MirDirection Direction;
        public List<int> QuestIDs = new List<int>();

        protected override void ReadPacket(BinaryReader reader)
        {
            ObjectID = reader.ReadUInt32();
            Name = reader.ReadString();
            NameColour = CommonUtils.argb2Color32(reader.ReadInt32());
            Image = reader.ReadUInt16();
            Colour = CommonUtils.argb2Color32(reader.ReadInt32());
            Location = new Vector2(reader.ReadInt32(), reader.ReadInt32());
            Direction = (MirDirection)reader.ReadByte();

            int count = reader.ReadInt32();

            for (var i = 0; i < count; i++)
                QuestIDs.Add(reader.ReadInt32());
        }
        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(ObjectID);
            writer.Write(Name);
            writer.Write(CommonUtils.color32toArgb(NameColour));
            writer.Write(Image);
            writer.Write(CommonUtils.color32toArgb(Colour));
            writer.Write(Location.x);
            writer.Write(Location.y);
            writer.Write((byte)Direction);

            writer.Write(QuestIDs.Count);

            for (int i = 0; i < QuestIDs.Count; i++)
                writer.Write(QuestIDs[i]);
        }
    }
    public sealed class ObjectAttack : Packet
    {
        public override short Index
        {
            get { return (short)ServerPacketIds.ObjectAttack; }
        }

        public uint ObjectID;
        public Vector2 Location;
        public MirDirection Direction;
        public Spell Spell;
        public byte Level;
        public byte Type;

        protected override void ReadPacket(BinaryReader reader)
        {
            ObjectID = reader.ReadUInt32();
            Location = new Vector2(reader.ReadInt32(), reader.ReadInt32());
            Direction = (MirDirection)reader.ReadByte();
            Spell = (Spell)reader.ReadByte();
            Level = reader.ReadByte();
            Type = reader.ReadByte();
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(ObjectID);
            writer.Write(Location.x);
            writer.Write(Location.y);
            writer.Write((byte)Direction);
            writer.Write((byte)Spell);
            writer.Write(Level);
            writer.Write(Type);
        }
    }
}
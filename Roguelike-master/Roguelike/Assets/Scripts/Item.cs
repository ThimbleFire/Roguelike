using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("Item")]
public class Item
{
    [Serializable]
    public class Attribute
    {
        public enum AType
        {
            Level, Strength, Dexterity, Constitution, Intelligence
        }
        public AType type;
        public byte value;
    }

    [Serializable]
    public class Prefix
    {
        public enum PType
        {
            Dmg_Ele_Fire = 4,
            Dmg_Ele_Cold = 5,
            Dmg_Ele_Lightning = 6,
            Dmg_Ele_Poison = 7,
            Dmg_Phys_Percent = 3,
            Def_Phys_Percent = 9,
            Def_Dmg_Reduction_Phys = 10,
            Def_Dmg_Reduction_Magic = 11,
            Def_Ele_Res_Fire = 13,
            Def_Ele_Res_Cold = 14,
            Def_Ele_Res_Lightning = 15,
            Def_Ele_Res_Poison = 16,
            Def_Ele_Res_All = 17,
            On_Hit_Life = 18,
            On_Kill_Life = 19,
            Plus_Mana = 23,
            Plus_Regen_Mana = 25,
            Plus_Magic_Find = 35,
            Plus_Item_Find = 36,
            Plus_Attack_Rating = 37
        }
        public PType type;
        public int value;
    }

    [Serializable]
    public class Suffix
    {
        public enum SType
        {
            Plus_Str = 26,
            Plus_Dex = 27,
            Plus_Con = 28,
            Plus_Int = 29,

            Def_Phys_Flat = 8,
            Dmg_Phys_Min = 1,
            Dmg_Phys_Max = 2,

            Dmg_Ele_Fire = 4,
            Dmg_Ele_Cold = 5,
            Dmg_Ele_Lightning = 6,
            Dmg_Ele_Poison = 8,

            On_Kill_Mana = 20,
            On_Hit_Mana = 21,

            Plus_Speed_Phys = 30,
            Plus_Speed_Magic = 31,
            Plus_Speed_Movement = 32,
            Plus_Block_Recovery = 33,
            Plus_Stagger_Recovery = 34,

            Def_Dmg_Reduction_All = 12,

            Plus_Life = 22,
            Plus_Regen_Life = 24,

            Plus_Magic_Find = 35,
            Plus_Item_Find = 36,
            Plus_Defence_Rating = 38,
            Plus_Blockrate = 39,
        }
        public SType type;
        public int value;
    }

    [Serializable]
    public class Implicit
    {
        public enum IType
        {
            Plus_Magic_Find = 35,
            Plus_Life = 22,
            Plus_Mana = 23,
            Def_Ele_Res_All = 17,
            Plus_Speed_Phys = 30,
            Plus_Speed_Magic = 31,
            Plus_Speed_Movement = 32,
            Plus_Block_Recovery = 33,
            Plus_Stagger_Recovery = 34,
            Def_Dmg_Reduction_All = 12,
            Plus_Regen_Life = 24,
            Plus_Regen_Mana = 25,
            Plus_Blockrate = 39,
        }
        public IType type;
        public int value;
    }

    public enum Type
    {
        ANY,
        HEAD,
        CHEST,
        GLOVES,
        LEGS,
        FEET,
        PRIMARY,
        SECONDARY,
        RING,
        NECK,
        CONSUMABLE,
        BELT
    }

    public Item()
    {
        Name = string.Empty;
        ItemType = Item.Type.ANY;
        SpriteUIFilename = string.Empty;
        ItemLevel = 0;
        DmgMin = 0;
        DmgMax = 0;
        DefMin = 0;
        DefMax = 0;
        Blockrate = 0;
        Durability = 0;
        Implicits = new List<Implicit>();
        Unique = false;
        Prefixes = new List<Prefix>();
        Suffixes = new List<Suffix>();
        Description = string.Empty;
        ReqStr = 0;
        ReqInt = 0;
        ReqDex = 0;
        ReqCons = 0;
        ReqLvl = 0;
    }

    public string Name;
    public Type ItemType;
    public string SpriteUIFilename;
    public string animationName;
    public int ItemLevel;
    public int DmgMin, DmgMax;
    public int DefMin, DefMax;
    public int Blockrate;
    public int Durability;
    public bool Unique;
    public string Description;
    public int ReqStr, ReqInt, ReqDex, ReqCons, ReqLvl;
    [XmlArray("Implicits")]
    public List<Implicit> Implicits;
    [XmlArray("Prefixes")]
    public List<Prefix> Prefixes;
    [XmlArray("Suffixes")]
    public List<Suffix> Suffixes;
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemStats : MonoBehaviour
{
    public bool Equipped = false;
    public const string hexMagic = "<color=#4850B8>";
    public const string hexRare = "<color=#FFFF00>";
    public const string hexGray = "<color=#8A8A8A>";
    public const string hexRed = "<color=#FF0000>";

    private Item item;

    public Item.Type ItemType { get { return item.ItemType; } }
    public List<Item.Implicit> Implicits { get { return item.Implicits; } }
    public List<Item.Prefix> Prefixes { get { return item.Prefixes; } }
    public List<Item.Suffix> Suffixes { get { return item.Suffixes; } }

    public byte Rarity { get { return ( byte )( item.Prefixes.Count + item.Suffixes.Count ); } }

    public int MinDamage
    {
        get {
            Item.Suffix s = item.Suffixes.Find( x => x.type == Item.Suffix.SType.Dmg_Phys_Min );
            Item.Prefix p = item.Prefixes.Find( x => x.type == Item.Prefix.PType.Dmg_Phys_Percent );

            if ( s != null && p != null ) return ( int )( (item.DmgMin + s.value ) * ( p.value / 100.0f + 1 ) );
            if ( s != null ) return item.DmgMin + s.value;
            if ( p != null ) return ( int )(item.DmgMin * ( p.value / 100.0f + 1 ) );
            
            return item.DmgMin;
        }
    }
    public int MaxDamage
    {
        get {
            Item.Suffix s = item.Suffixes.Find( x => x.type == Item.Suffix.SType.Dmg_Phys_Max );
            Item.Prefix p = item.Prefixes.Find( x => x.type == Item.Prefix.PType.Dmg_Phys_Percent );

            if ( s != null && p != null ) return ( int )( (item.DmgMax + s.value ) * ( p.value / 100.0f + 1 ) );
            if ( s != null ) return item.DmgMax + s.value;
            if ( p != null ) return ( int )(item.DmgMax * ( p.value / 100.0f + 1 ) );
            
            return item.DmgMax;
        }
    }
    public int Defense
    {
        get {
            Item.Suffix s = item.Suffixes.Find( x => x.type == Item.Suffix.SType.Def_Phys_Flat );
            Item.Prefix p = item.Prefixes.Find( x => x.type == Item.Prefix.PType.Def_Phys_Percent );

            if ( s != null && p != null ) return ( int )( ( item.DefMin + s.value ) * ( p.value / 100.0f + 1 ) );
            if ( s != null ) return item.DefMin + s.value;
            if ( p != null ) return ( int )(item.DefMin * ( p.value / 100.0f + 1 ) );
            
            return item.DefMin;
        }
    }
    public int Blockrate
    {
        get
        {
            Item.Suffix s = item.Suffixes.Find(x => x.type == Item.Suffix.SType.Plus_Blockrate);
            Item.Implicit i = item.Implicits.Find(x => x.type == Item.Implicit.IType.Plus_Blockrate);

            if (s != null && i != null) return item.Blockrate + s.value + i.value;
            if (s != null) return item.Blockrate + s.value;
            if (i != null) return item.Blockrate + i.value;

            return item.Blockrate;
        }
    }

    public AudioClip soundEndDrag;

    public string Tooltip {
        get {
            
            System.Text.StringBuilder t = new System.Text.StringBuilder( item.Name );
            
            t.Append( "\n" + Type_Text[( byte )item.ItemType] );
            
            if (item.ItemType == Item.Type.PRIMARY || item.ItemType == Item.Type.SECONDARY )
                t.Append( "\n" + hexGray + "One-hand damage: </color>" + hexMagic + MinDamage + " to " + MaxDamage + "</color>" );

            if (item.ItemType == Item.Type.SECONDARY && Blockrate > 0)
                t.Append("\n" + hexGray + "Chance to block: </color>" + hexMagic + Blockrate + "</color>");

            if (item.ItemType != Item.Type.RING && item.ItemType != Item.Type.NECK )
                t.Append( "\n" + hexGray + "Defense: </color>" + hexMagic + Defense + "</color>" );
            
            if ( item.Durability > 0 )
                t.Append( "\n" + hexGray + "Durability: </color>" + item.Durability );

            if (item.ReqStr > 0)
            {
                if (Entities.GetPCS.Strength < item.ReqStr)
                    t.Append(hexRed);
                else t.Append(hexGray);
                t.Append(string.Format("\nRequired Strength: {0} </color>", item.ReqStr));
            }
            if (item.ReqDex > 0)
            {
                if (Entities.GetPCS.Dexterity < item.ReqDex)
                    t.Append(hexRed);
                else t.Append(hexGray);
                t.Append(string.Format("\nRequired Dexterity: {0} </color>", item.ReqDex));
            }
            if (item.ReqInt > 0)
            {
                if (Entities.GetPCS.Intelligence < item.ReqInt)
                    t.Append(hexRed);
                else t.Append(hexGray);
                t.Append(string.Format("\nRequired Intelligence: {0} </color>", item.ReqInt));
            }
            if (item.ReqCons > 0)
            {
                if (Entities.GetPCS.Constitution < item.ReqCons)
                    t.Append(hexRed);
                else t.Append(hexGray);
                t.Append(string.Format("\nRequired Constitution: {0} </color>", item.ReqCons));
            }
            if (item.ReqLvl > 0)
            {
                if (Entities.GetPCS.Level < item.ReqLvl)
                    t.Append(hexRed);
                else t.Append(hexGray);
                t.Append(string.Format("\nRequired Level: {0} </color>", item.ReqLvl));
            }

            foreach ( var implicitMod in item.Implicits ) {
                t.Append( "\n" + hexMagic + string.Format( GearStats.Affix_Text[( byte )implicitMod.type], implicitMod.value ) + "</color>" );
            }
            foreach ( var prefix in item.Prefixes ) {
                t.Append( "\n" + hexMagic + string.Format( GearStats.Affix_Text[( byte )prefix.type], prefix.value ) + "</color>" );
            }
            foreach ( var suffix in item.Suffixes ) {
                t.Append( "\n" + hexMagic + string.Format( GearStats.Affix_Text[( byte )suffix.type], suffix.value ) + "</color>" );
            }

            if ( item.Description != string.Empty )
                t.Append( "\n\n<i>" + item.Description + "</i>" );

            return t.ToString();
        }
    }

    public static string[] Type_Text = new string[12]
    { 
        "Any", 
        "Helmet", 
        "Chest", 
        "Gloves", 
        "Legs", 
        "Feet", 
        "Weapon", 
        "Offhand", 
        "Ring", 
        "Amulet",
        "Consumable", 
        "Belt"
    };

    public void Load(string itemName)
    {
        item = XMLUtility.Load<Item>("Items/" + itemName);

        string path = "UI/Inventory/Item/" + item.ItemType + "/" + item.SpriteUIFilename;

        GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(path);
    }
}

public class GearStats {

    // The order of affix text must match the enum order on Character.StatID
    public static string[] Affix_Text = new string[40]
    {
        "+{0} to accuracy rating",
        "+{0} to minimum damage",
        "+{0} to maximum damage",
        "{0}% increased damage",
        "Adds {0} fire damage",
        "Adds {0} cold damage",
        "Adds {0} lightning damage",
        "Adds {0} poison damage",

        "+{0} to defense",
        "{0}% increased defence",
        "{0} physical damage reduction",
        "{0} magical damage reduction",
        "Damage reduced by {0}",
        "{0}% to fire resistance",
        "{0}% to cold resistance",
        "{0}% to lightning resistance",
        "{0}% to poison resistance",
        "{0}% to all resistances",

        "+{0} to life on hit",
        "+{0} to life after each kill",
        "+{0} to mana on hit",
        "+{0} to mana after each kill",

        "+{0} to life",
        "+{0} to mana",
        "+{0} to life regen per second",
        "+{0} to mana regen per second",
        "+{0} to strength",
        "+{0} to dexterity",
        "+{0} to constitution",
        "+{0} to intelligence",
        "+{0}% increase attack speed",
        "+{0}% faster cast rate",
        "+{0}% increased movement speed",
        "+{0}% faster block recovery",
        "+{0}% faster stagger recovery",

        "+{0}% magic find",
        "+{0} to maximum durability",
        "+{0} to attack rating",
        "+{0} to defence rating",
        "+{0} to block rate"
    };
}
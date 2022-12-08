using System;
using UnityEngine;

public class Item
{
    public enum SubCategories
    {
        Empty,
        Helmet,
        Chest,
        Belt,
        Legs,
        Foot_L,
        Foot_R,
        Hand_L,
        Hand_R,
        Offhand,
        Weapon,
        Shoulder_L,
        Shoulder_R,
        Cape,
        Neck,
        Ring,
        Bracelet
    }
    public enum Properties
    {
        ItemType,      //greatsword
        Category,      //weapon
        Subcategory,   //sword
        Material,      //metal
        Tier,          //1 to 3, normal, exceptional, masterclass
        Prefix1,
        Prefix2,
        Prefix3,
        Suffix1,
        Suffix2,
        Suffix3,
    }

    public Sprite Sprite { get; set; }
    public string Binary { get; set; }

    public int ItemType { get { return global::Binary.ToDecimal( Binary, 0 ); } }
    public int Category { get { return global::Binary.ToDecimal( Binary, 1 ); } }
    public int Subcategory { get { return global::Binary.ToDecimal( Binary, 2 ); } }
    public int Material { get { return global::Binary.ToDecimal( Binary, 3 ); } }
    public int Tier { get { return global::Binary.ToDecimal( Binary, 4 ); } }
    public int Prefix1 { get { return global::Binary.ToDecimal( Binary, 5 ); } }
    public int Prefix2 { get { return global::Binary.ToDecimal( Binary, 6 ); } }
    public int Prefix3 { get { return global::Binary.ToDecimal( Binary, 7 ); } }
    public int Suffix1 { get { return global::Binary.ToDecimal( Binary, 8 ); } }
    public int Suffix2 { get { return global::Binary.ToDecimal( Binary, 9 ); } }
    public int Suffix3 { get { return global::Binary.ToDecimal( Binary, 10 ); } }

    public Item(string binary)
    {
        this.Binary = binary;

        Sprite = SpriteManager.Instance.Get( Subcategory, ItemType );
    }

    public string BuildBinary()
    {
        string 
        bin  = global::Binary.ToBinary( ItemType );
        bin += global::Binary.ToBinary( Category );
        bin += global::Binary.ToBinary( Subcategory );
        bin += global::Binary.ToBinary( Material );
        bin += global::Binary.ToBinary( Tier );

        bin += global::Binary.ToBinary( Prefix1 );
        bin += global::Binary.ToBinary( Prefix2 );
        bin += global::Binary.ToBinary( Prefix3 );

        bin += global::Binary.ToBinary( Suffix1 );
        bin += global::Binary.ToBinary( Suffix2 );
        bin += global::Binary.ToBinary( Suffix3 );

        return bin;
    }
}
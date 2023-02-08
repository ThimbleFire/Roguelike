using System;

[Serializable]
public class EntityBaseStats
{
    public string animatorOverrideControllerFileName;
    
    public string Name;
    public int Level;
    public int Speed;
    
    public int LifeMax;
    public int LifeCurrent;
    public int ManaMax;
    public int ManaCurrent;
    
    public int RangeOfAggression;
    public int Experience;
    
    public int ItemFind;
    public int MagicFind;
    
    public int ResFire;
    public int ResCold;
    public int ResLight;
    public int ResPoison;
    public int ResAll;
    
    public int AttackRating;
    public int ChanceToBlock;
    public int Defense;
    
    public int DmgPhyMin;
    public int DmgPhyMax;

    public int DmgFireMin;
    public int DmgColdMin;
    public int DmgLightMin;
    public int DmgPoisonMin;
    public int DmgEleAllMin;

    public int DmgFireMax;
    public int DmgColdMax;
    public int DmgLightMax;
    public int DmgPoisonMax;
    public int DmgEleAllMax;
}

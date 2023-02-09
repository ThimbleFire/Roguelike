using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EntityEditor : EditorBase
{
    private const string LBL_NAME = "Name";
    private const string LBL_LEVEL = "Level";
    private const string LBL_SPEED = "Speed";
    private const string LBL_TREASURE_CLASS = "Treasure Class";
    private const string LBL_LIFE_MAX = "Maximum Life";
    private const string LBL_LIFE_CURRENT = "Current Life";
    private const string LBL_MANA_MAX = "Maximum Mana";
    private const string LBL_MANA_CURRENT = "Current Mana";    
    private const string LBL_RANGE_OF_AGGRESSION = "Range of Aggression";
    private const string LBL_EXPERIENCE = "Experience Reward" ;    
    private const string LBL_ITEM_FIND = "Item Find";
    private const string LBL_MAGIC_FIND = "Magic Find";    
    private const string LBL_RES_FIRE = "Fire Resistance";
    private const string LBL_RES_COLD = "Cold Resistance";
    private const string LBL_RES_LIGHT = "Lightning Resistance" ;
    private const string LBL_RES_POISON = "Poison Resistance" ;
    private const string LBL_RES_ELEMENTAL_ALL = "All Elemental Resistance";    
    private const string LBL_ATTACK_RATING = "Attack Rating" ;
    private const string LBL_CHANCE_TO_BLOCK = "Chance to Block" ;
    private const string LBL_DEFENSE = "Defense" ;    
    private const string LBL_DMG_PHYSICAL_MIN = "Minimum Physical Damage" ;
    private const string LBL_DMG_FIRE_MIN = "Minimum Fire Damage" ;
    private const string LBL_DMG_COLD_MIN ="Minimum Cold Damage" ;
    private const string LBL_DMG_LIGHT_MIN = "Minimum Lightning Damage" ;
    private const string LBL_DMG_POISON_MIN = "Minimum Poison Damage" ;
    private const string LBL_DMG_ELEMENTAL_ALL_MIN ="Minimum Elemental Damage" ;
    private const string LBL_DMG_PHYSICAL_MAX = "Maximum Physical Damage" ;
    private const string LBL_DMG_FIRE_MAX =  "Maximum Fire Damage" ;
    private const string LBL_DMG_COLD_MAX = "Maximum Cold Damage" ;
    private const string LBL_DMG_LIGHT_MAX =  "Maximum Lightning Damage" ;
    private const string LBL_DMG_POISON_MAX =  "Maximum Poison Damage" ;
    private const string LBL_DMG_ELEMENTAL_ALL_MAX = "Maximum Elemental Damage" ;
    private const string LBL_SPAWN_GROUP_SIZE = "Spawn Group Size";
    private const string LBL_SOUND_CLIPS_ON_ATTACK = "AudioClips_onAttack";
    private const string LBL_SOUND_CLIPS_ON_HIT = "AudioClips_onHit";
    private const string LBL_SOUND_CLIPS_ON_DEATH = "AudioClips_onDeath";
    private const string LBL_SOUND_CLIPS_ON_AGGRO = "AudioClips_onAggro";
    private const string LBL_SOUND_CLIPS_ON_IDLE = "AudioClips_onIdle";
    
    Vector2 scrollView;

    EntityReplacement activeEntity;
    TextAsset obj;
    UnityEngine.AnimatorOverrideController animatorOverrideController;
    public List<AudioClip> AudioClips_onAttack = new List<AudioClip>();
    public List<AudioClip> AudioClips_onHit = new List<AudioClip>();
    public List<AudioClip> AudioClips_onDeath = new List<AudioClip>();
    public List<AudioClip> AudioClips_onAggro = new List<AudioClip>();
    public List<AudioClip> AudioClips_onIdle = new List<AudioClip>();

    [MenuItem("Window/Editor/Entities")]
    private static void ShowWindow()
    {
        GetWindow(typeof(EntityEditor));
    }

    private void Awake()
    {
        so = new SerializedObject(this);
        activeEntity = new EntityReplacement();
    }

    protected override void MainWindow()
    {
        scrollView = EditorGUILayout.BeginScrollView(scrollView, false, true, GUILayout.Width(position.width));
        {
            obj = PaintXMLLookup(obj, "Resource File", true);
            if (PaintButton("Save"))
            {
                Save();
            }
            
            animatorOverrideController = 
            PaintAnimationOverrideControllerLookup( animatorOverrideController );
            PaintTextField(ref activeEntity.baseStats.Name, LBL_NAME );
            PaintIntField(ref activeEntity.baseStats.Level, LBL_LEVEL );
            PaintIntField(ref activeEntity.baseStats.Speed, LBL_SPEED );       
            PaintIntField(ref activeEntity.baseStats.TreasureClass, LBL_TREASURE_CLASS);
            PaintIntField(ref activeEntity.spawnGroupSize, LBL_SPAWN_GROUP_SIZE);
            PaintIntField(ref activeEntity.baseStats.LifeMax, LBL_LIFE_MAX);
            PaintIntField(ref activeEntity.baseStats.ManaMax, LBL_MANA_MAX);            
            PaintIntField(ref activeEntity.baseStats.RangeOfAggression, LBL_RANGE_OF_AGGRESSION);
            PaintIntField(ref activeEntity.baseStats.Experience, LBL_EXPERIENCE);            
            PaintIntField(ref activeEntity.baseStats.ItemFind, LBL_ITEM_FIND);
            PaintIntField(ref activeEntity.baseStats.MagicFind, LBL_MAGIC_FIND);            
            PaintIntField(ref activeEntity.baseStats.ResFire, LBL_RES_FIRE);
            PaintIntField(ref activeEntity.baseStats.ResCold, LBL_RES_COLD);
            PaintIntField(ref activeEntity.baseStats.ResLight, LBL_RES_LIGHT);
            PaintIntField(ref activeEntity.baseStats.ResPoison, LBL_RES_POISON);
            PaintIntField(ref activeEntity.baseStats.ResAll, LBL_RES_ELEMENTAL_ALL);            
            PaintIntField(ref activeEntity.baseStats.AttackRating, LBL_ATTACK_RATING);
            PaintIntField(ref activeEntity.baseStats.ChanceToBlock, LBL_CHANCE_TO_BLOCK);
            PaintIntField(ref activeEntity.baseStats.Defense, LBL_DEFENSE);            
            PaintIntField(ref activeEntity.baseStats.DmgPhyMin, LBL_DMG_PHYSICAL_MIN);            
            PaintIntField(ref activeEntity.baseStats.DmgFireMin, LBL_DMG_FIRE_MIN);
            PaintIntField(ref activeEntity.baseStats.DmgColdMin, LBL_DMG_COLD_MIN);
            PaintIntField(ref activeEntity.baseStats.DmgLightMin, LBL_DMG_LIGHT_MIN);
            PaintIntField(ref activeEntity.baseStats.DmgPoisonMin, LBL_DMG_POISON_MIN);
            PaintIntField(ref activeEntity.baseStats.DmgEleAllMin, LBL_DMG_ELEMENTAL_ALL_MIN);            
            PaintIntField(ref activeEntity.baseStats.DmgPhyMax, LBL_DMG_PHYSICAL_MAX);
            PaintIntField(ref activeEntity.baseStats.DmgFireMax, LBL_DMG_FIRE_MAX);
            PaintIntField(ref activeEntity.baseStats.DmgColdMax, LBL_DMG_COLD_MAX);
            PaintIntField(ref activeEntity.baseStats.DmgLightMax, LBL_DMG_LIGHT_MAX);
            PaintIntField(ref activeEntity.baseStats.DmgPoisonMax, LBL_DMG_POISON_MAX);
            PaintIntField(ref activeEntity.baseStats.DmgEleAllMax, LBL_DMG_ELEMENTAL_ALL_MAX);
            PaintList<AudioClip>( LBL_SOUND_CLIPS_ON_ATTACK );
            PaintList<AudioClip>( LBL_SOUND_CLIPS_ON_HIT );
            PaintList<AudioClip>( LBL_SOUND_CLIPS_ON_DEATH );
            PaintList<AudioClip>( LBL_SOUND_CLIPS_ON_AGGRO );
            PaintList<AudioClip>( LBL_SOUND_CLIPS_ON_IDLE );
        }
        EditorGUILayout.EndScrollView();
    }

    protected override void ResetProperties()
    {

    }

    protected override void LoadProperties(TextAsset textAsset)
    {
        activeEntity = XMLUtility.Load<EntityReplacement>(textAsset);

        animatorOverrideController = Resources.Load<AnimatorOverrideController>(activeEntity.animatorOverrideControllerFileName);
    }
    
    private void Save()
    {
        string filePath = AssetDatabase.GetAssetPath(animatorOverrideController).Substring(S_RESOURCE_DIR_LENGTH);
        filePath = filePath.Substring(0, filePath.Length - S_OVERRIDECONTROLLER_LENGTH);
        activeEntity.animatorOverrideControllerFileName = animatorOverrideController == null ? string.Empty : filePath;
        
        foreach( AudioClip AudioClip in AudioClips_onAttack ) {
            filePath = AssetDatabase.GetAssetPath(AudioClip).Substring(S_RESOURCE_DIR_LENGTH);
            filePath = filePath.Substring(0, filePath.Length - S_OGG_EXTENSION_LENGTH);
            activeEntity.soundClipFileNamesOnAttack.Add(filePath);
        }
    
        foreach( AudioClip AudioClip in AudioClips_onHit ) {
            filePath = AssetDatabase.GetAssetPath(AudioClip).Substring(S_RESOURCE_DIR_LENGTH);
            filePath = filePath.Substring(0, filePath.Length - S_OGG_EXTENSION_LENGTH);
            activeEntity.soundClipFileNamesOnHit.Add(filePath);
        }
    
        foreach( AudioClip AudioClip in AudioClips_onDeath ) {
            filePath = AssetDatabase.GetAssetPath(AudioClip).Substring(S_RESOURCE_DIR_LENGTH);
            filePath = filePath.Substring(0, filePath.Length - S_OGG_EXTENSION_LENGTH);
            activeEntity.soundClipFileNamesOnDeath.Add(filePath);
        }
    
        foreach( AudioClip AudioClip in AudioClips_onAggro ) {
            filePath = AssetDatabase.GetAssetPath(AudioClip).Substring(S_RESOURCE_DIR_LENGTH);
            filePath = filePath.Substring(0, filePath.Length - S_OGG_EXTENSION_LENGTH);
            activeEntity.soundClipFileNamesOnAggro.Add(filePath);
        }
    
        foreach( AudioClip AudioClip in AudioClips_onIdle ) {
            filePath = AssetDatabase.GetAssetPath(AudioClip).Substring(S_RESOURCE_DIR_LENGTH);
            filePath = filePath.Substring(0, filePath.Length - S_OGG_EXTENSION_LENGTH);
            activeEntity.soundClipFileNamesOnIdle.Add(filePath);
        }

        XMLUtility.Save<EntityReplacement>(activeEntity, S_ENTITIES_DIR, activeEntity.baseStats.Name);
    }
}

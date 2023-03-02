﻿using System.Collections.Generic;
using UnityEngine;

namespace AlwaysEast
{
    [RequireComponent(typeof(Animator))]
    public abstract class Entity : MonoBehaviour
    {

        public enum StatID
        {
            nothing = 0,
            Dmg_Phys_Min = 1,
            Dmg_Phys_Max = 2,
            Dmg_Phys_Percent = 3,
            Dmg_Ele_Fire = 4,
            Dmg_Ele_Cold = 5,
            Dmg_Ele_Lightning = 6,
            Dmg_Ele_Poison = 7,
            Def_Phys_Flat = 8,
            Def_Phys_Percent = 9,
            Def_Dmg_Reduction_Phys = 10,
            Def_Dmg_Reduction_Magic = 11,
            Def_Dmg_Reduction_All = 12,
            Def_Ele_Res_Fire = 13,
            Def_Ele_Res_Cold = 14,
            Def_Ele_Res_Lightning = 15,
            Def_Ele_Res_Poison = 16,
            Def_Ele_Res_All = 17,
            On_Hit_Life = 18,
            On_Kill_Life = 19,
            On_Kill_Mana = 20,
            On_Hit_Mana = 21,
            Plus_Life = 22,
            Plus_Mana = 23,
            Plus_Regen_Life = 24,
            Plus_Regen_Mana = 25,
            Plus_Str = 26,
            Plus_Dex = 27,
            Plus_Con = 28,
            Plus_Int = 29,
            Plus_Speed_Phys = 30,
            Plus_Speed_Magic = 31,
            Plus_Speed_Movement = 32,
            Plus_Block_Recovery = 33,
            Plus_Stagger_Recovery = 34,
            Plus_Magic_Find = 35,
            Plus_Item_Find = 36,
            Plus_Attack_Rating = 37,
            Plus_Defence_Rating = 38,
            Plus_Blockrate = 39
        }

        public const int BlockRecoveryBase = 5;
        public const int StaggerRecoveryBase = 1; // Stagger is not yet implemented.

        [SerializeField]
        public EntityReplacement _base { get; set; }

        public int IncMagicFind => stats[(StatID)Item.Suffix.SType.Plus_Magic_Find] + stats[(StatID)Item.Implicit.IType.Plus_Magic_Find] + stats[(StatID)Item.Prefix.PType.Plus_Magic_Find];
        public int Speed => _base.baseStats.Speed + (int)stats[(StatID)Item.Implicit.IType.Plus_Speed_Movement] + stats[(StatID)Item.Suffix.SType.Plus_Speed_Movement];
        public int DefDmgReductionPhys => stats[(StatID)Item.Prefix.PType.Def_Dmg_Reduction_Phys] + stats[(StatID)Item.Suffix.SType.Def_Dmg_Reduction_All] + stats[(StatID)Item.Implicit.IType.Def_Dmg_Reduction_All];
        public int DefDmgReductionMagic => stats[(StatID)Item.Prefix.PType.Def_Dmg_Reduction_Magic] + stats[(StatID)Item.Suffix.SType.Def_Dmg_Reduction_All] + stats[(StatID)Item.Implicit.IType.Def_Dmg_Reduction_All];
        public float Life_Max => _base.baseStats.LifeMax + Constitution * 3 + stats[(StatID)Item.Suffix.SType.Plus_Life] + _base.baseStats.Level * 2;
        public float Mana_Max => _base.baseStats.ManaMax + Mathf.Floor(Intelligence * 1.5f) + stats[(StatID)Item.Prefix.PType.Plus_Mana] + Mathf.Floor(_base.baseStats.Level * 1.5f);
        public int DmgPhysMin => _base.baseStats.DmgPhyMin + Strength / 10 + stats[(StatID)Item.Suffix.SType.Dmg_Phys_Min];
        public int DmgPhysMax => _base.baseStats.DmgPhyMax + Strength / 10 + stats[(StatID)Item.Suffix.SType.Dmg_Phys_Max];
        public float Defense => _base.baseStats.Defense + (Dexterity / 10) + stats[StatID.Def_Phys_Flat];
        public int DmgEleFireMin => stats[(StatID)Item.Prefix.PType.Dmg_Ele_Fire] + stats[(StatID)Item.Suffix.SType.Dmg_Ele_Fire];
        public int DmgEleFireMax => stats[(StatID)Item.Prefix.PType.Dmg_Ele_Fire] + stats[(StatID)Item.Suffix.SType.Dmg_Ele_Fire];
        public int DmgEleColdMin => stats[(StatID)Item.Prefix.PType.Dmg_Ele_Cold] + stats[(StatID)Item.Suffix.SType.Dmg_Ele_Cold];
        public int DmgEleColdMax => stats[(StatID)Item.Prefix.PType.Dmg_Ele_Cold] + stats[(StatID)Item.Suffix.SType.Dmg_Ele_Cold];
        public int DmgEleLightningMin => stats[(StatID)Item.Prefix.PType.Dmg_Ele_Lightning] + stats[(StatID)Item.Suffix.SType.Dmg_Ele_Lightning];
        public int DmgEleLightningMax => stats[(StatID)Item.Prefix.PType.Dmg_Ele_Lightning] + stats[(StatID)Item.Suffix.SType.Dmg_Ele_Lightning];
        public int DmgElePoisonMin => stats[(StatID)Item.Prefix.PType.Dmg_Ele_Poison] + stats[(StatID)Item.Suffix.SType.Dmg_Ele_Poison];
        public int DmgElePoisonMax => stats[(StatID)Item.Prefix.PType.Dmg_Ele_Poison] + stats[(StatID)Item.Suffix.SType.Dmg_Ele_Poison];
        public int DefResFire => stats[(StatID)Item.Prefix.PType.Def_Ele_Res_All] + stats[(StatID)Item.Prefix.PType.Def_Ele_Res_Fire];
        public int DefResCold => stats[(StatID)Item.Prefix.PType.Def_Ele_Res_All] + stats[(StatID)Item.Prefix.PType.Def_Ele_Res_Cold];
        public int DefResLightning => stats[(StatID)Item.Prefix.PType.Def_Ele_Res_All] + stats[(StatID)Item.Prefix.PType.Def_Ele_Res_Lightning];
        public int DefResPoison => stats[(StatID)Item.Prefix.PType.Def_Ele_Res_All] + stats[(StatID)Item.Prefix.PType.Def_Ele_Res_Poison];
        public int OnHitLife => stats[(StatID)Item.Prefix.PType.On_Hit_Life];
        public int OnKillLife => stats[(StatID)Item.Prefix.PType.On_Kill_Life];
        public int OnHitMana => stats[(StatID)Item.Suffix.SType.On_Hit_Mana];
        public int OnKillMana => stats[(StatID)Item.Suffix.SType.On_Kill_Mana];
        public int RegenLife => stats[(StatID)Item.Implicit.IType.Plus_Regen_Life] + stats[(StatID)Item.Suffix.SType.Plus_Regen_Life];
        public int RegenMana => stats[(StatID)Item.Implicit.IType.Plus_Regen_Mana] + stats[(StatID)Item.Prefix.PType.Plus_Regen_Mana];
        public int IncMoveSpeed => stats[(StatID)Item.Suffix.SType.Plus_Speed_Movement] + stats[(StatID)Item.Implicit.IType.Plus_Speed_Movement];
        public int IncBlockRecovery => stats[(StatID)Item.Suffix.SType.Plus_Block_Recovery] + stats[(StatID)Item.Implicit.IType.Plus_Block_Recovery];
        public int IncStaggerRecovery => stats[(StatID)Item.Suffix.SType.Plus_Stagger_Recovery] + stats[(StatID)Item.Implicit.IType.Plus_Stagger_Recovery];
        public float IncBlockRate => stats[(StatID)Item.Suffix.SType.Plus_Blockrate] + stats[(StatID)Item.Implicit.IType.Plus_Blockrate] + _base.baseStats.ChanceToBlock;
        public float IncAttackRating => Dexterity / 2 + stats[(StatID)Item.Prefix.PType.Plus_Attack_Rating] + _base.baseStats.AttackRating;
        public int Strength => stats[(StatID)Item.Suffix.SType.Plus_Str] + _base.baseStats.Strength;
        public int Dexterity => stats[(StatID)Item.Suffix.SType.Plus_Dex] + _base.baseStats.Dexterity;
        public int Constitution => stats[(StatID)Item.Suffix.SType.Plus_Con] + _base.baseStats.Constitution;
        public int Intelligence => stats[(StatID)Item.Suffix.SType.Plus_Int] + _base.baseStats.Intelligence;

        protected Dictionary<StatID, int> stats = new Dictionary<StatID, int>();
        protected SpriteRenderer spriteRenderer;

        public bool IsAggressive
        {
            get
            {
                int disX = Mathf.Abs(Entities.GetPCS._coordinates.x - _coordinates.x);
                int disY = Mathf.Abs(Entities.GetPCS._coordinates.y - _coordinates.y);
                int distance = disX + disY;

                return distance <= _base.baseStats.RangeOfAggression;
            }
        }

        public AudioClip onAttack, onHit, onMove, miss, block;

        protected List<Node> _chain = new List<Node>();

        public Vector3Int _coordinates;
        protected Animator _animator;
        protected int BlockRecoveryTurnsRemaining = 0;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            for (int i = 0; i < System.Enum.GetNames(typeof(StatID)).Length; i++)
            {
                stats.Add((StatID)i, 0);
            }
        }

        public virtual void Attack()
        {
            _animator.SetTrigger("Attack");
            AudioDevice.Play(onAttack);
        }

        public virtual void Move()
        {
        }

        public virtual void Interact()
        {
        }

        /// <summary>
        /// This method is used by the AI. Player actions are separate in PlayerCharacter.cs
        /// </summary>
        public virtual void Action()
        {
            if (_base.baseStats.Speed == 0)
            {
                Entities.Step(false);
                return;
            }

            Vector3Int playerCharacterCoordinates = Entities.GetPCS._coordinates;

            // some AI shit

            int disX = Mathf.Abs(playerCharacterCoordinates.x - _coordinates.x);
            int disY = Mathf.Abs(playerCharacterCoordinates.y - _coordinates.y);

            bool canAttack = disX + disY == 1;
            if (canAttack)
            {
                Attack();
                AttackSplash.Show(playerCharacterCoordinates, AttackSplash.Type.Pierce);
                Entities.Attack(playerCharacterCoordinates, Random.Range(_base.baseStats.DmgPhyMin, _base.baseStats.DmgPhyMax + 1), _base.baseStats.AttackRating, _base.baseStats.Level);
                return;
            }

            if (IsAggressive)
            {
                _chain = Pathfind.GetPath(_coordinates, playerCharacterCoordinates, false);

                if (IsNullOrDefault(_chain))
                {
                    Entities.Step(spriteRenderer.isVisible);
                    return;
                }
            }
            else _chain = Pathfind.Wander(_coordinates);
            
            if (spriteRenderer.isVisible)
                AudioDevice.Play(onMove);
        }

        public virtual void RecieveDamage(int incomingDamage, float attackerCombatRating, float attackerLevel) { }

        public virtual void PreTurn()
        {
            //recover from blocking
            if (BlockRecoveryTurnsRemaining > 0)
                BlockRecoveryTurnsRemaining--;
        }

        protected virtual void Die()
        {
            //TextLog.Print( string.Format("<color=#FF0000>{0}</color> is slain", Name ) );
            _animator.SetTrigger("Die");
            Pathfind.Unoccupy(_coordinates);
            Entities.Remove(this);
            TileMapCursor.Hide();
        }

        public void DestroyAfterDeathAnimation()
        {
            Destroy(gameObject);
        }

        public void AlertObservers(string message)
        {
            if (message.Equals("AttackAnimationEnd"))
            {
                Entities.Step(spriteRenderer.isVisible);
            }
        }

        protected void UpdateAnimator(Vector3Int dir)
        {
            if (dir != Vector3Int.zero)
            {
                transform.localScale = -dir.x > 0 ? new Vector3(1.0f, 1.0f) : new Vector3(-1.0f, 1.0f);
                _animator.SetBool("Moving", true);
            }
        }

        public void SetEntity(EntityReplacement replacement)
        {
            _base = replacement;

            _base.baseStats.LifeCurrent = (int)Life_Max;
            _base.baseStats.ManaCurrent = (int)Mana_Max;

            this._animator.runtimeAnimatorController = Resources.Load<AnimatorOverrideController>(replacement.animatorOverrideControllerFileName);
        }

        public static bool IsNullOrDefault<T>(T value)
        {
            return object.Equals(value, default(T));
        }
    }
}

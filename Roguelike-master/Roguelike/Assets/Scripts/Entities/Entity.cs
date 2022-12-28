﻿using UnityEngine;

[RequireComponent( typeof( Animator ) )]
public class Entity : MonoBehaviour {

    //Properties
    public string Name { get; protected set; }

    public int LootLevel { get; protected set; }

    protected int Speed { get; set; }
    protected int RangeOfAggression { get; set; }
    protected int Attack_Damage { get; set; }
    protected int Health_Current { get; set; }
    protected int Health_Maximum { get; set; }

    public Vector3Int _coordinates;
    protected Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    public virtual void Attack() => _animator.SetTrigger( "Attack" );

    public virtual void Action() {
    }

    public virtual void Move() {
    }

    public virtual void Interact() {
    }

    public void Teleport( Vector3Int coordinates ) {
        _coordinates = coordinates;
        //Teleport does not set the tilemap as occupied
        //Teleport is called prior to pathfinding nodes being created
        gameObject.transform.SetPositionAndRotation( coordinates + Vector3.up * 0.75f + Vector3.right * 0.5f, Quaternion.identity );
    }

    public virtual void DealDamage( int damage ) {
        Health_Current -= damage;
        if ( Health_Current <= 0 ) {
            Die();
        }
    }

    protected virtual void Die() {
        _animator.SetTrigger( "Die" );
        Pathfind.Unoccupy( _coordinates );
        Entities.Remove( this );
    }

    public void AlertObservers( string message ) {
        if ( message.Equals( "AttackAnimationEnd" ) ) {
            Entities.Step();
        }
    }

    protected void UpdateAnimator( Vector3Int dir ) {
        if ( dir != Vector3Int.zero ) {
            transform.localScale = -dir.x > 0 ? new Vector3( 1.0f, 1.0f ) : new Vector3( -1.0f, 1.0f );
            _animator.SetBool( "Moving", true );
        }
    }
}
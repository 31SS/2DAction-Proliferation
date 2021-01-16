using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterState;
using UniRx;
public class BaseCharacter : MonoBehaviour, ISteponable
{
    protected const int REVERSE = -1;
    protected const int NON_REVERSE = 1;
    
    //変更前のステート名
    protected string _prevStateName;

    //ステート
    public StateProcessor StateProcessor { get; set; } = new StateProcessor();
    public CharacterStateIdle StateIdle { get; set; } = new CharacterStateIdle();
    public CharacterStateRun StateRun { get; set; } = new CharacterStateRun();
    public CharacterStateAir StateAir { get; set; } = new CharacterStateAir();
    public CharacterStateAttack StateAttack { get; set; } = new CharacterStateAttack();


    [SerializeField] protected PlayerParameter playerParameter;
    protected static readonly float FloatingDistance = 0.01f;

    protected PlayerInput _playerInput;
    protected PlayerMover _playerMover;
    [SerializeField] protected Rigidbody2D m_rigidbody2D;
    [SerializeField] protected Animator m_animator;
    [SerializeField] protected bool m_isGround/* { get; set; }*/;
    [SerializeField] protected BoxCollider2D m_boxCollider2D;
    [SerializeField] protected LayerMask _layerMask;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected ContactFilter2D _filter2D;
    protected RaycastHit2D _hit;

    protected void FixedUpdate()
    {
        
    }

    public void StepedOn(UnityChan2DController player)
    {
        
    }
    
    protected void OnCollisionEnter2D(Collision2D other)
    {
        var stepedOnable = other.gameObject.GetComponent<ISteponable>();
    }

    public void Idle()
    {

    }
    public void Run()
    {
        
    }
    public void Air()
    {
        //_playerMover.Jump(m_animator, JUMP_POWER);
    }

    public void Attack()
    {

    }
}

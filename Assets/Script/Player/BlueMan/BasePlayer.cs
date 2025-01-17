﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterState;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity.Mathematics;

public class BasePlayer : MonoBehaviour
{
    protected const int REVERSE = -1;
    protected const int NON_REVERSE = 1;
    protected const float KNOCKBACK_POWER = 5f;
    protected const float ANGELRING_POS = 5f;
    protected const float LIMIT_Y_VEL = -15f;

    //変更前のステート名
    protected string _prevStateName;

    //ステート
    public StateProcessor StateProcessor { get; set; } = new StateProcessor();
    public CharacterStateIdle StateIdle { get; set; } = new CharacterStateIdle();
    public CharacterStateRun StateRun { get; set; } = new CharacterStateRun();
    public CharacterStateAir StateAir { get; set; } = new CharacterStateAir();
    public CharacterStateAttack StateAttack { get; set; } = new CharacterStateAttack();
    
    public CharacterStateDamage StateDamage { get; set; } = new CharacterStateDamage();


    [SerializeField] protected PlayerParameter playerParameter;
    protected static readonly float FloatingDistance = 0.01f;

    protected PlayerInput _playerInput;
    protected PlayerMover _playerMover;
    [SerializeField] protected Rigidbody2D m_rigidbody2D;
    [SerializeField] protected Animator m_animator;
    [SerializeField] protected SpriteRenderer m_spriteRenderer;
    [SerializeField] protected bool m_isGround/* { get; set; }*/;
    [SerializeField] protected ContactFilter2D _groundFilter2D;
    [SerializeField] protected ContactFilter2D _stepedOnFilter2D;
    [SerializeField] protected GameObject angelRing;
    [SerializeField] protected GameObject damageEffect;
    private bool isInvincible;
    private Vector2 minScreenEdge;
    private Vector2 maxScreenEdge;
    private float m_HalfWidth = 0.7f;

    private void Awake()
    {
        StateProcessor.State.Value = StateIdle;
        StateIdle.ExecAction = Idle;
        StateRun.ExecAction = Run;
        StateAir.ExecAction = Air;
        StateAttack.ExecAction = Attack;

        _playerInput = new PlayerInput();
        _playerMover = new PlayerMover(m_rigidbody2D);
    }
    protected void Start()
    {
        var cameraMain = Camera.main;
        minScreenEdge = cameraMain.ViewportToWorldPoint(Vector2.zero);
        maxScreenEdge = cameraMain.ViewportToWorldPoint(Vector2.one);
    }

    protected void FixedUpdate()
    {
        
    }

    protected virtual void Update()
    {
        var cameraMain = Camera.main;
        minScreenEdge = cameraMain.ViewportToWorldPoint(Vector2.zero);
        maxScreenEdge = cameraMain.ViewportToWorldPoint(Vector2.one);
        var position = transform.position;
        //画面(カメラから見える部分)から出ないように
        transform.position = 
            new Vector2(Mathf.Clamp(position.x,minScreenEdge.x + m_HalfWidth,maxScreenEdge.x - m_HalfWidth),
                Mathf.Clamp(position.y,minScreenEdge.y + m_HalfWidth,maxScreenEdge.y - m_HalfWidth));
        //落下時のコライダーすり抜け防止のためにY軸の速さに上限を設定
        if (m_rigidbody2D.velocity.y <= LIMIT_Y_VEL)
        {
            m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, LIMIT_Y_VEL);
        }
        if (GameManager.Instance.currentState == GameManager.GameState.Over)
        {
            DamageAct();
        }
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        var stepedOnable = other.gameObject.GetComponent<ISteponable>();
        var damageable = other.gameObject.GetComponent<IDamageable>();
        
        if (stepedOnable != null && m_rigidbody2D.IsTouching(_stepedOnFilter2D))
        {
            _playerMover.Jump(m_animator, playerParameter.HIGH_JUMP_POWER);
                stepedOnable.StepedOn();
        }
        if (damageable != null)
        {
            DamageAct();
        }
    }
    
    //SoilBlockや拾えるアイテムに触れた時の処理
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        // other.GetComponent<IBreakable>()?.Breaked();
        other.GetComponent<IPickupable>()?.PickedUp();
        var damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            DamageAct();
        }
    }

    private void DamageAct()
    {
        GameManager.Instance.dispatch(GameManager.GameState.Over);
        var position = transform.position;
        var rotation = transform.rotation;
        // Damage();
        // m_rigidbody2D.AddForce(playerParameter.KNOCKBACK_POWER);
        // m_rigidbody2D.velocity = new Vector2(transform.right.x * playerParameter.KNOCKBACK_POWER.x, transform.up.y * playerParameter.KNOCKBACK_POWER.y);
        // Instantiate(angelRing, transform.position, quaternion.identity);
        Instantiate(damageEffect, position, rotation);
        Instantiate(angelRing, position, rotation);
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
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

    public void Damage()
    {
        
    }
}

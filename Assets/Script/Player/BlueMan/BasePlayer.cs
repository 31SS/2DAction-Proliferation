using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterState;
using Cysharp.Threading.Tasks;
using UniRx;
public class BasePlayer : MonoBehaviour
{
    protected const int REVERSE = -1;
    protected const int NON_REVERSE = 1;
    protected const float KNOCKBACK_POWER = 5f;

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
    [SerializeField] protected SpriteRenderer m_spriteRenderer;
    [SerializeField] protected bool m_isGround/* { get; set; }*/;
    [SerializeField] protected ContactFilter2D _groundFilter2D;
    [SerializeField] protected ContactFilter2D _stepedOnFilter2D;
    private bool isInvincible;
    private Vector2 minScreenEdge;
    private Vector2 maxScreenEdge;
    private float m_HalfWidth = 0.7f;

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
        transform.position = new Vector2(Mathf.Clamp(position.x,minScreenEdge.x + m_HalfWidth,maxScreenEdge.x - m_HalfWidth),Mathf.Clamp(position.y,minScreenEdge.y + m_HalfWidth,maxScreenEdge.y - m_HalfWidth));
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        var stepedOnable = other.gameObject.GetComponent<ISteponable>();
        var damageable = other.gameObject.GetComponent<IDamageable>();
        
        if (stepedOnable != null)
        {
            if (m_rigidbody2D.IsTouching(_stepedOnFilter2D))
            {
                _playerMover.Jump(m_animator, playerParameter.JUMP_POWER);
                stepedOnable.StepedOn();
            }
        }

        if (damageable != null)
        {
            Damage();
            m_rigidbody2D.AddForce(playerParameter.KNOCKBACK_POWER);
            // m_rigidbody2D.velocity = new Vector2(transform.right.x * playerParameter.KNOCKBACK_POWER.x, transform.up.y * playerParameter.KNOCKBACK_POWER.y);
        }
    }
    
    //SoilBlockや拾えるアイテムに触れた時の処理
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<IBreakable>()?.Breaked();
        other.GetComponent<IPickupable>()?.PickedUp();
    }

    protected async void Damage()
    {
        //無敵中は処理しない
        if (isInvincible)
        {
            return;
        }

        isInvincible = true;
        //ノックバック
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        isInvincible = false;
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

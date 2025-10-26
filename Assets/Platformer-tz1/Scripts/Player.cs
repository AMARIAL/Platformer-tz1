using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player ST  {get; private set;}
    
    [SerializeField] private Collider2D bodyCollider;
    [SerializeField] private bool isAlive = true;
    [SerializeField] private bool isLookRight = true;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private bool isFalling;
    [SerializeField] private float jumpForce;
    public ParticleSystem particleJumpUpGround;
    private Inputs _inputs;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private GroundDetection _groundDetection;
    private Attack _attack;
    private Health _health;
    private Material matDefault;
    private Material matBlink;
    private void Awake ()
    {
        ST = this;
        _inputs = GetComponent<Inputs>();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _attack = GetComponent<Attack>();
        _groundDetection = GetComponentInChildren<GroundDetection>();
        _health = GetComponent<Health>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        matDefault = _spriteRenderer.material;
        matBlink = Resources.Load<Material>("Materials/matBlink");
    }

    private void Start()
    {
        _health.isDead += Death;
        _health.healthDamage += Damage;
    }
    
    private void FixedUpdate()
    {
        if(!isAlive) return;
        
        float mathfAbs = Mathf.Abs(_inputs.horizontalDirection);
        
        _animator.SetFloat("speed", mathfAbs);
        
        if(mathfAbs > 0.01f && !_attack.isAttack)
            Move(_inputs.horizontalDirection);
    }

    private void Update()
    {
        if(!isAlive) return;
        
        if (_animator.GetBool("isGrounded") != _groundDetection.IsGrounded)
        {
            _animator.SetBool("isGrounded", _groundDetection.IsGrounded);
            if (_groundDetection.IsGrounded)
            {
                particleJumpUpGround.Play();
                Audio.ST.PlaySound(Sound.landing);
            }
        }
            

        if (_inputs.isJumpPressed && _groundDetection.IsGrounded)
            Jump();
        
        if (_inputs.isFirePressed)
            Hit();

        if ((isJumping || isFalling) && _groundDetection.IsGrounded)
        {
            isJumping = isFalling = false;
            _animator.SetBool("isJumping", false);
        }
        else if (!isJumping && !_groundDetection.IsGrounded)
        {
            isFalling = true;
        }
            
    }

    private void Move(float dir)
    {
        _rigidbody2D.velocity = new Vector2(dir * moveSpeed, _rigidbody2D.velocity.y);
        if(dir > 0 && !isLookRight || dir < 0 && isLookRight)
            Flip();
    }

    private void Damage()
    {
        Audio.ST.PlaySound(Sound.ouch);
        _spriteRenderer.material = matBlink;
        Invoke("ResetMaterial",0.2f);
    }

    private void ResetMaterial()
    {
        _spriteRenderer.material = matDefault;
    }

    private void Death()
    {
        bodyCollider.enabled = false;
        Audio.ST.PlaySound(Sound.death);
        //_animator.SetTrigger("trDeath");
        GameManager.ST.PlayerDead();
        isAlive = false;
    }

    public void Restart()
    {
        isAlive = true;
        bodyCollider.enabled = true;
        _health.Resurrection();
        _animator.Play("Idle");
    }

    private void Jump()
    {
        Audio.ST.PlaySound(Sound.jump);
        _rigidbody2D.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
        isJumping = true;
        _animator.SetBool("isJumping", true);
    }
    private void Hit()
    {
        if(GameManager.ST.currentState != GameState.Game) return;
        _attack.StartAttack();
    }
    private void Flip ()
    {
        isLookRight = !isLookRight;
        transform.localScale = new Vector3(isLookRight ? 1 : -1, 1, 1);
    }
}

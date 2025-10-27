using System.Collections;
using UnityEngine;
public class PatrolEnemyLogic : MonoBehaviour, IEnemy

{
    enum State
    {
        Idle,
        Move,
        Attack,
        Dead
    }
    [SerializeField] public bool isLookRight = true;
    [SerializeField] private bool isCanMove;
    [SerializeField] private bool isAlive;
    [SerializeField] private float moveSpeed;
    [SerializeField] private State state;
    [SerializeField] private Collider2D hitCollider;
    
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Health _health;

    private void Awake ()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
    }
    private void Start ()
    {
        CoinsManager.ST.enemyCoinsContainer.Add(this, CoinsManager.ST.CreateCoin());
        isAlive = isCanMove = true;
        _health.isDead += Death;
        _health.healthDamage += Hit;
        StartCoroutine(Patrol());
    }
    private void Death()
    {
        isAlive = isCanMove = false;
        Audio.ST.PlaySound(Sound.skeletonDeath);
        _animator.SetTrigger("trDeath");
        state = State.Dead;
        hitCollider.enabled = false;
        CoinsManager.ST.ActivateCoin(this, transform);
        StopAllCoroutines();
    }
    private void Hit()
    {
        Audio.ST.PlaySound(Sound.hit);
        _animator.Play("Hit");
        isCanMove = false;
    }
    private void Move ()
    {
        if (isAlive)
        {
            if (isCanMove)
                _rigidbody2D.velocity = new Vector2(moveSpeed * (isLookRight ? 1 : -1) , _rigidbody2D.velocity.y);
        }
        else
        {
            state = State.Idle;
        }
        
        _animator.SetFloat("Speed", Mathf.Abs(_rigidbody2D.velocity.x));

    }
    private void Update ()
    {
        _animator.SetFloat("Speed", 0);
        
        if(state == State.Move)
            Move();
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Limiter"))
        {
            Flip();
        }
    }
    public void DoAttack ()
    {
        if(!isAlive || !isCanMove) return;
        
        state = State.Attack;
        
        float px = Player.ST.transform.position.x;
        float x = transform.position.x;
        if(px < x &&  isLookRight  || px > x && !isLookRight)
            Flip();
            
        if(!_animator.GetBool("isAttack"))
            _animator.SetBool("isAttack", true);
    }

    public void EventHit()
    {
        if (!hitCollider.enabled)
        {
            hitCollider.enabled = true;
            Audio.ST.PlaySound(Sound.attack);
        }
        else
        {
            state = State.Move;
            hitCollider.enabled = false;
            _animator.SetBool("isAttack", false);
        }

    }

    private IEnumerator Patrol ()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f);
            if (state != State.Attack)
            {
                if (Random.Range(0, 10) < 8)
                    state = State.Move;
                else
                    state = State.Idle;
            }
        }
        yield return null;
    }
    public void CanMove()
    {
        isCanMove = true;
    }
    public void CantMove()
    {
        isCanMove = false;
    }
    private void Flip ()
    {
        isLookRight = !isLookRight;
        transform.localScale = new Vector3(isLookRight ? 1 : -1, 1, 1);
    }
}

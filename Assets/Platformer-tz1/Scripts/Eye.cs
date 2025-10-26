using System.Collections;
using UnityEngine;

public class Eye : MonoBehaviour, IEnemy
{
    [SerializeField] private bool isLookRight = true;
    [SerializeField] private bool isAlive;
    [SerializeField] private float speed;
    private Vector3 currentTarget;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    
    [SerializeField] private EyeBullet eyeBullet;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Health _health;
    
    private void Awake ()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
    }

    private void Start()
    {
        CoinsManager.ST.enemyCoinsContainer.Add(this, CoinsManager.ST.CreateCoin());
        isAlive = true;
        _health.isDead += Death;
        _health.healthDamage += Hit;
        currentTarget = endPoint.position;
        eyeBullet.gameObject.SetActive(false);
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        if(isAlive)
            Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed*Time.deltaTime);
        
        if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
        {
            currentTarget = (currentTarget == endPoint.position) ? startPoint.position : endPoint.position;
        }
        
        float px = Player.ST.transform.position.x;
        float x = transform.position.x;
        if(px < x &&  isLookRight  || px > x && !isLookRight)
            Flip();
    }

    private void Death()
    {
        isAlive = false;
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        Audio.ST.PlaySound(Sound.skeletonDeath);
        CoinsManager.ST.ActivateCoin(this, transform);
        StopAllCoroutines();
    }
    private void Hit()
    {
        Audio.ST.PlaySound(Sound.hit);
        _animator.SetTrigger("trHit");
    }

    public void DoAttack()
    {}
    private void Flip ()
    {
        isLookRight = !isLookRight;
        transform.localScale = new Vector3(isLookRight ? 1 : -1, 1, 1);
    }

    private IEnumerator Shoot()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(2.0f);
            _animator.SetTrigger("trAttack");
            yield return null;  
        }
    }

    public void EventAttack()
    {
        eyeBullet.Shoot(transform.position);
    }
}

using System.Collections;
using UnityEngine;
using Random = System.Random;

    public class Wolf : MonoBehaviour, IEnemy
    {
        [SerializeField] private bool isAlive = true;
        [SerializeField] private bool isCanMove = true;
        [SerializeField] private int speed;
        [SerializeField] private int walkSpeed = 2;
        [SerializeField] private int runSpeed = 10;
        [SerializeField] private bool isLookRight = false;

        [SerializeField] private float viewingRange = 5f;
        [SerializeField] private float flipRange = 0.20f;
        [SerializeField] private float goundCheckerRange = 0.5f;
        [SerializeField] private float biteDistance = 1.6f;
        [SerializeField] private int attackForce = 10;

        private float delta;
        private RaycastHit2D hit;
        private Vector2 radar;

        [SerializeField] private LayerMask layerMaskGround;
        [SerializeField] private LayerMask layerMaskTarget;
        private GroundDetection groundDetection;
        private Animator animator;
        private Rigidbody2D rigidbody;

        private float startPositionX;
        private Random rand;

        private enum State
        {
            Idle,
            Walk,
            Run,
            Attack,
            Dead
        };

        [SerializeField] private State state;
        [SerializeField] private float distance;


        private void Awake()
        {
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody2D>();
            groundDetection = GetComponent<GroundDetection>();
            delta = GetComponent<BoxCollider2D>().size.x / 2 - GetComponent<BoxCollider2D>().offset.x + 0.01f;
        }

        void Start()
        {
            CoinsManager.ST.enemyCoinsContainer.Add(this, CoinsManager.ST.CreateCoin());
            state = State.Walk;
            speed = walkSpeed;
            StartCoroutine(Patrol());
        }

        private IEnumerator Patrol ()
        {
            while (true)
            {
                yield return new WaitForSeconds(3.0f);
                if (state != State.Attack)
                {
                    if (UnityEngine.Random.Range(0, 10) < 8)
                        state = State.Walk;
                    else
                        state = State.Idle;
                }
            }
            yield return null;
        }
        
        private void FixedUpdate()
        {
            Move();
            if (state != State.Attack)
                Raycast();
        }

        private void Flip()
        {
            isLookRight = !isLookRight;
            transform.localScale = new Vector3(isLookRight ? -1 : 1, 1, 1);
        }
        
        private void Move()
        {
            rigidbody.velocity = new Vector2(isLookRight ? 1 : -1, rigidbody.velocity.y) * speed;

            if (animator.GetFloat("Speed") != speed)
                animator.SetFloat("Speed", speed);
        }
        private void Death()
        {
            isAlive = isCanMove = false;
            Audio.ST.PlaySound(Sound.wolfDeath);
            animator.SetTrigger("trDeath");
            state = State.Dead;
            StopAllCoroutines();
        }
        private void Raycast()
        {

            radar.Set(transform.position.x + delta * (isLookRight ? 1 : -1), transform.position.y - 0.1f);
            
            hit = Physics2D.Raycast(radar, isLookRight ? Vector2.right : Vector2.left, flipRange, layerMaskGround);
            Debug.DrawRay(new Vector2(radar.x, radar.y + 0.1f), isLookRight ? Vector2.right*flipRange : Vector2.left*flipRange, Color.yellow);
            if (hit.collider && hit.collider.tag == "Ground")
            {
                Flip();
                return;
            }
            
            hit = Physics2D.Raycast(radar, Vector2.down, goundCheckerRange, layerMaskGround);
            Debug.DrawRay(radar, Vector2.down, Color.green);
            if (!hit.collider)
            {
                Flip();
                return;
            }
            
            hit = Physics2D.Raycast(radar, isLookRight ? Vector2.right : Vector2.left, viewingRange,layerMaskTarget);
            Debug.DrawRay(radar, isLookRight ? Vector2.right*viewingRange : Vector2.left*viewingRange, Color.magenta);
            if (hit.collider && hit.collider.tag == "Player")
            {
                if (state != State.Run)
                {
                    ChangeState(State.Run);
                    return;
                }
                    
            }
            else if (state != State.Walk)
            {
                ChangeState(State.Walk);
            }

            radar.Set(transform.position.x + delta * (isLookRight ? -1 : 1), transform.position.y - 0.1f);
            hit = Physics2D.Raycast(radar, isLookRight ? Vector2.left : Vector2.right, viewingRange);
            Debug.DrawRay(radar, isLookRight ? Vector2.left*viewingRange : Vector2.right*viewingRange, Color.blue);
            if (hit.collider && hit.collider.tag == "Player")
            {
                Flip();
            }

        }

        private void Biting()
        {
            distance = Vector3.Distance(Player.ST.transform.position, transform.position);

            if (distance < biteDistance)
            {
                speed = 0;
                state = State.Attack;
                transform.localScale = new Vector3(Player.ST.transform.position.x < transform.position.x ? 1:-1, 1, 1);
               
            }
            else if (state == State.Attack)
            {
                speed = walkSpeed;
                state = State.Idle;
                transform.localScale = new Vector3(isLookRight? -1:1, 1, 1);
            }

        }

        private void ChangeState(State newstate)
        {
            if (state != newstate)
            {
                state = newstate;
                animator.SetBool("Bite", false);
                switch (state)
                {
                    case State.Idle:
                        speed = 0;
                        break;
                    case State.Walk:
                        speed = walkSpeed;
                        break;
                    case State.Run:
                        speed = runSpeed;
                        break;
                    case State.Attack:
                        animator.SetBool("Bite", true);
                        speed = 0;
                        break;
                }
            }

        }

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (collider2D.gameObject.CompareTag("Player"))
            {
                if (state != State.Attack)
                    ChangeState(State.Attack);
            }
        }

        private void OnTriggerExit2D(Collider2D collider2D)
        {
            if (collider2D.gameObject.CompareTag("Player"))
            {
                if (state != State.Walk)
                    ChangeState(State.Walk);
            }
        }

        public void DoAttack()
        {
            throw new System.NotImplementedException();
        }
    }

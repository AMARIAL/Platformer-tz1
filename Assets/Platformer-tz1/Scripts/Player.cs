using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player ST  {get; private set;}
    
    [SerializeField] private bool isLookRight;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isFalling;
    [SerializeField] private float jumpForce;
    
    private Inputs _inputs;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private GroundDetection _groundDetection;
    
    void Awake ()
    {
        ST = this;
        _inputs = GetComponent<Inputs>();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _groundDetection = GetComponentInChildren<GroundDetection>();
    }

    
    private void FixedUpdate()
    {
        if(Mathf.Abs(_inputs.horizontalDirection) > 0.01f)
            Move(_inputs.horizontalDirection);
    }

    private void Update()
    {
        if (_animator.GetBool("isGrounded") != _groundDetection.IsGrounded)
            _animator.SetBool("isGrounded", _groundDetection.IsGrounded);

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
        
        _animator.SetFloat("speed", Mathf.Abs(dir));
        
        if(dir > 0 && !isLookRight || dir < 0 && isLookRight)
            Flip();
    }
    private void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
        isJumping = true;
        _animator.SetBool("isJumping", true);
    }
    private void Hit()
    {
        
    }
    private void Flip ()
    {
        isLookRight = !isLookRight;
        transform.localScale = new Vector3(isLookRight ? 1 : -1, 1, 1);
    }
}

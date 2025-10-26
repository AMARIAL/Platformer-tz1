using UnityEngine;

public class EyeBullet : MonoBehaviour
{
    [SerializeField] private float force = 5;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Vector2 startPos;
    private bool isBoom;
    
    private void Awake()
    {
        startPos = transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    public void Shoot(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
        isBoom = false;
        float px = Player.ST.transform.position.x;
        float x = transform.position.x;
        _rigidbody2D.AddForce(px > x ? Vector2.right : Vector2.left *force,ForceMode2D.Impulse);
        Invoke("ToStart", 1.0f);
    }

    private void ToStart()
    {
        gameObject.SetActive(false);
        transform.position = startPos;
    } 
    
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (!isBoom && (other.CompareTag("Player") || other.CompareTag("Ground")))
        {
            isBoom = true;
            _animator.SetTrigger("trBoom");
        }
    }
}

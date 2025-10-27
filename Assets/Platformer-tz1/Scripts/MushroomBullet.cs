using UnityEngine;

public class MushroomBullet : MonoBehaviour
{
    [SerializeField] private float force = 5;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Shoot()
    {
        gameObject.SetActive(true);
        float px = Player.ST.transform.position.x;
        float x = transform.position.x;
        _rigidbody2D.AddForce(px > x ? Vector2.right : Vector2.left *force,ForceMode2D.Impulse);
        Invoke("ToStart", 1.0f);
    }

    private void ToStart()
    {
        gameObject.SetActive(false);
        transform.position = transform.parent.position;
    }
}

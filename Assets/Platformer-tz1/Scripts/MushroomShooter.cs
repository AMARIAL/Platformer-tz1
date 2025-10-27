using UnityEngine;

public class MushroomShooter : MonoBehaviour
{
    [SerializeField] private MushroomBullet mushroomBullet;
    private PatrolEnemyLogic mushroom;
    private Animator animator;
    private RaycastHit2D hit;
    private Vector2 radar;
    [SerializeField] private float viewingRange = 3f;
    [SerializeField] private LayerMask layerMaskTarget;

    private void Awake()
    {
        mushroom = GetComponent<PatrolEnemyLogic>();
        animator = GetComponent<Animator>();
        mushroomBullet.gameObject.SetActive(false);
    }

    public void EventAttack()
    {
        mushroomBullet.Shoot();
    }

    private void Update()
    {
        radar.Set(transform.position.x, transform.position.y + 0.1f);
        
        hit = Physics2D.Raycast(radar, mushroom.isLookRight ? Vector2.right : Vector2.left, viewingRange,layerMaskTarget);
        Debug.DrawRay(radar, mushroom.isLookRight ? Vector2.right*viewingRange : Vector2.left*viewingRange, Color.magenta);
        
        if (hit.collider)
        {
            animator.SetTrigger("trShoot");
        }
    }

}

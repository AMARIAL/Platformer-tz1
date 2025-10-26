using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask playerLayer;
    
    private bool isActive = true;
    private IEnemy enemy;
    
    private void Start()
    {
        enemy = GetComponentInParent<IEnemy>();
    }
    private void Update()
    {
        if (isActive && Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, playerLayer))
        {
            enemy.DoAttack();
            isActive = false;
            Invoke("Activate", 1.0f);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up*castDistance, boxSize);
    }

    private void Activate()
    {
        isActive = true;
    }

}

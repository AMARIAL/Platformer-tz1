using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask playerLayer;
    
    private Skeleton skeleton;
    
    private void Start()
    {
        skeleton = GetComponentInParent<Skeleton>();
    }
    private void Update()
    {

            
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up*castDistance, boxSize);
    }
    
}

using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask groundLayer;

    public bool IsGrounded
    {
        get {        
            if (Physics2D.BoxCast(transform.position, boxSize, 0 , -transform.up, castDistance, groundLayer))
                return true;
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up*castDistance, boxSize);
    }
}

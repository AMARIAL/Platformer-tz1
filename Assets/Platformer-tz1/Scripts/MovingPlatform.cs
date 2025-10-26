using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float speed = 1.0f;

    private Transform playerParent;
    private Vector3 currentTarget;

    private void Start()
    {
        currentTarget = endPoint.position;
        playerParent = Player.ST.transform.parent;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
        {
            currentTarget = (currentTarget == endPoint.position) ? startPoint.position : endPoint.position;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
            col.transform.parent = transform;

    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
            col.transform.parent = playerParent;
    }
}
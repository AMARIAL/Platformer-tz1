using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private GameObject glow;
    
    private Vector2 pos;
    
    private void Start()
    {
        Containers.ST.checkPointContainer.Add(gameObject,this);
        isActive = false;
        pos = transform.position;
        glow.SetActive(false);
    }
    
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.ST.NewCheckPoint(pos);
            isActive = true;
            glow.SetActive(true);
        }
    }
}

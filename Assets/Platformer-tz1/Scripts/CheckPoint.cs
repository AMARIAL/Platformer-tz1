using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private GameObject glow;
    
    private Vector2 pos;
    
    private void Start()
    {
        isActive = true;
        glow.SetActive(false);
    }
    
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            if (GameManager.ST.checkPoint)
            {
                GameManager.ST.checkPoint.isActive = true;
                GameManager.ST.checkPoint.glow.SetActive(false);
            }
            GameManager.ST.NewCheckPoint(this);
            Audio.ST.PlaySound(Sound.savepoint);
            isActive = false;
            glow.SetActive(true);
        }
    }
}

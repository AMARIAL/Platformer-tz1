using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private GameObject bouquet;
    
    private Vector2 pos;
    
    private void Start()
    {
        isActive = true;
        bouquet.SetActive(false);
    }
    
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            if (GameManager.ST.coins == CoinsManager.ST.allCoinsContainer.Count)
            {
                Menu.ST.OpenWin();
                isActive = false;
                bouquet.SetActive(true);
            }
            else
            {
                Debug.Log("Монет осталось на уровне: "+ (CoinsManager.ST.allCoinsContainer.Count - GameManager.ST.coins));
            }
        }
    }
}

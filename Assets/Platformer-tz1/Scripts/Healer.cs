using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] private int health;
    
    private void Start()
    {
        Containers.ST.healerContainer.Add(gameObject,this);
    }
    
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Containers.ST.healthContainer[other.transform.parent.gameObject].DoHeal(health);
            gameObject.SetActive(false);
        }
    }
}

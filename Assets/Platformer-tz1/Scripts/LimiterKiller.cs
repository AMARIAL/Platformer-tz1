using UnityEngine;

public class LimiterKiller : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D other)
    {
        GameManager.ST.healthContainer[other.transform.parent.gameObject].TakeDamage(9999);
    }
}

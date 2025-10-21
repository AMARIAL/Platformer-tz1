using UnityEngine;

public class LimiterKiller : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D other)
    {
        //Containers.ST.healthContainer[other.transform.parent.gameObject].TakeDamage(1000);
    }
}

using UnityEngine;

public class Chest : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }
}

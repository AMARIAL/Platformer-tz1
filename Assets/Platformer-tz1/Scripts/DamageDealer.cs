using UnityEngine;

public enum OtherHealthOwner: byte
{
    Enemy,
    Player
}

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private OtherHealthOwner otherHealthOwner;

    private void OnTriggerStay2D (Collider2D other)
    {
        if (other.CompareTag(otherHealthOwner.ToString()))
        {
            GameManager.ST.healthContainer[other.transform.parent.gameObject].TakeDamage(damage);
        }
    }
}

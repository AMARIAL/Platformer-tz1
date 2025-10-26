using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] private int health;
    private bool isActive = true;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            GameManager.ST.healthContainer[other.transform.parent.gameObject].DoHeal(health);
            Audio.ST.PlaySound(Sound.apple);
            isActive = false;
            _animator.Play("Item-destroy");
            
        }
    }
    public void DoDestroy()
    {
        gameObject.SetActive(false);
    }
}

using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool isActive = true;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        if(!CoinsManager.ST.allCoinsContainer.Contains(transform.parent.gameObject))
            CoinsManager.ST.allCoinsContainer.Add(transform.parent.gameObject);
        GameManager.ST.CoinsChanged(true);
    }
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            GameManager.ST.CoinsChanged(false);
            Audio.ST.PlaySound(Sound.coin);
            isActive = false;
            _animator.Play("Coin-Destroy");
            
        }
    }
    public void DoDestroy()
    {
        gameObject.SetActive(false);
    }
}
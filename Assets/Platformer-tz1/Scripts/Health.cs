using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action healthChanged;
    public event Action healthDamage;
    public event Action isDead;
    
    [SerializeField] private bool isActive = true;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        GameManager.ST.healthContainer.Add(gameObject,this);
        Resurrection();
    }
    public void TakeDamage(int dmg)
    {
        if(!isActive) 
            return;
        currentHealth -= dmg;
        isActive = false;
        healthDamage?.Invoke();
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animator.Play("Death");
            isDead?.Invoke();
        }
        else
        {
            animator.Play("Hit");
            StartCoroutine(HitCoolDown());
        }
    }
    public void DoHeal(int hp)
    {
        currentHealth += hp;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        healthChanged?.Invoke();
    }
    public void Resurrection()
    {
        isActive = true;
        DoHeal(maxHealth);
    }

    public int CurrentHealth
    {
        get => currentHealth;
    }
    
    private IEnumerator HitCoolDown ()
    {
        isActive = false;
        yield return new WaitForSeconds(1.0f);
        isActive = true;
        yield return null;
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        Containers.ST.healthContainer.Add(gameObject,this);
        Resurrection();
    }
    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth < 0)
            currentHealth = 0;
        animator.Play("Hit");
    }
    public void DoHeal(int hp)
    {
        currentHealth += hp;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
    public void Resurrection()
    {
        currentHealth = maxHealth;
    }
}
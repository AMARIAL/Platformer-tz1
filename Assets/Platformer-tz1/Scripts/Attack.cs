using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Collider2D hitCollider;
    public bool isAttack;
        
    private Animator _animator;
    
    private byte attackNum = 1;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        hitCollider.enabled = false;
    }
    
    public void StartAttack()
    {
        if(isAttack) return;
        
        Audio.ST.PlaySound(Sound.attack);
        _animator.Play("Knight-Attack " + attackNum);
        
        if (attackNum == 3)
            attackNum = 1;
        else
            attackNum++;
        
        isAttack = true;
        hitCollider.enabled = true;
        
        StartCoroutine(AttackReset());
    }
    
    public void StopAttack ()
    {
        isAttack = false;
        hitCollider.enabled = false;
    }
    
    private IEnumerator AttackReset ()
    {
        yield return new WaitForSeconds(3.0f);
        StopAttack();
        yield return null;
    }
    
}
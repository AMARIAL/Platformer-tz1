using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HitpointsUI : MonoBehaviour
{
    private Health playerHealth;
    [SerializeField] private Image hрIndiсator;
    private TextMeshProUGUI lives;

    private bool isHpAnim = false;
    
    private void Start()
    {
        if (Player.ST)
        {
            playerHealth = Player.ST.GetComponent<Health>();
            playerHealth.healthChanged += ChangeHp;
            playerHealth.healthDamage += ChangeHp;
            lives = GetComponentInChildren<TextMeshProUGUI>();
            GameManager.ST.livesChanged += ChangeLives;
            ChangeHp();
            ChangeLives();
        }
        else
        {
            Destroy(transform.parent.gameObject);
        }
    }

    private void ChangeHp()
    {
        if (isHpAnim)
        {
            isHpAnim = false;
            StopAllCoroutines();
        }
        
        StartCoroutine(HpAnim());
    }
    private void ChangeLives()
    {
        lives.text = GameManager.ST.lives.ToString();
    }
    private IEnumerator HpAnim()
    {
        isHpAnim = true;
        int i = playerHealth.CurrentHealth > Mathf.RoundToInt(hрIndiсator.fillAmount * 100) ? 1 : -1;
        
        while (playerHealth.CurrentHealth != Mathf.RoundToInt(hрIndiсator.fillAmount*100))
        {
            yield return new WaitForSeconds(0.01f);
            hрIndiсator.fillAmount += 0.01f * i;
        }
        hрIndiсator.fillAmount = (float)playerHealth.CurrentHealth / 100;
        isHpAnim = false;
        yield return null;
    }


}

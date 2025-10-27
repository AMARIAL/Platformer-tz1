using TMPro;
using UnityEngine;

public class CoinsCountUI : MonoBehaviour
{
    private TextMeshProUGUI count;

    private void Awake()
    {
        count = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.ST.coinsChanged += CounterUpdate;
    }

    private void CounterUpdate()
    {
        count.text = "x " + GameManager.ST.coins + " // "+CoinsManager.ST.allCoinsContainer.Count;
    }

}

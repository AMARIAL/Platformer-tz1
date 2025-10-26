using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    public Dictionary<IEnemy, GameObject> enemyCoinsContainer = new Dictionary<IEnemy, GameObject>();
    public List<GameObject> allCoinsContainer = new List<GameObject>();
    public static CoinsManager ST  {get; private set;}
    
    private void Awake()
    {
        if (ST)
            Destroy(gameObject);
        else
            ST = this;
    }
    public GameObject CreateCoin()
    {
         GameObject coin = Instantiate(coinPrefab, transform);
         allCoinsContainer.Add(coin.gameObject);
         coin.SetActive(false);
         return coin;
    }
    public void ActivateCoin(IEnemy enemy, Transform tf)
    {
        enemyCoinsContainer[enemy].SetActive(true);
        enemyCoinsContainer[enemy].transform.position = tf.position + Vector3.up * 0.2f;
    }
}

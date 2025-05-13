using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Spawner : MonoBehaviour
{
    public GameObject coinSpawner;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < coinSpawner.transform.childCount; i++)
        {
            coinSpawner.transform.GetChild(i).gameObject.SetActive(false);
        }
        StartCoroutine(CoinSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CoinSpawn()
    {

        yield return new WaitForSeconds(5);

        for (int i = 0; i < coinSpawner.transform.childCount; i++)
        {
            coinSpawner.transform.GetChild(i).gameObject.SetActive(false);
        }
        SpawnRandomCoin();



    }
    public void SpawnRandomCoin()
    {
        int numCoins = Random.Range(coinSpawner.transform.childCount / 4, coinSpawner.transform.childCount);
        for (int i = 0; i < numCoins; i++)
        {
            coinSpawner.transform.GetChild(Random.Range(0, coinSpawner.transform.childCount)).gameObject.SetActive(true);
        }
        StartCoroutine(CoinSpawn());
    }
}

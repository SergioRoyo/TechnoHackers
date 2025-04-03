using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRASS_SPAWNER : MonoBehaviour
{
    public GameObject grassSpawner;
    public int randomSpawn;
   
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < grassSpawner.transform.childCount; i++)
        {
            grassSpawner.transform.GetChild(i).gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            for (int i = 0; i < grassSpawner.transform.childCount; i++)
            {
                grassSpawner.transform.GetChild(i).gameObject.SetActive(false);
            }
            SpawnRandomGrass();
        }
       
    }
    public void SpawnRandomGrass()
    {
        int numOfGrass = Random.Range(grassSpawner.transform.childCount / 4, grassSpawner.transform.childCount / 2);
        for (int i = 0; i < numOfGrass; i++)
        {
            grassSpawner.transform.GetChild(Random.Range(0, grassSpawner.transform.childCount)).gameObject.SetActive(true);
        }
    }
    
    
}

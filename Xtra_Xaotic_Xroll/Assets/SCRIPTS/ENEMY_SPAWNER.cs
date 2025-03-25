using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_SPAWNER : MonoBehaviour
{
    public GameObject enemyprefab;
    public float generationTime = 5f;
    public GameObject allyBase;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GenerateEnemy", 0f, generationTime);
    }

    // Update is called once per frame
    void Update()
    {
            
    }
    public void GenerateEnemy()
    {
        // Generar el enemigo en la posici�n del spawner
        Instantiate(enemyprefab, transform.position, Quaternion.identity);       
    }
}

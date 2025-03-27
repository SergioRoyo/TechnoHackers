using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_SPAWNER : MonoBehaviour
{
    public GameObject enemyprefab;
    public GameObject tankEnemyprefab;
    public float generationTime = 5f;
    public int waveRound;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GenerateEnemy", 0f, generationTime);
        InvokeRepeating("GenerateTank", 2f, generationTime);
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    public void GenerateEnemy()
    {
        // Generar el enemigo en la posición del spawner
        Instantiate(enemyprefab, transform.position, Quaternion.identity);
    }
    public void GenerateTank()
    {
        Instantiate(tankEnemyprefab, transform.position, Quaternion.identity);
    }

}

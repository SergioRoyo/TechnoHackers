using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_SPAWNER : MonoBehaviour
{
    public GameObject enemyprefab;
    public float generationTime = 5f;
    public GameObject background_e;
    public float enemybaseHealth;
    // Start is called before the first frame update
    void Start()
    {
        enemybaseHealth = 50;
        InvokeRepeating("GenerateEnemy", 0f, generationTime);
    }
    public void GetDamage()
    {
        enemybaseHealth -= 5;
    }
    public void GenerateEnemy()
    {
        // Generar el enemigo en la posición del spawner
        Instantiate(enemyprefab, transform.position, Quaternion.identity);
    }
    

}

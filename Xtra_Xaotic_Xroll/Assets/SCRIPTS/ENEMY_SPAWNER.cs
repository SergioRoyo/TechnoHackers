using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_SPAWNER : MonoBehaviour
{
    public GameObject enemyprefab;
    public GameObject tankEnemyprefab;
    public float generationTime = 5f;
    public int waveRound;
    public List<GameObject> enemies = new();
    PLAYER_MOVEMENT player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PLAYER_MOVEMENT>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void NightEnemySpawn()
    {
        InvokeRepeating("GenerateEnemy", 0f, generationTime);
        InvokeRepeating("GenerateTank", 2f, generationTime);
    }
    public void GenerateEnemy()
    {
        // Generar el enemigo en la posición del spawner
        GameObject enemyGenerated = Instantiate(enemyprefab, transform.position, Quaternion.identity);
        enemies.Add(enemyGenerated);
    }
    public void GenerateTank()
    {
        GameObject tankGenerated = Instantiate(tankEnemyprefab, transform.position, Quaternion.identity);
        enemies.Add(tankGenerated);
    }

    public void StopEnemySpawner()
    {
        CancelInvoke("GenerateEnemy");
        CancelInvoke("GenerateTank");
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        enemies.Clear();
    }

}

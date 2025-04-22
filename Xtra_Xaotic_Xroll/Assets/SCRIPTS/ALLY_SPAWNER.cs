using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALLY_SPAWNER : MonoBehaviour
{
    public GameObject allyprefab;
    public GameObject background_a;
    public float allybaseHealth;
    // Start is called before the first frame update
    void Start()
    {
        allybaseHealth = 50;
    }
    public void GetDamage()
    {
        allybaseHealth -= 5;
    }
    public void GenerateAlly()
    {
        // Generar el aliado en la posicion del spawner
        Instantiate(allyprefab, transform.position, Quaternion.identity);
    }
}
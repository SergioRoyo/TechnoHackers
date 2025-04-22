using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALLY_SPAWNER : MonoBehaviour
{
    public GameObject allyprefab;
    public GameObject background_a;
    public float allybaseHealth;
   
    PLAYER_MOVEMENT pm;
    // Start is called before the first frame update
    void Start()
    {
        pm = FindAnyObjectByType<PLAYER_MOVEMENT>();
        allybaseHealth = 50;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GenerateAlly()
    {
        if (pm._dropCount >= 20)
        {
            Instantiate(allyprefab, transform.position, Quaternion.identity); // Generar el enemigo en la posición del spawner
            pm._dropCount -= 20;
        }
    }
    
    
    public void GetDamage()
    {

        allybaseHealth -= 5;
    }
    
}

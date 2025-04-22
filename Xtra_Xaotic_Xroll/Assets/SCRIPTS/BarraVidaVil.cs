using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVidaVil : MonoBehaviour
{
    public Image rellenoBarraVidaVil;
    private ENEMY_SPAWNER ENEMY_SPAWNER;
    private float vidaMaxima;
    // Start is called before the first frame update
    void Start()
    {
        ENEMY_SPAWNER = GameObject.Find("ENEMY BASE").GetComponent<ENEMY_SPAWNER>();
        vidaMaxima = ENEMY_SPAWNER.enemybaseHealth;
    }

    // Update is called once per frame
    void Update()
    {
       rellenoBarraVidaVil.fillAmount = ENEMY_SPAWNER.enemybaseHealth / vidaMaxima;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class BarraVida : MonoBehaviour
{
    public Image rellenoBarraVida;
    private ALLY_SPAWNER ALLY_SPAWNER;
    private float vidaMaxima;
    // Start is called before the first frame update
    void Start()
    {
        ALLY_SPAWNER = GameObject.Find("ALLY BASE").GetComponent<ALLY_SPAWNER>();
        vidaMaxima = ALLY_SPAWNER.allybaseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        rellenoBarraVida.fillAmount = ALLY_SPAWNER.allybaseHealth / vidaMaxima;
    }
}

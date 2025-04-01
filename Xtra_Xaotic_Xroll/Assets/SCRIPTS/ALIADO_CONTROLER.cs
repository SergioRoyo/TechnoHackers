using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ALIADO_CONTROLER : MonoBehaviour
{
    public int vidaAliado1 = 10;
    public int damageAliado1 = 10;
    public Slider barraVidaAliado1;
    public GameObject drop;
    public MINION_CONTROLLER minion_controller;
    // Start is called before the first frame update
    void Start()
    {
        barraVidaAliado1.value = vidaAliado1;
    }

    // Update is called once per frame
    void Update()
    {
        if (vidaAliado1 <= 0)
        {

            StartCoroutine(Dropear());
        }

        barraVidaAliado1.value = vidaAliado1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ENEMY") || collision.gameObject.CompareTag("TORRE"))
        {
            //minion_controller.minionHealth -= 2;
            MINION_CONTROLLER minion_controller = collision.gameObject.GetComponent<MINION_CONTROLLER>();
            minion_controller.GetDamage(2);
            //vidaEnemigo -= damageAliado1; 
            //vidaTorre -= damageAliado1;
            Destroy(this.gameObject);
        }
    }
    public void GenerateDrop()
    {
        // Generar drop en la posición del enemigo
        Instantiate(drop, transform.position, Quaternion.identity);
    }
    private IEnumerator Dropear() // Corrutina para crear un drop al morir un enemigo
    {
        yield return new WaitForSeconds(0.0f);
        GenerateDrop();
        Destroy(this.gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ALIADO_CONTROLER : MonoBehaviour
{
    public int vidaAliado1 = 10;
    public int damageAliado1 = 10;
    public Slider barraVidaAliado1;
    // Start is called before the first frame update
    void Start()
    {
        barraVidaAliado1.value = vidaAliado1;
    }

    // Update is called once per frame
    void Update()
    {
       
        barraVidaAliado1.value = vidaAliado1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ENEMY")|| collision.gameObject.CompareTag("TORRE")) 
        {
            //vidaEnemigo -= damageAliado1; 
            //vidaTorre -= damageAliado1;
            Destroy(this.gameObject);
        }
    }

}

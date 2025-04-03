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
    public TANKENEMY_CONTROLLER tankenemy_controller;
    // Start is called before the first frame update
    void Start()
    {
        barraVidaAliado1.value = vidaAliado1;
    }

    // Update is called once per frame
    void Update()
    {
        if (CompareTag("ALLY")) // Comportamietos de aliados
        {
            this.gameObject.transform.Translate(2 * Time.deltaTime, 0, 0);
        }
        if (vidaAliado1 <= 0)
        {

            CooldownAliado();
        }

        barraVidaAliado1.value = vidaAliado1;
    }
    public IEnumerator CooldownAliado()
    {
        yield return new WaitForSeconds(.6f);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ENEMY"))
        {
            //minion_controller.minionHealth -= 2;
            MINION_CONTROLLER minion = other.gameObject.GetComponent<MINION_CONTROLLER>();
            TANKENEMY_CONTROLLER tanke = other.gameObject.GetComponent<TANKENEMY_CONTROLLER>();
            if (minion != null)
            {
                StartCoroutine(other.gameObject.GetComponent<MINION_CONTROLLER>().GetDamage());
                StartCoroutine(CooldownAliado());

            }
            if (tanke != null)
            {
                StartCoroutine(other.gameObject.GetComponent<TANKENEMY_CONTROLLER>().GetDamage());
                StartCoroutine(CooldownAliado());
            }
            vidaAliado1 = 0;
           
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MINION_CONTROLLER : MonoBehaviour
{
    public Slider barraVidaMinion;
    public float minionHealth;
    public bool minionDmgCooldown;
    public GameObject drop;
    // Start is called before the first frame update
    void Start()
    {
        barraVidaMinion.value = minionHealth;
        minionHealth = 10;
        minionDmgCooldown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (minionHealth <= 0)
        {
            //aqui agrega cosas de soltar objetos y dineros y eso, se hace antes de que se destruya el objeto
            //Instantiate(drop, transform.position, Quaternion.identity); NO LO ACTIVES QUE SPAWNEA SIN 
            StartCoroutine(Dropear());

        }

        if (CompareTag("ENEMY")) // Comportamientos de enemigos
        {
            this.gameObject.transform.Translate(-2 * Time.deltaTime, 0, 0);
        }
        //if (CompareTag("ALLY")) // Comportamietos de aliados
        //{
        //    this.gameObject.transform.Translate(2 * Time.deltaTime, 0, 0);
        //}
        barraVidaMinion.value = minionHealth;
    }

    public IEnumerator GetDamage()
    {
        if (minionDmgCooldown)
        {
            minionHealth -= 2;
            print("BONK");
            minionDmgCooldown = false;
            yield return new WaitForSeconds(0.5f);
            minionDmgCooldown = true;
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

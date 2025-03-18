using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apuesta_mode : MonoBehaviour
{
    public bool changeApuesta = false;
    public bool apuestaHecha = false;

    public bool rojoApuesta = false;
    public bool negroApuesta = false;
    public bool verdeApuesta = false;

    public bool rojoRuleta = false;
    public bool negroRuleta = false;
    public bool verdeRuleta = false;

    public int timeChange = 30;
    public int probabilidad;
    public int dañoABoss;

    public GameObject botonApuesta;
    public GameObject zonaApuesta;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeApuesta());
        zonaApuesta.SetActive(false);
        botonApuesta.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (apuestaHecha)
        {
            StartCoroutine(TimeApuesta());
            zonaApuesta.SetActive(false);
        }

        if (probabilidad <= 4) //verde
        {
            verdeRuleta = true;
            ResultadoApuesta();

        }
        else if (probabilidad >= 5 || probabilidad <= 48) // rojo
        {
            rojoRuleta = true;
            ResultadoApuesta();
        }
        else // negro
        { 
            negroRuleta = true;
            ResultadoApuesta();
        }


    }

    public void Apuesta()
    {
        zonaApuesta.SetActive(true);
        botonApuesta.SetActive(false);

    }
    public void ResultadoApuesta()
    {
        if (rojoApuesta || rojoRuleta) 
        {
            //Dano a boss
        }
        else if (negroApuesta || negroRuleta)
        {
            //Dano a boss
        }
        else if (verdeApuesta || verdeRuleta)
        {

            //Destroy(Boss);
        }
        else
        {
            //Dano a player
        }

    }

    IEnumerator TimeApuesta()
    {
        yield return new WaitForSeconds(timeChange);
        botonApuesta.SetActive(true);
    }


}

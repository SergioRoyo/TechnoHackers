using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public int timeChange = 30; // tiempo cada que se activa el modo apuesta
    public int probabilidad; // numero del 1 al 100 que se usa para hacer la probabilidad de la ruleta
    public int dañoABoss; // cantidad de daño que se le hace al boss si ganas en la ruleta

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
        if (apuestaHecha) // Una vez hecha la apuesta se vuelve a activar el tiempo para el modo apuesta
        {
            StartCoroutine(TimeApuesta());
            zonaApuesta.SetActive(false);
        }

        if (probabilidad <= 4) //despues de apostar, probalbilidad de que te toque verde
        {
            verdeRuleta = true;
            ResultadoApuesta();

        }
        else if (probabilidad >= 5 || probabilidad <= 48) // despues de apostar, probalbilidad de que te toque rojo
        {
            rojoRuleta = true;
            ResultadoApuesta();
        }
        else //despues de apostar, probalbilidad de que te toque negro
        {
            negroRuleta = true;
            ResultadoApuesta();
        }


        //probabilidad = Random.Range(0,100) ; codigo a añadir al darle a apostar
        // y añadir uno de estos el que sea en cada caso
        //rojoApuesta = true;
        //negroApuesta = true;
        //verdeApuesta = true;


    }

    public void Apuesta() //funcion llamada por el boton de apostar para que se active la zona de apuesta y el boton de apostar se desactive 
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
        else if (verdeApuesta || verdeRuleta)// al ser tan baja la probabilidad de que te toque el verde, si apuestas y te toca, matas automaticamente al boss
        {

            //Destruir a (Boss) ;
        }
        else
        {
            //Dano a player, te quita una vida
        }
            rojoApuesta = false;
            negroApuesta = false;
            verdeApuesta = false;

            rojoRuleta = false;
            negroRuleta = false;
            verdeRuleta = false;

    }

    IEnumerator TimeApuesta() // cada (timeChange) segundos se activa el modo apuesta en la partida
    {
        yield return new WaitForSeconds(timeChange);
        botonApuesta.SetActive(true);
    }


}

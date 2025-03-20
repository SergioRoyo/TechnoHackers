using UnityEngine;
using UnityEngine.UI;

public class BotonesApuesta : MonoBehaviour
{
    public GameObject botonRojo;
    public GameObject botonVerde;
    public GameObject botonNegro;
    public GameObject player;

    // Referencia al objeto principal que contiene las zonas
    public GameObject ZonaApuesta;

    private GameObject redZone;
    private GameObject greenZone;
    private GameObject blackZone;

    private GameObject zonaApuesta; // Para determinar la zona activa.
    public GameObject botones;
    private Button botonActivo; // Bot�n activo.

    private void Start()
    {
        // Asignamos las zonas a partir de los hijos de ZonaApuesta
        redZone = ZonaApuesta.transform.GetChild(0).gameObject; // Primer hijo: zona roja
        greenZone = ZonaApuesta.transform.GetChild(1).gameObject; // Segundo hijo: zona verde
        blackZone = ZonaApuesta.transform.GetChild(2).gameObject; // Tercer hijo: zona negra

        // Aseguramos que los botones est�n desactivados al inicio.
        botonRojo.SetActive(false);
        botonVerde.SetActive(false);
        botonNegro.SetActive(false);
    }

    private void Update()
    {
        // Determinamos en qu� zona est� el jugador.
        if (Vector3.Distance(player.transform.position, redZone.transform.position) < 1f)
        {
            zonaApuesta = redZone;
            SetBotonActivo(botonRojo);
            Debug.Log("Est� en el rojo");
        }
        else if (Vector3.Distance(player.transform.position, greenZone.transform.position) < 1f)
        {
            zonaApuesta = greenZone;
            SetBotonActivo(botonVerde);
            Debug.Log("Est� en el verde");
        }
        else if (Vector3.Distance(player.transform.position, blackZone.transform.position) < 1f)
        {
            zonaApuesta = blackZone;
            SetBotonActivo(botonNegro);
            Debug.Log("Est� en el negro");
        }
        else
        {
            // Si el jugador no est� en ninguna zona, desactivamos todos los botones.
            ResetBotones();
            Debug.Log("No est� en ninguna zona.");
        }

        // Si el jugador presiona la tecla E y est� en una zona, activamos el bot�n correspondiente.
        if (zonaApuesta != null && Input.GetKeyDown(KeyCode.E))
        {
            if (botonActivo != null)
            {
                botonActivo.onClick.Invoke();
                Debug.Log("Bot�n presionado.");

                // Cuando el bot�n sea presionado, desactivamos la ZonaApuesta.
                ZonaApuesta.SetActive(false);
                Debug.Log("Apuesta finalizada");
                botones.SetActive(false);
            }
        }
    }

    // M�todo para activar el bot�n correspondiente.
    private void SetBotonActivo(GameObject boton)
    {
        // Desactivamos todos los botones.
        ResetBotones();

        // Activamos el bot�n correspondiente.
        boton.SetActive(true);

        // Referencia al bot�n activo.
        botonActivo = boton.GetComponent<Button>();
    }

    // M�todo para desactivar todos los botones.
    private void ResetBotones()
    {
        botonRojo.SetActive(false);
        botonVerde.SetActive(false);
        botonNegro.SetActive(false);
    }
}

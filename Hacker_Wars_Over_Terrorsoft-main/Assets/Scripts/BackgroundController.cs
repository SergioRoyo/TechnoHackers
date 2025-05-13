using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos, length;
    public GameObject cam;
    public float parallaxEffect; // La velocidad en la que el fondo deber�a moverse relativamente a la c�mara.

    void Start()
    {
        startPos = transform.position.x; // Guarda la posici�n inicial del fondo
        length = GetComponent<SpriteRenderer>().bounds.size.x; // Calcula el tama�o del fondo en base al sprite
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Calcula la distancia recorrida por el fondo en base en el movimiento de la camara
        float distance = cam.transform.position.x * parallaxEffect; // 0 = fondo se mueve con la camara || 0.5 = la mitad || 1 = no se mover�
        float movement = cam.transform.position.x * (1 - parallaxEffect); // La distancia que se mover� el fondo

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if(movement > startPos + length) // Si el fondo se ha movido m�s all� de su longitud
        {
            startPos += length; // Reinicia la posici�n del fondo
        }
        else if (movement < startPos - length) // Si el fondo se ha movido m�s all� de su longitud en la otra direcci�n
        {
            startPos -= length; // Reinicia la posici�n del fondo
        }
    }
}

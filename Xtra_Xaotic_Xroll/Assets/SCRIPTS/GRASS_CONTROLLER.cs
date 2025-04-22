using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRASS_CONTROLLER : MonoBehaviour
{
    public GameObject wood;
    public bool destroyGrassCoolDown;
    public int grassHealth = 3;
    public Collider2D grassCollider;
    public GameObject grass;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grassHealth <= 0)// SI LA VIDA DE GRASS ES 0 SPAWNEA UN DROP DE MADERA 
        {
            GenerateDrop();
            Destroy(this.gameObject);
        }
    }
    public IEnumerator GrassDestroy()
    {
        if (!destroyGrassCoolDown)//Esta corrutina "tala el grass" le elimina el primer hijo para hacer efecto de estar talando
                                  //y desactiva y activa el collider para que vuelva a contar que estas dentro
        {
            print("TAlando");
            Destroy(grass.transform.GetChild(0).gameObject);
            grassCollider.enabled = false;
            grassCollider.enabled = true;
            destroyGrassCoolDown = true;
            grassHealth--;
            yield return new WaitForSeconds(.2f);// tiempo de espera entre que puedes seguir talando
            destroyGrassCoolDown = false;
        }
    }
    public void GenerateDrop()
    {
        // Generar drop en la posición del enemigo
        Instantiate(wood, transform.position, Quaternion.identity);// spawnea drop de madera
    }

}

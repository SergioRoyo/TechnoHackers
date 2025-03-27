using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TANKENEMY_CONTROLLER : MonoBehaviour
{
    public int tankLife = 50;
    PLAYER_MOVEMENT playerS;
    public GameObject drop;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(-2 * Time.deltaTime, 0, 0);
        if(tankLife <= 0)
        {
            StartCoroutine(Dropear());
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (CompareTag("Player"))
        {
            AttackPlayer();
        }
    }
    public void AttackPlayer()
    {
        playerS.playerLife -= 2;
        Debug.Log("Te han atacado");
    }
    public void GetDamage()
    {
        tankLife -= 2;
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

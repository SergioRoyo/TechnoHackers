using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TANKENEMY_CONTROLLER : MonoBehaviour
{
    public int tankHealth = 50;
    public GameObject drop;
    public bool tankDmgCooldown;
    // Start is called before the first frame update
    void Start()
    {
      tankDmgCooldown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(tankHealth <= 0)
        {
            StartCoroutine(Dropear());
        }
        this.gameObject.transform.Translate(-2 * Time.deltaTime, 0, 0);
    }
    public IEnumerator GetDamage()
    {
        if (tankDmgCooldown)
        {
            tankHealth -= 2;
            print("BONK TANK");
            tankDmgCooldown = false;
            yield return new WaitForSeconds(0.5f);
            tankDmgCooldown = true;
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

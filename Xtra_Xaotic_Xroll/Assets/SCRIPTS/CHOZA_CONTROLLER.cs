using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHOZA_CONTROLLER : MonoBehaviour
{
    public GameObject allyChoza;
    public float generationTime = 5f;
    public int nivelChoza;
    public bool Active;
    public int chozaHealth = 50;          // Salud inicial de la choza
    public int maxChozaHealth = 50;       // Salud m�xima de la choza
    public GameObject repairButton;
    public float repairTime = 2f;         // Tiempo de reparaci�n
    public int woodAmount = 100;          // Cantidad de madera disponible
    public int repairCostPerHealth = 10; // Madera por cada punto de salud reparado

    // Start is called before the first frame update
    void Start()
    {
        Active = true;
        repairButton.SetActive(false);
        InvokeRepeating("GenerateAllyChoza", 5f, generationTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (chozaHealth <= 0)
        {
            Active = false;
        }
        if (Active == false)
        {
            CancelInvoke();
            repairButton.SetActive(true);
        }
    }

    public void GenerateAllyChoza()
    {
        Instantiate(allyChoza, transform.position, Quaternion.identity);
    }

    public void RepairChoza()
    {
        // Comprobar si hay suficiente madera y si la choza no est� a plena salud
        if (woodAmount > 0 && chozaHealth < maxChozaHealth)
        {
            // Inicia la reparaci�n con un tiempo de espera
            StartCoroutine(RepairCoroutine());
        }
        else
        {
            Debug.Log("No hay suficiente madera o la choza ya est� a plena salud.");
        }
    }

    private IEnumerator RepairCoroutine()
    {
        // Desactiva el bot�n de reparaci�n mientras se repara
        repairButton.SetActive(false);

        // Calcula cu�nto se va a reparar (proporcional al coste de madera)
        int healthToRepair = maxChozaHealth - chozaHealth;

        // Calcula el coste de madera para la reparaci�n
        int woodCost = healthToRepair * repairCostPerHealth;

        // Verifica que haya suficiente madera
        if (woodAmount >= woodCost)
        {
            // Espera durante el tiempo de reparaci�n
            yield return new WaitForSeconds(repairTime);

            // Repara la choza y actualiza la cantidad de madera
            chozaHealth = maxChozaHealth;
            woodAmount -= woodCost;

            // Una vez reparada, activa la choza
            Active = true;
            Debug.Log("Choza reparada. Madera restante: " + woodAmount);
        }
        else
        {
            Debug.Log("No hay suficiente madera para reparar la choza.");
        }

        // Reactiva el bot�n de reparaci�n
        repairButton.SetActive(true);
    }
}


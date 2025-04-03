using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHOZA_CONTROLLER : MonoBehaviour
{
    public GameObject allyChoza;
    public float generationTime = 5f;
    public int nivelChoza;
    public bool Active;
    public Slider barraVidaChoza;
    public int chozaHealth = 50;          // Salud inicial de la choza
    public int maxChozaHealth = 50;       // Salud m�xima de la choza
    public GameObject repairButton;
    public float repairTime = 2f;         // Tiempo de reparaci�n
    public int repairCostPerHealth = 10; // Madera por cada punto de salud reparado
    public PLAYER_MOVEMENT player;        // Referencia al script del jugador

    private int damageReceived = 0;       // Almacena el da�o recibido

    // Start is called before the first frame update
    void Start()
    {
        Active = true;
        barraVidaChoza.value = chozaHealth;
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

        // Si la choza est� inactiva (destruida), activa el bot�n de reparaci�n
        if (Active == false)
        {
            repairButton.SetActive(true);
        }
        else
        {
            repairButton.SetActive(false);
        }

        barraVidaChoza.value = chozaHealth;
    }

    // Colisi�n con enemigos para recibir da�o
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ENEMY") && Active)
        {
            GetDamage(5);  // El da�o recibido por el enemigo es de 5 por cada colisi�n
        }
    }

    // Funci�n que aplica el da�o a la choza y acumula el da�o total
    public void GetDamage(int damage)
    {
        chozaHealth -= damage;
        damageReceived += damage;
        print("Atacan la choza. Da�o recibido: " + damageReceived);
    }

    // Funci�n de reparaci�n de la choza
    public void RepairChoza()
    {
        // Comprobar si la choza est� destruida (sin vida) y no est� activa
        if (chozaHealth <= 0 && Active == false)
        {
            // Comprobar si hay suficiente madera y si la choza no est� a plena salud
            if (player.woodCount > 0 && chozaHealth < maxChozaHealth)
            {
                // Inicia la reparaci�n con un tiempo de espera
                StartCoroutine(RepairCoroutine());
            }
        }
    }

    // Corutina que maneja la reparaci�n de la choza
    private IEnumerator RepairCoroutine()
    {
        // Desactiva el bot�n de reparaci�n mientras se repara
        repairButton.SetActive(false);

        // Calcula cu�nto se va a reparar (proporcional al coste de madera)
        int healthToRepair = maxChozaHealth - chozaHealth;

        // Calcula el coste de madera para la reparaci�n
        int woodCost = healthToRepair * repairCostPerHealth;

        // Verifica que haya suficiente madera
        if (player.woodCount >= woodCost)
        {
            // Espera durante el tiempo de reparaci�n
            yield return new WaitForSeconds(repairTime);

            // Repara la choza y actualiza la cantidad de madera
            chozaHealth = maxChozaHealth;
            player.woodCount -= woodCost;

            // Una vez reparada, activa la choza
            Active = true;
            damageReceived = 0;  // Restablecemos el da�o recibido tras la reparaci�n
            Debug.Log("Choza reparada. Madera restante: " + player.woodCount);
        }
        else
        {
            Debug.Log("No hay suficiente madera para reparar la choza.");
        }

        // Reactiva el bot�n de reparaci�n
        repairButton.SetActive(true);
    }

    // Genera una nueva choza aliada
    public void GenerateAllyChoza()
    {
        Instantiate(allyChoza, transform.position, Quaternion.identity);
    }
}

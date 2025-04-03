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
    public int maxChozaHealth = 50;       // Salud máxima de la choza
    public GameObject repairButton;
    public float repairTime = 2f;         // Tiempo de reparación
    public int repairCostPerHealth = 10; // Madera por cada punto de salud reparado
    public PLAYER_MOVEMENT player;        // Referencia al script del jugador

    private int damageReceived = 0;       // Almacena el daño recibido

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

        // Si la choza está inactiva (destruida), activa el botón de reparación
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

    // Colisión con enemigos para recibir daño
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ENEMY") && Active)
        {
            GetDamage(5);  // El daño recibido por el enemigo es de 5 por cada colisión
        }
    }

    // Función que aplica el daño a la choza y acumula el daño total
    public void GetDamage(int damage)
    {
        chozaHealth -= damage;
        damageReceived += damage;
        print("Atacan la choza. Daño recibido: " + damageReceived);
    }

    // Función de reparación de la choza
    public void RepairChoza()
    {
        // Comprobar si la choza está destruida (sin vida) y no está activa
        if (chozaHealth <= 0 && Active == false)
        {
            // Comprobar si hay suficiente madera y si la choza no está a plena salud
            if (player.woodCount > 0 && chozaHealth < maxChozaHealth)
            {
                // Inicia la reparación con un tiempo de espera
                StartCoroutine(RepairCoroutine());
            }
        }
    }

    // Corutina que maneja la reparación de la choza
    private IEnumerator RepairCoroutine()
    {
        // Desactiva el botón de reparación mientras se repara
        repairButton.SetActive(false);

        // Calcula cuánto se va a reparar (proporcional al coste de madera)
        int healthToRepair = maxChozaHealth - chozaHealth;

        // Calcula el coste de madera para la reparación
        int woodCost = healthToRepair * repairCostPerHealth;

        // Verifica que haya suficiente madera
        if (player.woodCount >= woodCost)
        {
            // Espera durante el tiempo de reparación
            yield return new WaitForSeconds(repairTime);

            // Repara la choza y actualiza la cantidad de madera
            chozaHealth = maxChozaHealth;
            player.woodCount -= woodCost;

            // Una vez reparada, activa la choza
            Active = true;
            damageReceived = 0;  // Restablecemos el daño recibido tras la reparación
            Debug.Log("Choza reparada. Madera restante: " + player.woodCount);
        }
        else
        {
            Debug.Log("No hay suficiente madera para reparar la choza.");
        }

        // Reactiva el botón de reparación
        repairButton.SetActive(true);
    }

    // Genera una nueva choza aliada
    public void GenerateAllyChoza()
    {
        Instantiate(allyChoza, transform.position, Quaternion.identity);
    }
}

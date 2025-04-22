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
    public int chozaHealth = 50;
    public int maxChozaHealth = 50;
    public GameObject repairButton;
    public float repairTime = 2f;
    public int repairCostPerHealth = 10;
    public PLAYER_MOVEMENT player;
    public ParticleSystem repairparticles;

    private int damageReceived = 0;

    void Start()
    {
        Active = true;
        barraVidaChoza.value = chozaHealth;
        repairButton.SetActive(false);
        repairparticles.Stop();
        StartCoroutine(GenerarAliadosLoop());
    }

    void Update()
    {
        if (chozaHealth <= 0)
        {
            Active = false;
        }

        repairButton.SetActive(!Active);
        barraVidaChoza.value = chozaHealth;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ENEMY") && Active)
        {
            GetDamage(5);
        }
    }

    public void GetDamage(int damage)
    {
        chozaHealth -= damage;
        damageReceived += damage;
        print("Atacan la choza. Daño recibido: " + damageReceived);
    }

    public void RepairChoza()
    {
        if (chozaHealth <= 0 && !Active)
        {
            if (player.woodCount > 0 && chozaHealth < maxChozaHealth)
            {
                StartCoroutine(RepairCoroutine());
            }
        }
    }

    private IEnumerator RepairCoroutine()
    {
        repairButton.SetActive(false);
        repairparticles.Play();

        int healthToRepair = maxChozaHealth - chozaHealth;
        int woodCost = healthToRepair * repairCostPerHealth;

        if (player.woodCount >= woodCost)
        {
            yield return new WaitForSeconds(repairTime);

            chozaHealth = maxChozaHealth;
            player.woodCount -= woodCost;
            Active = true;
            damageReceived = 0;
            Debug.Log("Choza reparada. Madera restante: " + player.woodCount);
        }
        else
        {
            Debug.Log("No hay suficiente madera para reparar la choza.");
        }

        repairButton.SetActive(true);
        repairparticles.Stop();
    }

    private IEnumerator GenerarAliadosLoop()
    {
        while (true)
        {
            if (Active)
            {
                Instantiate(allyChoza, transform.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(generationTime);
        }
    }
}

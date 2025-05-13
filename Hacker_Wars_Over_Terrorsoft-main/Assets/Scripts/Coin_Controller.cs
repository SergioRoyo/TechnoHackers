using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin_Controller : MonoBehaviour
{
    public int coinCount = 0; // Contador global de monedas
    public Animator _animator;
    public TMP_Text coinText;
    public Collider2D coinCollider;
    public TimeManager timeManager;
    public int timeAdd;
  
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       
           // coinText.text = "Monedas: " + coinCount.ToString();
    }

   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetTrigger("jump");// Activa la animacion 
            timeManager.time += timeAdd; // Aumenta el tiempo
            Destroy(coinCollider);
            Destroy(gameObject, 2f); // Destruye la moneda
        }
    }
}

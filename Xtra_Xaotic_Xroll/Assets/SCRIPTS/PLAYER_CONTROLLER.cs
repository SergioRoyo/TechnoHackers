using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PLAYER_MOVEMENT : MonoBehaviour
{
    public float vel;
    public float jumpForce;
    int _jumpCount;
    Rigidbody2D _rb;
    public GameObject vaultmenu;
    public Slider minimap;
    public float playerPos;
    public TextMeshProUGUI locationText;
    public GameObject playerWeapon;
    public MINION_CONTROLLER MINION_CONTROLLER;
    public GameObject drop;
    public int _dropCount;
    public TextMeshProUGUI resourceCounter;

    // Start is called before the first frame update
    void Start()
    {
        // Desactivamos el panel del Vault y pillamos el RigidBody
        vaultmenu.SetActive(false);
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Estas 3 l�neas copian la posici�n del player y la manda al minimapa
        playerPos = this.gameObject.transform.position.x;
        minimap.value = playerPos;
        locationText.text = playerPos.ToString("F0");

        float direction = Input.GetAxis("Horizontal");
        if (direction != 0) // Mambiar la direccion del sprite segun su desplazamiento
        {
            FlipSprite(direction);
        }
        _rb.velocity = new Vector2(vel * direction, _rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && _jumpCount <= 0) // Movimiento basico del jugador
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _jumpCount++;
        }
    }
    void FlipSprite(float direction)
    {
        // Si el player se mueve a la izquierda -1
        if (direction < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Mscala invertida en vertical (flip)
        }
        // Si el player se mueve a la derecha +1
        else if (direction > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Mantener la escala original
        }
    }


    // Comprobamos los colliders y sus tags
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Cuando tocamos el suele, reseteamos la cuenta de saltos
        if (collision.collider.tag == "SUELO")
        {
            _jumpCount = 0;
        }

        // Cuando tocamos la base enemiga, reseteamos la escena (provisional)
        if (collision.collider.tag == "ENEMYBASE")
        {
            SceneManager.LoadScene(0);
        }
    }

    // Comprobamos la entrada en triggers y sus tags
    public void OnTriggerEnter2D(Collider2D other)
    {
        // Cuando entramos en la base aliada, activamos el panel de vault
        if (other.tag == "YOURBASE")
        {
            vaultmenu.SetActive(true);
        }
    }

    // Comprobamos la salida de triggers y sus tags
    public void OnTriggerExit2D(Collider2D other)
    {
        // Cuando salimos de la base, desactivamos el panel de vault
        if (other.tag == "YOURBASE")
        {
            vaultmenu.SetActive(false);
        }
    }

    // Comprobamos si estamos dentro de un trigger y sus tags
    public void OnTriggerStay2D(Collider2D other)
    {
        // Si estamos dentro de un enemigo, hacemos clic y llamamos al conteo de vida del enemigo
        if(other.tag == "ENEMY" && Input.GetMouseButton(0)) 
        {
            other.gameObject.GetComponent<MINION_CONTROLLER>().GetDamage();
            print("BONK");
            //aqui si deja apretao se ejecuta 1000 veces por segundo, a�ade algun tipo de cooldown crack
        }

        // Si estamos dentro de un drop, nos suma puntos y eliminamos el objeto
        if (other.tag == "DROP")
        {
            _dropCount += 10;
            print(_dropCount);
            Destroy(other.gameObject);
            resourceCounter.text = _dropCount.ToString("F0");
        }
    }
}

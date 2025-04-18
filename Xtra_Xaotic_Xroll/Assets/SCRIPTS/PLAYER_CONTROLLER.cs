using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

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
    public GameObject drop;
    public int _dropCount;
    public int woodCount;
    public TextMeshProUGUI resourceCounter;
    public int playerLife = 100;

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

        resourceCounter.text = _dropCount.ToString("F0");
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

        // Si estamos dentro de un drop, nos suma puntos y eliminamos el objeto
        if (collision.collider.tag == "DROP")
        {
            _dropCount += 10;
            print(_dropCount);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "WOOD")
        {
            woodCount++;
            print(woodCount);
            Destroy(collision.gameObject);
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
        if (other.tag == "ENEMY" && Input.GetMouseButton(0))
        {
            MINION_CONTROLLER minion = other.gameObject.GetComponent<MINION_CONTROLLER>();
            TANKENEMY_CONTROLLER tanke = other.gameObject.GetComponent<TANKENEMY_CONTROLLER>();
            if (minion != null)
            {
                StartCoroutine(other.gameObject.GetComponent<MINION_CONTROLLER>().GetDamage());
            }
            if (tanke != null)
            {
                StartCoroutine(other.gameObject.GetComponent<TANKENEMY_CONTROLLER>().GetDamage());
            }
            //aqui si deja apretao se ejecuta 1000 veces por segundo, a�ade algun tipo de cooldown crack
        }
       
            if (other.tag == "GRASS" && Input.GetMouseButton(0))
            {
            GRASS_CONTROLLER grass = other.gameObject.GetComponent<GRASS_CONTROLLER>();
                StartCoroutine(grass.GrassDestroy());
            }
    }
}

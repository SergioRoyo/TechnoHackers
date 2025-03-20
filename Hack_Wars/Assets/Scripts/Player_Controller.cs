using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("Par�metros de Movimiento")]
    public float setSpeed;       // Velocidad de movimiento horizontal
    float actualSpeed;
    public float jumpForce;   // Fuerza aplicada al saltar

    private Rigidbody2D rb;
    private bool isGrounded = false; // Indica si el jugador est� en el suelo
    private Vector2 lastMoveDirection = Vector2.right; // �ltima direcci�n de movimiento

    public GameObject bulletPrefab;
    public Transform firePoint; // Un GameObject vac�o donde se originan las balas
    public float fireRate = 0.1f;
    private float nextFireTime = 0f;

    void Start()
    {
        // Obtiene el componente Rigidbody2D del jugador
        rb = GetComponent<Rigidbody2D>();
        actualSpeed = setSpeed;
    }

    void Update()
    {
        // Movimiento horizontal
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * actualSpeed, rb.velocity.y);

        // Actualizar la �ltima direcci�n de movimiento si hay input del jugador
        if (moveInput > 0)
            lastMoveDirection = Vector2.right;
        else if (moveInput < 0)
            lastMoveDirection = Vector2.left;

        // Salto: se activa cuando se presiona la tecla "Salto" y el jugador est� en el suelo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKey(KeyCode.Return) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            actualSpeed = 0;
        }
        else
        {
            actualSpeed = setSpeed;
        }
    }

    // Comprueba si el jugador est� en contacto con el suelo mediante colisiones
    void OnCollisionEnter2D(Collision2D collision)
    {
        // El suelo debe tener la etiqueta "Ground"
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    // Cuando el jugador deja de estar en contacto con el suelo, se marca como no en el suelo
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        // Direcci�n predeterminada basada en el �ltimo movimiento
        Vector2 shootDirection = lastMoveDirection;

        // Detectar combinaciones de teclas para disparo en diagonal
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            shootDirection = new Vector2(1, 1).normalized; // Arriba-Derecha
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            shootDirection = new Vector2(-1, 1).normalized; // Arriba-Izquierda
        else if (Input.GetKey(KeyCode.W))
            shootDirection = Vector2.up;
        else if (Input.GetKey(KeyCode.S))
            shootDirection = Vector2.down;
        else if (Input.GetKey(KeyCode.A))
            shootDirection = Vector2.left;
        else if (Input.GetKey(KeyCode.D))
            shootDirection = Vector2.right;

        bulletScript.direction = shootDirection;
    }
}
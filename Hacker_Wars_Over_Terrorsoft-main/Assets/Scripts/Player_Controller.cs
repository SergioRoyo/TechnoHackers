using System.Collections;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public ParticleSystem dust;

    [Header("Par�metros de Movimiento")]
    public float speed = 5f;       // Velocidad de movimiento horizontal
    public float jumpForce = 5f;   // Fuerza aplicada al saltar
    public bool useTransformMovement;
    public Animator flip;
    public TrailRenderer dash;
    private bool canDash = true;

    private Rigidbody2D rb;
    private bool isGrounded = false; // Indica si el jugador est� en el suelo
    public GrapplingHook hook;
    public GameObject civil;
    public bool civilOn = false; //Indica si el civil esta activo o no
    TimeManager timeManager;

    Window_Controller window;
    void Start()
    {
        timeManager = FindAnyObjectByType<TimeManager>(); //
        civil.gameObject.SetActive(false); //Desactiva al civil que acompa�a al player
        window = FindObjectOfType<Window_Controller>();
        // Obtiene el componente Rigidbody2D del jugador
        rb = GetComponent<Rigidbody2D>();
        flip = transform.GetChild(0).GetComponent<Animator>();
        hook = FindAnyObjectByType<GrapplingHook>();
        dash = GetComponent<TrailRenderer>();
        dash.emitting = false;
    }

    void Update()
    {
        // Movimiento horizontal
        float x = Input.GetAxis("Horizontal");
        if (isGrounded)
        {
            canDash = true;
        }

        Jump();
        Dash();

        if (useTransformMovement == false)
        {
            rb.velocity = new Vector3(x, rb.velocity.y, 0);

        }
        else
        {
            transform.position = new Vector3(transform.position.x + x * Time.deltaTime * speed, transform.position.y, transform.position.z);
        }
    }


    void Jump()
    {
        // Salto: se activa cuando se presiona la tecla "Salto" y el jugador est� en el suelo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            CreateDust();
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
            flip.Play("FlipAnimation");
        }
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded == false && hook.isGrappling == false && canDash)
        {
            float dashForce = 20f;

            // Obtiene la direcci�n actual del movimiento (basada en input)
            Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

            if (inputDirection != Vector3.zero)
            {
                Vector3 dashDirection = transform.TransformDirection(inputDirection);
                rb.velocity = dashDirection * dashForce;

                // Activar trail
                dash.emitting = true;
                StartCoroutine(StopTrailAfterTime(0.2f)); // Opcional: detenerlo luego de un momento

                // Inhabilita el dash hasta que vuelva a estar en el suelo
                canDash = false;
            }
        }
//dash.Play();
    }

    // Comprueba si el jugador est� en contacto con el suelo mediante colisiones
    void OnCollisionEnter2D(Collision2D collision)
    {
        // El suelo debe tener la etiqueta "Ground"
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Void")) //Se vuelve a comenzar en el punto de spawn pero con los segundos restantes que te quedaban
        {
            civilOn=false;
            civil.SetActive(false);
            transform.position = new Vector3(0, 5, 0); 
            
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

    //Si el player entra en la bandera se activa la ventana y se destruye el objeto
    void OnTriggerEnter2D(Collider2D banderon) 
    {
        
        if (banderon.gameObject.CompareTag("Bandera"))
        {
            banderon.gameObject.SetActive(false);
            window.ventana();
            speed = 0;
        }
    }
    private IEnumerator StopTrailAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        dash.emitting = false;
    }

    public void CreateDust()
    {
        dust.Play();
    }
}

using UnityEngine;

public class KaleMovement : MonoBehaviour
{
    public GameObject OndaPrefab;

    [Header("Referencias de Transformación")]
    public GameObject visualHumano;
    public GameObject visualLobo;
    public CapsuleCollider2D colliderHumano; 
    public BoxCollider2D colliderLobo;      

    [Header("Ajustes de Movimiento")]
    public float Speed;
    public float JumpForce;

    [Header("Detección de Suelo")]
    public float rayDistHumano = 2.1f;
    public float rayDistLobo = 0.8f;
    private float RayDistance;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private Vector3 originalScale;
    private bool esLobo = false;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();

        // escala oficial del personaje (0.8, 0.4, 1)
        originalScale = transform.localScale;

        esLobo = false;
        ActualizarEstadoForma();
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        Animator.SetBool("running", Horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down * RayDistance, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, RayDistance))
        {
            Grounded = true;
        }
        else Grounded = false;

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            esLobo = !esLobo;
            ActualizarEstadoForma();
        }

        if (Horizontal > 0)
        {
            transform.localScale = originalScale;
        }
        else if (Horizontal < 0)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void ActualizarEstadoForma()
    {
        visualHumano.SetActive(!esLobo);
        visualLobo.SetActive(esLobo);

        colliderHumano.enabled = !esLobo;
        colliderLobo.enabled = esLobo;

        RayDistance = esLobo ? rayDistLobo : rayDistHumano;

        Animator = esLobo ? visualLobo.GetComponent<Animator>() : visualHumano.GetComponent<Animator>();
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.linearVelocity = new Vector2(Horizontal * Speed, Rigidbody2D.linearVelocity.y);
    }

    void Attack()
    {
        Animator.SetTrigger("Attack");

        float direccion = transform.localScale.x > 0 ? 1f : -1f;

        float offsetY = esLobo ? 0.1f : 0.3f;
        Vector3 spawnPos = transform.position + new Vector3(direccion * 0.1f, offsetY, 0);

        GameObject ondaInstancia = Instantiate(OndaPrefab, spawnPos, Quaternion.identity);
        ondaInstancia.transform.localScale = new Vector3(0.8f, 0.6f, 1f);

        if (direccion < 0)
        {
            ondaInstancia.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            ondaInstancia.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
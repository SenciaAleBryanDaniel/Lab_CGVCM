using UnityEngine;
using UnityEngine.InputSystem;

public class Jugador25D : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 10f;
    private Rigidbody rb;
    private bool enSuelo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ
                       | RigidbodyConstraints.FreezeRotationX
                       | RigidbodyConstraints.FreezeRotationY
                       | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        float movX = 0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            movX = 1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            movX = -1f;

        rb.linearVelocity = new Vector3(movX * velocidad, rb.linearVelocity.y, 0);
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && enSuelo)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            enSuelo = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Suelo"))
            enSuelo = true;
    }
}
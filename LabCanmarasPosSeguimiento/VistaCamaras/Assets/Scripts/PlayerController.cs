using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam; // Arrastra la Main Camera aquí
    public float speed = 2.5f;

    private float gravity = -9.81f;
    private Vector3 velocity;

    void Update()
    {
        // 1. Gravedad para que no se bugee al retroceder
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // 2. ROTACIÓN: El personaje SIEMPRE mira hacia donde mira la cámara
        // Esto permite que al mover el mouse, el soldado gire sobre su eje
        transform.rotation = Quaternion.Euler(0, cam.eulerAngles.y, 0);

        // Cambia GetAxisRaw por GetAxis para que Unity aplique su propio suavizado
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical"); 

        // Calculamos el movimiento basado en hacia dónde mira el modelo actualmente
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        controller.Move(move * speed * Time.deltaTime);

        // 4. Aplicar caída
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
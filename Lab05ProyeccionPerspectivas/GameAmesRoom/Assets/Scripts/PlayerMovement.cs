using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    public float mouseSensitivity = 2f;

    [Header("Camara")]
    public Camera playerCamera;

    private float verticalRotation = 0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MoverJugador();
        RotarCamara();
    }

    void MoverJugador()
    {
        Vector2 moveInput = new Vector2(
            Keyboard.current.dKey.isPressed ? 1 : Keyboard.current.aKey.isPressed ? -1 : 0,
            Keyboard.current.wKey.isPressed ? 1 : Keyboard.current.sKey.isPressed ? -1 : 0
        );

        Vector3 direccion = transform.right * moveInput.x + transform.forward * moveInput.y;
        rb.MovePosition(rb.position + direccion * speed * Time.deltaTime);
    }

    void RotarCamara()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * 0.1f * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseDelta.x);

        verticalRotation -= mouseDelta.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
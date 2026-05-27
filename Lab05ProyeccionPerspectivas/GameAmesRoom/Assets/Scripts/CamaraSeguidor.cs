using UnityEngine;

public class CamaraSeguidor : MonoBehaviour
{
    public Transform jugador;
    private Vector3 offset;
    private float zFija;

    void Start()
    {
        offset = transform.position - jugador.position;
        zFija = transform.position.z;
    }

    void LateUpdate()
    {
        if (jugador == null) return;

        transform.position = new Vector3(
            jugador.position.x + offset.x,
            jugador.position.y + offset.y,
            zFija
        );
    }
}
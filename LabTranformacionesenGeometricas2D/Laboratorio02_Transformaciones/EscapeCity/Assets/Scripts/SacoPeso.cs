using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // Garantiza que el objeto tenga física
public class SacoPeso : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    [Tooltip("Fuerza horizontal aplicada al recibir un impacto")]
    public float fuerzaEmpuje = 3f;

    private Rigidbody2D rb;

    void Awake()
    {
        // Inicialización de la referencia al componente físico
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Método público para aplicar desplazamiento lateral.
    /// Se utiliza linearVelocity para un movimiento inmediato y controlado.
    /// </summary>
    /// <param name="direccion">Valor 1 para derecha, -1 para izquierda</param>
    public void RecibirGolpe(float direccion)
    {
        // Aplicamos el vector de fuerza manteniendo la velocidad vertical actual (caída)
        rb.linearVelocity = new Vector2(direccion * fuerzaEmpuje, rb.linearVelocity.y);
    }
}
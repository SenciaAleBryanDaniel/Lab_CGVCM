using UnityEngine;

public class PlataformaPeso : MonoBehaviour
{
    public PuertaContrapeso scriptPuerta; // Arrastra la puerta aquí en el Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Peso")) scriptPuerta.AñadirPeso();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Peso")) scriptPuerta.QuitarPeso();
    }
}
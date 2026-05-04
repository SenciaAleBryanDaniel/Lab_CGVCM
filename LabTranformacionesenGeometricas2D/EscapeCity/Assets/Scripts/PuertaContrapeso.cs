using UnityEngine;

public class PuertaContrapeso : MonoBehaviour
{
    public Transform posCerrada;
    public Transform posAbierta;
    public float velocidadSuave = 2f;
    public int pesoMaximoNecesario = 3; // Configura esto en el Inspector

    private int objetosEncima = 0;

    void Update()
    {
        float porcentaje = (float)objetosEncima / pesoMaximoNecesario;
        Vector3 destino = Vector3.Lerp(posCerrada.position, posAbierta.position, Mathf.Clamp01(porcentaje));
        transform.position = Vector3.Lerp(transform.position, destino, velocidadSuave * Time.deltaTime);
    }

    public void AñadirPeso() { objetosEncima++; }
    public void QuitarPeso() { objetosEncima = Mathf.Max(0, objetosEncima - 1); }
}
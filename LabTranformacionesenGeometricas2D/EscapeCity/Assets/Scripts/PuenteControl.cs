using UnityEngine;

public class PuenteControl : MonoBehaviour
{
    [Header("Ajustes de Mecanismo")]
    public float velocidadCaida = 300f; 
    private bool activado = false;
    private Quaternion anguloFinal;

    void Start()
    {

        anguloFinal = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        if (activado)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, anguloFinal, velocidadCaida * Time.deltaTime);

            if (transform.rotation == anguloFinal)
            {
                this.enabled = false;
            }
        }
    }

    public void ActivarPuente()
    {
        activado = true;
    }
}
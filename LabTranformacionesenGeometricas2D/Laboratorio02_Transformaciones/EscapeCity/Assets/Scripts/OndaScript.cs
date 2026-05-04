using UnityEngine;

public class OndaScript : MonoBehaviour
{
    public float velocidad = 5f;
    public float tiempoVida = 0.6f;

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        // posibles enemigos
        if (otro.CompareTag("Enemigo"))
        {
            Debug.Log("Corte exitoso");
            Destroy(gameObject, 0.05f);
        }

        // choque con el puente
        if (otro.CompareTag("Puente"))
        {
            // colision con puente
            PuenteControl puente = otro.GetComponent<PuenteControl>();

            if (puente != null)
            {
                puente.ActivarPuente();
                Destroy(gameObject); // tiempo de vida de la onda
            }
        }
    }
}
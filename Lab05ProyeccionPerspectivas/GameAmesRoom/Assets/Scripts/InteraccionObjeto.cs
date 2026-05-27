using UnityEngine;
using UnityEngine.InputSystem;

public class InteraccionObjeto : MonoBehaviour
{
    [Header("Configuracion")]
    public Camera playerCamera;
    public float distanciaAgarre = 3.5f;  
    public float rangoInteraccion = 4f;   
    public float velocidadEscala = 0.5f; 
    public float escalaMinima = 0.3f;
    public float escalaMaxima = 3f;

    private GameObject objetoAgarrado;
    private Vector3 posicionOriginal;
    private Vector3 escalaOriginal;
    private bool sosteniendo = false;

    void Update()
    {
        //logica de agarre y suelte
        if (!sosteniendo)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
                IntentarAgarrar();
        }
        else
        {
            MantenerObjeto();
            EscalarObjeto();

            if (Mouse.current.leftButton.wasPressedThisFrame)
                SoltarObjeto();
        }
    }

    void IntentarAgarrar()
    {
        Ray ray = playerCamera.ScreenPointToRay(
            new Vector3(Screen.width / 2, Screen.height / 2)
        );

        if (Physics.Raycast(ray, out RaycastHit hit, rangoInteraccion))
        {
            if (hit.collider.CompareTag("Interactuable"))
            {
                objetoAgarrado = hit.collider.gameObject;
                posicionOriginal = objetoAgarrado.transform.position;
                escalaOriginal = objetoAgarrado.transform.localScale;
                sosteniendo = true;

                // al levantarlo se queda sin fisicas
                Rigidbody rb = objetoAgarrado.GetComponent<Rigidbody>();
                if (rb != null) rb.isKinematic = true;
            }
        }
    }

    void MantenerObjeto()
    {
        Vector3 posicionFrente = playerCamera.transform.position +
                                 playerCamera.transform.forward * distanciaAgarre;

        objetoAgarrado.transform.position = Vector3.Lerp(
            objetoAgarrado.transform.position,
            posicionFrente,
            Time.deltaTime * 10f
        );
    }

    void EscalarObjeto()
    {
        float scroll = 0f;

        // Q para agrandar, E para encoger
        if (Keyboard.current.qKey.isPressed) scroll = 1f;
        if (Keyboard.current.eKey.isPressed) scroll = -1f;

        if (scroll != 0f)
        {
            Vector3 nuevaEscala = objetoAgarrado.transform.localScale +
                                  Vector3.one * scroll * velocidadEscala * Time.deltaTime;

            // limita el tamaño
            nuevaEscala.x = Mathf.Clamp(nuevaEscala.x, escalaMinima, escalaMaxima);
            nuevaEscala.y = Mathf.Clamp(nuevaEscala.y, escalaMinima, escalaMaxima);
            nuevaEscala.z = Mathf.Clamp(nuevaEscala.z, escalaMinima, escalaMaxima);

            objetoAgarrado.transform.localScale = nuevaEscala;
        }
    }

    void SoltarObjeto()
    {
        // reactiva la fisica
        Rigidbody rb = objetoAgarrado.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;

        // vuelve a su posicion original
        objetoAgarrado.transform.position = posicionOriginal;

        sosteniendo = false;
        objetoAgarrado = null;
    }
}
using UnityEngine;
using Cinemachine; // O Unity.Cinemachine según tu versión

public class SwitchCams : MonoBehaviour
{
    public CinemachineVirtualCamera v1P; // Asignar CM_FirstPerson
    public CinemachineVirtualCamera v3P; // Asignar CM_ThirdPerson

    void Update()
    {
        // Al presionar la tecla 'V' una sola vez
        if (Input.GetKeyDown(KeyCode.V))
        {
            // Intercambiamos las prioridades de forma definitiva
            if (v3P.Priority > v1P.Priority)
            {
                // Pasamos a 1ra persona
                v3P.Priority = 0;
                v1P.Priority = 10;
            }
            else
            {
                // Regresamos a 3ra persona
                v3P.Priority = 10;
                v1P.Priority = 0;
            }
        }
    }
}
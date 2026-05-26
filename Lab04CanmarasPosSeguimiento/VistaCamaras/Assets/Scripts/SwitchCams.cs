using UnityEngine;
using Cinemachine; 

public class SwitchCams : MonoBehaviour
{
    public CinemachineVirtualCamera v1P; 
    public CinemachineVirtualCamera v3P; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (v3P.Priority > v1P.Priority)
            {
                v3P.Priority = 0;
                v1P.Priority = 10;
            }
            else
            {
                v3P.Priority = 10;
                v1P.Priority = 0;
            }
        }
    }
}
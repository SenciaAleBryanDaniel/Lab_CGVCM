using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Kale;



    void Update()
    {
        Vector3 position = transform.position;
        position.x = Kale.transform.position.x;
        transform.position = position;
    }
}

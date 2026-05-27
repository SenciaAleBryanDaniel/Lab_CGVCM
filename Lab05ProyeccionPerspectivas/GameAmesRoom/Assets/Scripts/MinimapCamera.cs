using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public Transform player;
    private float fixedY;

    void Start()
    {
        fixedY = transform.position.y;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(
            player.position.x,
            fixedY,
            player.position.z
        );
    }
}
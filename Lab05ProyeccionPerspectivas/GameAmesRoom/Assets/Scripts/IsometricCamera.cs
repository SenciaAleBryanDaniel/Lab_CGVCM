using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(
            player.position.x + 5f,
            player.position.y + 7f,
            player.position.z - 5f
        );
    }
}
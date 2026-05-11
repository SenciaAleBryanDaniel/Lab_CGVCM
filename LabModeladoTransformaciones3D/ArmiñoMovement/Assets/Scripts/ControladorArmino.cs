using UnityEngine;
using UnityEngine.InputSystem;

public class ControladorArmino : MonoBehaviour
{
    public float velMov = 5f;
    public float velRot = 100f;
    public float fDash = 10f;
    public float escMax = 1.5f;

    float hBase = 3.347f;

    void Update()
    {
        if (Keyboard.current.wKey.isPressed) transform.Translate(Vector3.forward * velMov * Time.deltaTime);
        if (Keyboard.current.sKey.isPressed) transform.Translate(Vector3.back * velMov * Time.deltaTime);

        // rotacion
        if (Keyboard.current.aKey.isPressed) transform.Rotate(0, -velRot * Time.deltaTime, 0);
        if (Keyboard.current.dKey.isPressed) transform.Rotate(0, velRot * Time.deltaTime, 0);

        // dash
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            transform.position += transform.forward * fDash;
        }

        // crecer
        if (Keyboard.current.eKey.isPressed)
        {
            transform.localScale = new Vector3(escMax, escMax, escMax);
            transform.position = new Vector3(transform.position.x, hBase + 0.8f, transform.position.z);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            transform.position = new Vector3(transform.position.x, hBase, transform.position.z);
        }
    }
}
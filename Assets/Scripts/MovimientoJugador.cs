using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float speed = 3.0f; // Velocidad de movimiento

    void Update()
    {
        // Entrada por teclado
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(horizontalInput, 0, 0);
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Entrada t�ctil solo para dispositivos m�viles
      //  if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {  }
        HandleTouchInput();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log("Touch Detected");
            Touch touch = Input.GetTouch(0);

            if (touch.position.x < Screen.width / 2)
            {
                Debug.Log("Moving Left");
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else if (touch.position.x > Screen.width / 2)
            {
                Debug.Log("Moving Right");
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D _rb;
    // Variables para manejar el estado de los botones
    bool isBothPressed = false;
    bool isLeftFirst = false;
    bool isRightFirst = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
        {
            Debug.LogError("Failed to get Rigidbody2D component.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            // Obtener la dirección de la colisión
            Vector2 normal = collision.contacts[0].normal;
            // Normal arriba significa que el jugador chocó con la parte inferior de la plataforma
            if (normal == Vector2.down)
            {
                // El jugador está debajo de la plataforma, no aplicar fuerza de salto
                return;
            }

            // Si el jugador choca con la parte superior de la plataforma, aplicar fuerza de salto
            if (normal == Vector2.up)
            {
                // Llama a un método en PlatformManager para activar el movimiento de las plataformas hacia abajo
                PlatformManager platformManager = GameObject.Find("Generator").GetComponent<PlatformManager>();
                if (platformManager != null)
                {
                    platformManager.ChangeDirectionTemporarily();
                }
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // Detectar presión de botones del teclado o toques en la pantalla
        bool isLeftPressed = Input.GetKey(KeyCode.A) || (Input.touchCount > 0 && Input.GetTouch(0).position.x < Screen.width / 2);
        bool isRightPressed = Input.GetKey(KeyCode.D) || (Input.touchCount > 0 && Input.GetTouch(0).position.x > Screen.width / 2);

        // Determinar si ambos botones están presionados y cuál fue presionado primero
        if (isLeftPressed && isRightPressed && !isBothPressed)
        {
            isBothPressed = true;
            isLeftFirst = isLeftPressed && !isRightPressed;
            isRightFirst = isRightPressed && !isLeftPressed;
            Debug.Log("Ambos (izquierda y derecha) están siendo presionados. Esperando soltar uno...");
        }
        else if (!isLeftPressed && !isRightPressed)
        {
            // Resetear si ambos botones se sueltan
            isBothPressed = false;
            isLeftFirst = false;
            isRightFirst = false;
        }

        // Manejo del movimiento si solo uno está presionado o se suelta uno de los botones
        if (!isBothPressed || (isBothPressed && (isLeftFirst || isRightFirst)))
        {
            if (isLeftPressed && !isRightPressed)
            {
                Vector2 translation = Vector2.left * (speed * Time.deltaTime);
                transform.Translate(translation);
                Debug.Log("Movimiento hacia la izquierda");
                // Resetear para permitir movimiento en cualquier dirección nuevamente
                isBothPressed = false;
            }
            else if (isRightPressed && !isLeftPressed)
            {
                Vector2 translation = Vector2.right * (speed * Time.deltaTime);
                transform.Translate(translation);
                Debug.Log("Movimiento hacia la derecha");
                // Resetear para permitir movimiento en cualquier dirección nuevamente
                isBothPressed = false;
            }
        }
    }

}
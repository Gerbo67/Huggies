using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    
    private Rigidbody2D _rb;
    private float _jumpForce;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
        {
            Debug.LogError("Failed to get Rigidbody2D component.");
        }
        _jumpForce = InitialGame.JumpForce();
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
                // Aplica una fuerza de salto
                _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && _rb != null)
        {
            Touch touch = Input.GetTouch(0);

            float halfScreen = Screen.width / 2;

            // Movimiento horizontal
            if (touch.position.x < halfScreen)
            {
                Vector2 translation = Vector2.left * (speed * Time.deltaTime);
                // Mover a la izquierda
                transform.Translate(translation);
            }
            else if (touch.position.x > halfScreen)
            {
                Vector2 translation = Vector2.right * (speed * Time.deltaTime);
                // Mover a la derecha
                transform.Translate(translation);
            }
        }
    }
}
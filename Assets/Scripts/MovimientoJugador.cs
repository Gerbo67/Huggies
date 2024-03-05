using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5.0f;
    public float normalGravity = 1f;
    public float slowGravity = 0f;
   // private float currentSpeed;

    void Update()
    {
        movimientoJugador();
        Agarre();
        Salto();
    }

    void movimientoJugador()
    {
        float horizontalInput = Input.GetAxis("Horizontal");//Detecta el movimiento horizontal
        Vector3 moveDirection = new Vector3(horizontalInput, 0, 0);// Define la direccion del movimeinto
        transform.Translate(moveDirection * speed * Time.deltaTime);// Mueve el escenario en la direccion opuesta para simular el movimiento del jugador
    }
    void Agarre()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            rb.gravityScale = slowGravity;
        }
        else
        {
            rb.gravityScale = normalGravity;
        }
    }
    void Salto()
    {
        if (Input.GetKey(KeyCode.E))
        {
            rb.AddForce(Vector2.down * speed, ForceMode2D.Impulse);
        }
    }
}

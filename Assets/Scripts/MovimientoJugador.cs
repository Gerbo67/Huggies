using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float speed = 5.0f; // Velocidad de movimiento del escenarioe

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Detecta el movimiento horizontal
        Vector3 moveDirection = new Vector3(horizontalInput, 0, 0); // Define la dirección del movimiento
        transform.Translate(moveDirection * speed * Time.deltaTime); // Mueve el escenario en la dirección opuesta para simular el movimiento del jugador
    }
}

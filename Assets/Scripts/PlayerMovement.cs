using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool jump = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jump)
        {
            // Aplica un salto inicial
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jump = false;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Movimiento horizontal
            if (touch.position.x < Screen.width / 2)
            {
                // Mover a la izquierda
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
            else if (touch.position.x > Screen.width / 2)
            {
                // Mover a la derecha
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
        }
    }
}

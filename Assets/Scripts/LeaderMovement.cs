using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float jumpForce = 4f;
    public float normalGravityScale = 1f;
    public float slowGravityScale = 0.001f;

    //Variables que controlan la entrada mas reciente

    private bool isTouchInputActive = false;
    private bool isKeyboardInputActive = false;

    private int previousTouchCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MovimientoTeclado();
        MovimientoTouch();
        AjustarLaGravedad();
    }
    
    void MovimientoTeclado()
    {
        bool isJumpingUpward = _rb.velocity.y > 0;
        if (Input.GetKey(KeyCode.P))
        {
            isKeyboardInputActive = true;
            isTouchInputActive = false;
        }else if(Input.GetKeyUp(KeyCode.P)&&isJumpingUpward) //Detecta cuando la tecla se levanta
        {
            isKeyboardInputActive = false;
            ChunkJump();
        }
                
    }

    void MovimientoTouch()
    {
        if (Input.touchCount > 0)
        {

            isTouchInputActive = true;
            isKeyboardInputActive = false;
        }
        if (previousTouchCount == 2 && Input.touchCount < 2) // Detecta cuando los dedos se levantan de 2 a menos de 2
        {
            ChunkJump(); // Realiza un salto autom�tico
        }
        previousTouchCount = Input.touchCount; // Actualiza el contador de toques para el pr�ximo frame
    }


    void AjustarLaGravedad()
    {
        bool isJumpingUpward = _rb.velocity.y > 0;
        // Al presionar "P", reduce la gravedad para ralentizar la ca�da
        if (isTouchInputActive && Input.touchCount == 2 && isJumpingUpward)
        {
            _rb.gravityScale = slowGravityScale;
        }
        else if (isKeyboardInputActive && Input.GetKey(KeyCode.P)&&isJumpingUpward){

            _rb.gravityScale = normalGravityScale;
        }
    }
    //Funcion que mueve la gravedad a 1
    public void GravityStartChange()
    {
        _rb.gravityScale = _normalGravityScale;
    }

    public void ChunkJump()
    {
        if (_rb.gravityScale == _normalGravityScale)
        {
            _rb.velocity = Vector2.zero;
            _rb.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
        }
    }
}

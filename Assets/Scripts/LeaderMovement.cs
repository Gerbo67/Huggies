using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float jumpForce = 4f;
    public float normalGravityScale = 1f;
    public float slowGravityScale = 0.001f;

    private bool canSlowMotionAndJump = false;

    private bool isKeyboardInputActive = false;
    private bool isTouchInputActive = false;
    private int previousTouchCount = 0;
    private bool shouldJump = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    void Update()
    {
        MovimientoTeclado();
        MovimientoTouch();
        //AjustarLaGravedad();
    }

    void MovimientoTeclado()
    {
        bool isMovingDownward = _rb.velocity.y > 0; // Verifica si el movimiento es hacia abajo
        if (Input.GetKeyDown(KeyCode.P) && isMovingDownward)
        {
            isKeyboardInputActive = true;
            _rb.gravityScale = slowGravityScale;
            shouldJump = true;
        }
        else if (Input.GetKeyUp(KeyCode.P) && shouldJump)
        {
            isKeyboardInputActive = false;
            shouldJump = false;
            ChunkJump();
        }
    }

    void MovimientoTouch()
    {
        bool isMovingDownward = _rb.velocity.y > 0; // Verifica si el movimiento es hacia abajo
        if (Input.touchCount == 2 && isMovingDownward)
        {
            isTouchInputActive = true;
            _rb.gravityScale = slowGravityScale;
            shouldJump = true;
        }
        else if (previousTouchCount == 2 && Input.touchCount < 2 && shouldJump)
        {
            isTouchInputActive = false;
            shouldJump = false;
            ChunkJump();
        }
        previousTouchCount = Input.touchCount;
    }

   /* void AjustarLaGravedad()
    {
        // Si no hay entradas activas, restablece la gravedad a normal
        if (!isKeyboardInputActive && !isTouchInputActive)
        {
            _rb.gravityScale = 0;
        }
    }*/

    public void ChunkJump()
    {
        _rb.velocity = Vector2.zero;
        _rb.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
        _rb.gravityScale = normalGravityScale;
    }

    public void GravityStartChange()
    {
        _rb.gravityScale = normalGravityScale;
    }

    public void EnableSlowMotionAndJump(bool enable)
    {
        canSlowMotionAndJump = enable;
        if (!enable)
        {
            _rb.gravityScale = normalGravityScale;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float jumpForce = 5f;
    private float _normalGravityScale = 1f;
    private float _slowGravityScale = 0.1f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        bool isJumpingUpward = _rb.velocity.y < 0;
        // Al presionar "P", reduce la gravedad para ralentizar la caída
        if (Input.GetKey(KeyCode.P)&& !isJumpingUpward)
        {
            _rb.gravityScale = _slowGravityScale;
        }
        else
        {
            // Restablece a la gravedad normal cuando "P" no está presionada
            _rb.gravityScale = _normalGravityScale;
        }

        // Control adicional para manejar el salto o la caída
        if (Input.GetKeyDown(KeyCode.Space)) // Puedes cambiar esto según cómo manejes el salto
        {
            ChunkJump();
        }
        */
    }
    
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

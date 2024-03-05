using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float jumpForce = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ChunkJump()
    {
        _rb.velocity = Vector2.zero;
        _rb.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
    }
}

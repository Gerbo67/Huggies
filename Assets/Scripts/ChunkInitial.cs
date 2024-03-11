using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkInitial : MonoBehaviour
{
    private Rigidbody2D _rb;

    private Rigidbody2D _leaderRb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0; // Desactiva la gravedad

        // Intenta obtener el componente Rigidbody2D del líder
        _leaderRb = GameObject.FindGameObjectWithTag("LeaderTag").GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        _rb.velocity = _leaderRb.velocity;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DestructorTag"))
        {
            Destroy(gameObject); // Destruye el chunk al colisionar con el destructor

            // Genera un nuevo chunk
            GameObject chunk = GameObject.FindGameObjectWithTag("CreatorTag");

            if (chunk != null)
            {
                chunk.GetComponent<ChunkSpawner>().SpawnChunk();
            }
        }
    }
}

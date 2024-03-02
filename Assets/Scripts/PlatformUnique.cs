using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUnique : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
<<<<<<< Updated upstream
        
=======
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 normal = collision.contacts[0].normal;

            if (normal == Vector2.down)
            {
                //print
                Debug.Log("Player collided with platform");
                // Get object with tag
                GameObject[] chunk = GameObject.FindGameObjectsWithTag("Chunk");

                // Jump all chunks
                foreach (GameObject chunkObject in chunk)
                {
                    chunkObject.GetComponent<ChunkMover>().ChunkJump();
                }
            }
        }
>>>>>>> Stashed changes
    }


    // Update is called once per frame
    void Update()
    {
    }
}
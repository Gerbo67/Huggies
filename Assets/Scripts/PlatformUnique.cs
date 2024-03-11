using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUnique : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 normal = collision.contacts[0].normal;

            if (normal == Vector2.down)
            {
                // Get object with tag
                GameObject leader = GameObject.FindGameObjectWithTag("LeaderTag");
                GameObject creator = GameObject.FindGameObjectWithTag("CreatorTag");

                leader.GetComponent<LeaderMovement>().ChunkJump();
                creator.GetComponent<ChunkSpawner>().ReiniciarTiempo();
            }
        }
    }
}
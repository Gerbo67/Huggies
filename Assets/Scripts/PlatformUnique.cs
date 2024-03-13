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
                // Corrección aquí: Usar FindGameObjectWithTag para obtener un solo GameObject
                GameObject leader = GameObject.FindGameObjectWithTag("LeaderTag");
                if (leader != null)
                {
                    // Asegúrate de que el GameObject 'leader' tenga un componente 'LeaderMovement' antes de llamar a 'ChunkJump'
                    LeaderMovement leaderMovement = leader.GetComponent<LeaderMovement>();
                    if (leaderMovement != null)
                    {
                        leaderMovement.ChunkJump();
                    }
                    // Suponiendo que quieras llamar a 'ReiniciarTiempo' en otro GameObject etiquetado como 'CreatorTag'
                    GameObject creator = GameObject.FindGameObjectWithTag("CreatorTag");
                    ChunkSpawner chunkSpawner = creator?.GetComponent<ChunkSpawner>();
                    chunkSpawner?.ReiniciarTiempo();
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Corrección: Cambiar FindGameObjectsWithTag por FindGameObjectWithTag
            GameObject leader = GameObject.FindGameObjectWithTag("LeaderTag");
            if (leader != null)
            {
                // Obtiene el componente LeaderMovement y llama a EnableSlowMotionAndJump con false
                LeaderMovement leaderMovement = leader.GetComponent<LeaderMovement>();
                if (leaderMovement != null)
                {
                    leaderMovement.EnableSlowMotionAndJump(false);
                }
            }
        }
    }

}
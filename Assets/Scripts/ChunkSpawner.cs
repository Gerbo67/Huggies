using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    public GameObject chunkPrefab;
    public float spawnRate = 2f; // Tiempo en segundos entre spawns
    private float nextTimeToSpawn = 5f;

    void Update()
    {
        if (Time.time >= nextTimeToSpawn)
        {
<<<<<<< Updated upstream
            SpawnChunk();
            nextTimeToSpawn = Time.time + 1f / spawnRate;
        }
    }

    void SpawnChunk()
    {
        // Asumiendo que quieres spawnearlos en una posición Y específica y centrados en X
        float spawnY = Camera.main.orthographicSize + 10; // Un poco por encima de la vista de la cámara
        float spawnX = 0; // Centrado, ajusta según tu juego
=======
            SpawnChunk(true);
            isInitial = false;
        }
    }

    public void SpawnChunk(bool isInitial)
    {
        if (isInitial)
        {
            for (int i = 0; i < 3; i++)
            {
                // Generar spawn jump 10, -20, -10 debajo de Y
                float spawnY = 0;

                switch (i)
                {
                  case 0:
                      spawnY = 6;
                      break;
                  case 1:
                      spawnY = -1;
                      break;
                  case 2:
                      spawnY = 2;
                      break;
                }
                
                float spawnX = 0; // Centrado, ajusta según tu juego
            
                Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
                if (chunkPrefab != null)
                    Instantiate(chunkPrefab, spawnPosition, Quaternion.identity);
            }

            //
        }
        else
        {
            // Asumiendo que quieres spawnearlos en una posición Y específica y centrados en X
            float spawnY = Camera.main.orthographicSize + 10; // Un poco por encima de la vista de la cámara
            float spawnX = 0; // Centrado, ajusta según tu juego
>>>>>>> Stashed changes

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
        if (chunkPrefab != null)
            Instantiate(chunkPrefab, spawnPosition, Quaternion.identity);
    }
}
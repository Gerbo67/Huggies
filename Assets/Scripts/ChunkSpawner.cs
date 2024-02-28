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
            SpawnChunk();
            nextTimeToSpawn = Time.time + 1f / spawnRate;
        }
    }

    void SpawnChunk()
    {
        // Asumiendo que quieres spawnearlos en una posición Y específica y centrados en X
        float spawnY = Camera.main.orthographicSize + 10; // Un poco por encima de la vista de la cámara
        float spawnX = 0; // Centrado, ajusta según tu juego

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
        if (chunkPrefab != null)
            Instantiate(chunkPrefab, spawnPosition, Quaternion.identity);
    }
}
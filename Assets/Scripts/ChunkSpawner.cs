using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    public GameObject chunkPrefab;
    private bool _isInitial = true;
    private float _lastChunkTopY = -3;

    void Update()
    {
        if (_isInitial)
        {
            SpawnInitialChunks();
            _isInitial = false;
        }
    }


    private void SpawnInitialChunks()
    {
        int initialChunksCount = 5;
        for (int i = 0; i < initialChunksCount; i++)
        {
            SpawnChunk();
        }
    }

    public void SpawnChunk()
    {
        if (_isInitial)
        {
            if (chunkPrefab != null)
            {
                GameObject newChunk = Instantiate(chunkPrefab, Vector3.zero, Quaternion.identity);
                float chunkHeight = newChunk.transform.localScale.y;

                // Coloca el chunk en la posición correcta basada en la posición del último chunk y su altura
                float spawnY = _lastChunkTopY + chunkHeight / 2;
                newChunk.transform.position = new Vector3(0, spawnY, 0); // Centrado en X, ajusta según tu juego

                // Actualizamos lastChunkTopY para el próximo chunk
                _lastChunkTopY = spawnY + chunkHeight / 2;
            }
        }
        else
        {
            // Asumiendo que quieres spawnearlos en una posición Y específica y centrados en X
            float spawnY = Camera.main.orthographicSize + 10; // Un poco por encima de la vista de la cámara
            float spawnX = 0; // Centrado, ajusta según tu juego


            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
            if (chunkPrefab != null)
                Instantiate(chunkPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
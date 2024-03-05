using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChunkMover : MonoBehaviour
{
    public GameObject platformPrefab;
    public float jumpForce = 5f;
    public int gridWidth = 4;
    public int gridHeight = 3;
    public float margin = 0.5f;
    private float _gridBoxWidth = 0;
    private float _gridBoxHeight = 0;
    private List<Vector2> _gridPositions = new List<Vector2>();
    private Rigidbody2D _rb;

    private Rigidbody2D leaderRb; // Variable para almacenar el Rigidbody2D del líder


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0; // Desactiva la gravedad

        // Intenta obtener el componente Rigidbody2D del líder
        leaderRb = GameObject.FindGameObjectWithTag("LeaderTag").GetComponent<Rigidbody2D>();

        GenerateGrid();
        PlacePlatforms();
    }

    private void FixedUpdate()
    {
        // Usa el Rigidbody2D obtenido del líder
        if (leaderRb != null)
        {
            _rb.velocity = leaderRb.velocity;
        }
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
                chunk.GetComponent<ChunkSpawner>().SpawnChunk(false);
            }
        }
    }

    void GenerateGrid()
    {
        // Calcula el tamaño disponible para el grid descontando los márgenes
        float chunkWidth = transform.localScale.x - margin * (gridWidth + 1);
        float chunkHeight = transform.localScale.y - margin * (gridHeight + 1);

        // Calcula el tamaño de cada cuadro del grid
        _gridBoxWidth = chunkWidth / gridWidth;
        _gridBoxHeight = chunkHeight / gridHeight;

        // Genera las posiciones de cada cuadro del grid
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                float posX = (-transform.localScale.x / 2) + margin + (_gridBoxWidth / 2) +
                             (x * (_gridBoxWidth + margin));
                float posY = (-transform.localScale.y / 2) + margin + (_gridBoxHeight / 2) +
                             (y * (_gridBoxHeight + margin));
                _gridPositions.Add(new Vector2(posX, posY));
            }
        }
    }

    void PlacePlatforms()
    {
        HashSet<int> usedColumns = new HashSet<int>(); // Guarda las columnas usadas
        HashSet<float> usedYPositions = new HashSet<float>(); // Guarda las posiciones Y usadas
        List<Vector2> availablePositions = new List<Vector2>(_gridPositions); // Copia las posiciones del grid.

        for (int i = 0; i < gridHeight; i++) // Intenta asignar una plataforma por fila como ejemplo.
        {
            List<Vector2> possiblePositions = new List<Vector2>();
            foreach (var pos in availablePositions)
            {
                int column = (int)((pos.x + transform.localScale.x / 2) / (_gridBoxWidth + margin)) % gridWidth;
                if (!usedColumns.Contains(column) &&
                    !usedYPositions.Contains(pos.y)) // Verifica si la columna y la posición Y no han sido usadas
                {
                    possiblePositions.Add(pos);
                }
            }

            if (possiblePositions.Count > 0)
            {
                Vector2 gridPosition = possiblePositions[Random.Range(0, possiblePositions.Count)];
                GameObject platform = Instantiate(platformPrefab,
                    transform.position + new Vector3(gridPosition.x, gridPosition.y, 0), Quaternion.identity,
                    transform);

                // Ajusta el tamaño de la plataforma
                platform.transform.localScale = new Vector3(_gridBoxWidth - margin * (gridWidth) / (gridWidth - 1),
                    _gridBoxHeight - margin * (gridHeight) / (gridHeight - 1.2f), 1);

                // Ajuste tamaño del collider
                BoxCollider2D platformCollider = platform.GetComponent<BoxCollider2D>();
                if (platformCollider != null)
                {
                    // El tamaño del collider debe ser ajustado para coincidir con la escala visual de la plataforma.
                    // Como la plataforma ya ha sido escalada, establecemos el tamaño del collider para que coincida con esa escala.
                    platformCollider.size = new Vector2(1, 1); // Restablece a 1, 1 como base para el cálculo.
                }


                int usedColumn = (int)((gridPosition.x + transform.localScale.x / 2) / (_gridBoxWidth + margin)) %
                                 gridWidth; // Calcula la columna usada
                usedColumns.Add(usedColumn); // Añade la columna usada al conjunto
                usedYPositions.Add(gridPosition.y); // Añade la posición Y usada al conjunto

                availablePositions.Remove(gridPosition); // Elimina la posición usada de la lista de disponibles.
            }
        }
    }
}
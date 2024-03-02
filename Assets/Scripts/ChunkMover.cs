using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMover : MonoBehaviour
{
<<<<<<< Updated upstream
    public GameObject platformPrefab; // Referencia al prefab de la plataforma
    public float separation = 140f; // Separación vertical entre plataformas
    public float speed = 5f;

    void Start()
    {
        // Genera las dos plataformas dentro del chunk
        GeneratePlatforms();
=======
    public GameObject platformPrefab;
    public float jumpForce = 5f;
    public int gridWidth = 4;
    public int gridHeight = 3;
    public float margin = 0.5f;
    private float _gridBoxWidth = 0;
    private float _gridBoxHeight = 0;
    private List<Vector2> _gridPositions = new List<Vector2>();
    private Rigidbody2D _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        GenerateGrid();
        PlacePlatforms();
>>>>>>> Stashed changes
    }

    void Update()
    {
<<<<<<< Updated upstream
        // Mueve el chunk hacia abajo
        transform.Translate(Vector3.down * speed * Time.deltaTime);
=======
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        float baseY = -20f; 

        float rangeY = 10f;

        float posY1 = baseY + Random.Range(5, rangeY);

        float posY2 = posY1 + separation;

        // Crear posiciones de plataforma en relación al creador
        Vector3 platformPosition1 = new Vector3(0, -posY1, 0);
        Vector3 platformPosition2 = new Vector3(0, -posY2, 0);

        // Instanciar las plataformas en relación al chunk
        Instantiate(platformPrefab,  platformPosition1, Quaternion.identity, transform);
        Instantiate(platformPrefab, platformPosition2, Quaternion.identity, transform);
=======
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
                if (!usedColumns.Contains(column) && !usedYPositions.Contains(pos.y)) // Verifica si la columna y la posición Y no han sido usadas
                {
                    possiblePositions.Add(pos);
                }
            }

            if (possiblePositions.Count > 0)
            {
                Vector2 gridPosition = possiblePositions[Random.Range(0, possiblePositions.Count)];
                GameObject platform = Instantiate(platformPrefab, transform.position + new Vector3(gridPosition.x, gridPosition.y, 0), Quaternion.identity, transform);
        
                // Ajusta el tamaño de la plataforma
                platform.transform.localScale = new Vector3(_gridBoxWidth - margin * (gridWidth) / (gridWidth - 1), _gridBoxHeight - margin * (gridHeight) / (gridHeight - 1.2f), 1);

                // Ajuste tamaño del collider
                BoxCollider2D platformCollider = platform.GetComponent<BoxCollider2D>();
                if (platformCollider != null)
                {
                    // El tamaño del collider debe ser ajustado para coincidir con la escala visual de la plataforma.
                    // Como la plataforma ya ha sido escalada, establecemos el tamaño del collider para que coincida con esa escala.
                    platformCollider.size = new Vector2(1, 1); // Restablece a 1, 1 como base para el cálculo.
                }


                int usedColumn = (int)((gridPosition.x + transform.localScale.x / 2) / (_gridBoxWidth + margin)) % gridWidth; // Calcula la columna usada
                usedColumns.Add(usedColumn); // Añade la columna usada al conjunto
                usedYPositions.Add(gridPosition.y); // Añade la posición Y usada al conjunto
        
                availablePositions.Remove(gridPosition); // Elimina la posición usada de la lista de disponibles.
            }
        }
    }



    
    public void ChunkJump()
    {
        _rb.velocity = Vector2.zero;
        _rb.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
>>>>>>> Stashed changes
    }
}
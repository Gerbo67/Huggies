using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMover : MonoBehaviour
{
    public GameObject platformPrefab; // Referencia al prefab de la plataforma
    public float separation = 140f; // Separación vertical entre plataformas
    public float jumpForce = 3f; // La fuerza del salto, ajusta según sea necesario
    private Rigidbody2D _rb;
    //public float speed = 5f;

    public float normalGravityScale = 1f;
    public float slowGravityScale = 0f;

    void Start()
    {
        // Genera las dos plataformas dentro del chunk
        _rb = GetComponent<Rigidbody2D>();
        GeneratePlatforms();
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            _rb.gravityScale = slowGravityScale;
        }
        else
        {
            _rb.gravityScale = normalGravityScale;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DestructorTag"))
        {
            Destroy(gameObject); // Destruye el chunk al colisionar con el destructor
        }
    }
    
    void GeneratePlatforms()
    {
        // La altura total del chunk basada en su escala
        float chunkHeight = 5f;

        // Crear posiciones de plataforma dentro del chunk
        // Asumiendo que el pivote del chunk está en el centro,
        // la posición Y de la plataforma superior será chunkHeight/2
        // y la posición Y de la plataforma inferior será -chunkHeight/2
        Vector3 upperPlatformPosition = new Vector3(0, chunkHeight / 2, 0);
        Vector3 lowerPlatformPosition = new Vector3(0, -chunkHeight / 2, 0);

        // Instanciar las plataformas en relación al chunk
        // Los vectores de posición son locales debido a que el último parámetro es 'transform'
        Instantiate(platformPrefab, upperPlatformPosition, Quaternion.identity, transform);
        Instantiate(platformPrefab, lowerPlatformPosition, Quaternion.identity, transform);
    }


    public void ChunkJump()
    {
        // Resetear la velocidad para que los impulsos anteriores no se acumulen
        _rb.velocity = Vector2.zero;

        // Aplicar una nueva fuerza de salto hacia arriba
        _rb.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
    }


}

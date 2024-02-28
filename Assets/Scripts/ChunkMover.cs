using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMover : MonoBehaviour
{
    public GameObject platformPrefab; // Referencia al prefab de la plataforma
    public float separation = 140f; // Separación vertical entre plataformas
    public float speed = 5f;

    void Start()
    {
        // Genera las dos plataformas dentro del chunk
        GeneratePlatforms();
    }
    
    void Update()
    {
        // Mueve el chunk hacia abajo
        transform.Translate(Vector3.down * speed * Time.deltaTime);
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
    }


}

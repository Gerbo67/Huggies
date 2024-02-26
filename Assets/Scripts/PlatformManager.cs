using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject platformPrefab; // Asigna este prefab en el editor de Unity
    public float upwardSpeed = 4f; // Velocidad base a la que se mueven las plataformas hacia arriba
    public List<GameObject> platforms = new List<GameObject>();
    private float directionChangeTimer = 0f;
    private bool isMovingDown = false;
    private float timeSinceDirectionChange = 0f; // Controla la dirección del movimiento

    void Start()
    {
        // Asumiendo que usas una cámara ortográfica
        float cameraHeight = Camera.main.orthographicSize * 2;
        float pixelToUnit = cameraHeight / Screen.height; // Conversión de píxeles a unidades de mundo
        float startY = -Camera.main.orthographicSize * 0.3f; // -30% de la altura de la cámara en unidades de mundo
        float spaceBetweenPlatforms = 140 * pixelToUnit; // Espacio entre plataformas en unidades de mundo

        // Crear y posicionar las 3 plataformas iniciales
        for (int i = 0; i < 5; i++)
        {
            Vector3 position = new Vector3(0, startY + (i * spaceBetweenPlatforms), 0); // Posición para cada plataforma
            GameObject platform = Instantiate(platformPrefab, position, Quaternion.identity); // Crear plataforma
            platform.AddComponent<PlatformUnique>();
            platforms.Add(platform); // Añadir a la lista de plataformas
        }
    }

    void Update()
    {
        if (directionChangeTimer > 0)
        {
            directionChangeTimer -= Time.deltaTime;
            timeSinceDirectionChange += Time.deltaTime; // Incrementa el tiempo desde el último cambio de dirección
        }
        else
        {
            isMovingDown = false;
        }

        Vector3 movementDirection = isMovingDown ? Vector3.down : Vector3.up;

        // Modificar la velocidad basándose en la dirección
        float speedMultiplier;
        const float initialSpeedFactor = 2f; // Factor de velocidad inicial común para ambas direcciones
        if (isMovingDown)
        {
            // Hace que la bajada sea inicialmente rápida y luego desacelere.
            // La desaceleración se hace menos pronunciada ajustando el factor en el denominador.
            speedMultiplier =
                Mathf.Max(initialSpeedFactor / (1 + timeSinceDirectionChange), 1f); // Asegura un mínimo de velocidad
        }
        else
        {
            // Hace que la subida acelere con el tiempo partiendo de una velocidad inicial.
            speedMultiplier = initialSpeedFactor + timeSinceDirectionChange; // Acelera linealmente con el tiempo
        }

        float currentSpeed = upwardSpeed * speedMultiplier;

        foreach (GameObject platform in platforms)
        {
            if (platform != null)
            {
                platform.transform.position += movementDirection * (currentSpeed * Time.deltaTime);
            }
        }
    }

    public void ChangeDirectionTemporarily()
    {
        directionChangeTimer = 0.5f; // Reiniciar el temporizador a 1 segundo (ajustado desde 3 segundos según tu código)
        isMovingDown = !isMovingDown; // Invertir la dirección
        timeSinceDirectionChange = 0f; // Reiniciar el tiempo desde el cambio de dirección cada vez

        // Calcular la posición para la nueva plataforma, que debería agregarse en el principio de la lista
        float cameraHeight = Camera.main.orthographicSize * 2;
        float pixelToUnit = cameraHeight / Screen.height; // Conversión de píxeles a unidades de mundo
        float spaceBetweenPlatforms = 140 * pixelToUnit; // Espacio entre plataformas en unidades de mundo
        Vector3 position;

        if (platforms.Count > 0)
        {
            // Si ya hay plataformas en la lista, posiciona la nueva plataforma 140 unidades de mundo por encima de la más alta
            float highestY = platforms[0].transform.position.y;
            for (int i = 1; i < platforms.Count; i++)
            {
                if (platforms[i].transform.position.y > highestY)
                {
                    highestY = platforms[i].transform.position.y;
                }
            }

            position = new Vector3(0, highestY + spaceBetweenPlatforms, 0);
        }
        else
        {
            // Si no hay plataformas, coloca la nueva plataforma en la posición inicial
            float startY = -Camera.main.orthographicSize * 0.3f; // -30% de la altura de la cámara en unidades de mundo
            position = new Vector3(0, startY, 0);
        }

        // Crear una nueva plataforma y añadirla al principio de la lista
        GameObject newPlatform = Instantiate(platformPrefab, position, Quaternion.identity);
        newPlatform.AddComponent<PlatformUnique>();
        platforms.Insert(0, newPlatform); // Añadir al principio de la lista
    }

    public void RemovePlatform()
    {
        //Debug.Log("Removing platform");
        if(platforms.Count > 0)
        {
            GameObject platformToRemove = platforms[platforms.Count - 1];
            PlatformUnique uniqueComponent = platformToRemove.GetComponent<PlatformUnique>();
            if (uniqueComponent != null)
            {
                uniqueComponent.enabled = false; // Desactiva el componente antes de destruir
            }
            // Posiblemente desactivar otros componentes aquí
            Destroy(platformToRemove);
            platforms.RemoveAt(platforms.Count - 1);
        }

    }

}
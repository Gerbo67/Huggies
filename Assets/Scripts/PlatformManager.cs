using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject platformPrefab;
    public int poolSize = 10; // Tamaño del pool de plataformas
    public float playerOffsetY = 1f; // Ajuste para colocar el jugador encima de la plataforma

    private GameObject[] platformPool;
    private GameObject player;
    private GameObject initialPlatform;
    private float screenHeightInUnits;
    private float screenWidthInUnits;

    // Start is called before the first frame update
    void Start()
    {
        screenHeightInUnits = Camera.main.orthographicSize * 2;
        screenWidthInUnits = screenHeightInUnits * Screen.width / Screen.height;
        ResizePrefabs();
        CreatePlatformPool();
        SpawnInitialPlatformAndPlayer();
        StartGeneratingPlatforms();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ResizePrefabs()
    {
        // Dimensiones para el jugador
        Vector2 playerSize = new Vector2(screenHeightInUnits * 0.07f, screenHeightInUnits * 0.12f);
        playerPrefab.transform.localScale =
            new Vector3(playerSize.x / playerPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.x,
                playerSize.y / playerPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.y, 1);

        // Dimensiones para las plataformas
        Vector2 platformSize = new Vector2(screenWidthInUnits * 0.32f, screenHeightInUnits * 0.04f);
        platformPrefab.transform.localScale =
            new Vector3(platformSize.x / platformPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.x,
                platformSize.y / platformPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.y, 1);
    }

    void CreatePlatformPool()
    {
        platformPool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            platformPool[i] = Instantiate(platformPrefab, new Vector3(-100, -100, 0), Quaternion.identity);
            platformPool[i].SetActive(false);
        }
    }

    void SpawnInitialPlatformAndPlayer()
    {
        // Calcular la posición inicial de la plataforma basada en el porcentaje de la altura de la pantalla
        float initialPlatformY = -screenHeightInUnits * 0.3f;
        initialPlatform = Instantiate(platformPrefab, new Vector3(0, initialPlatformY, 0), Quaternion.identity);

        // Colocar al jugador encima de la plataforma inicial
        float playerY = initialPlatformY + playerOffsetY;
        player = Instantiate(playerPrefab, new Vector3(0, playerY, 0), Quaternion.identity);
    }

    void StartGeneratingPlatforms()
    {
        // Altura entre plataformas basada en el porcentaje de la pantalla
        float stepY = screenHeightInUnits * 0.05f;

        for (int i = 0; i < poolSize; i++)
        {
            // Obtén el ancho del sprite de la plataforma.
            SpriteRenderer spriteRenderer = platformPrefab.GetComponent<SpriteRenderer>();
            float platformWidth = spriteRenderer.sprite.bounds.size.x * platformPrefab.transform.localScale.x;
        
            // Asegúrate de que el pivot del sprite esté en el borde si quieres alinearlas perfectamente.
            float posX = screenWidthInUnits / 2 - platformWidth / 2; // Posición para el lado derecho de la pantalla
            if (i % 2 == 1) // Si el índice es impar, coloca la plataforma en el lado izquierdo
            {
                posX = -posX;
            }

            // Calcula la posición Y de la nueva plataforma
            float posY = initialPlatform.transform.position.y + (i + 1) * stepY;

            GameObject platform = GetPlatformFromPool();
            platform.transform.position = new Vector3(posX, posY, 0);
        }
    }


    GameObject GetPlatformFromPool()
    {
        foreach (GameObject platform in platformPool)
        {
            if (!platform.activeInHierarchy)
            {
                platform.SetActive(true);
                return platform;
            }
        }

        // Si todas las plataformas están en uso, opcionalmente crea una nueva o espera hasta que una esté disponible
        return null; // Aquí podrías manejar la lógica para esperar o crear una nueva plataforma
    }
}
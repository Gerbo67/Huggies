using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject platformPrefab;
    public int poolSize = 3;

    private GameObject[] _platformPool;
    private GameObject _player;
    private GameObject _initialPlatform;
    private float _screenHeightInUnits;
    private float _screenWidthInUnits;
    private Rigidbody2D _playerRigidbody;
    private float _heightPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _screenHeightInUnits = InitialGame.GetHeight();
        _screenWidthInUnits = InitialGame.GetWidth();
        CreatePlatformPool();
        SpawnInitialPlatformAndPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatformsBasedOnPlayer();
    }

    void MovePlatformsBasedOnPlayer()
    {
        float playerVelocityY = _playerRigidbody.velocity.y;

        // Si el jugador está subiendo (saltando), mueve las plataformas hacia abajo para simular el ascenso
        if (playerVelocityY > 0)
        {
            float moveSpeed = playerVelocityY * 2; // Move twice as fast when going up
            foreach (GameObject platform in _platformPool)
            {
                if (platform.activeInHierarchy)
                {
                    platform.transform.position += Vector3.down * (moveSpeed * Time.deltaTime);
                }
            }
        }
        // Si el jugador está cayendo, opcionalmente podrías mover las plataformas hacia arriba para simular la caída
        else if (playerVelocityY < 0)
        {
            foreach (GameObject platform in _platformPool)
            {
                if (platform.activeInHierarchy)
                {
                    platform.transform.position += Vector3.up * (Mathf.Abs(playerVelocityY) * Time.deltaTime);
                }
            }
        }

        // Recicla y reposiciona plataformas que salen de la vista
        //RecycleAndRepositionPlatforms();
    }


    void CreatePlatformPool()
    {
        _platformPool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            _platformPool[i] = Instantiate(platformPrefab, new Vector3(-100, -400, 0), Quaternion.identity);
            _platformPool[i].SetActive(false);
        }
    }

    void SpawnInitialPlatformAndPlayer()
    {
        // Calcular la posición inicial de la plataforma basada en el porcentaje de la altura de la pantalla
        float initialPlatformY = -_screenHeightInUnits * 0.4f;

        StartGeneratingPlatforms(initialPlatformY);

        // Colocar al jugador encima de la plataforma inicial
        float playerY = initialPlatformY + (InitialGame.GetHeightPlayer() * 2) +
                        (InitialGame.GetHeightPlatform() * 2 + (InitialGame.GetHeightPlayer() * 0.1f));
        _player = Instantiate(playerPrefab, new Vector3(0, playerY, 0), Quaternion.identity);
        _playerRigidbody = _player.GetComponent<Rigidbody2D>();
    }

    void StartGeneratingPlatforms(float initialPlatformY)
    {
        // Altura entre plataformas basada en el porcentaje de la pantalla
        float stepY = InitialGame.GetHeightPlayer() * 2; // Aumenta al 10% de la altura de la pantalla

        for (int i = 0; i < poolSize; i++)
        {
            // Obtén el ancho del sprite de la plataforma.
            SpriteRenderer spriteRenderer = platformPrefab.GetComponent<SpriteRenderer>();
            float platformWidth = spriteRenderer.sprite.bounds.size.x * platformPrefab.transform.localScale.x;

            // Asegúrate de que el pivot del sprite esté en el borde si quieres alinearlas perfectamente.
            float posX = 0; // Posición para el lado derecho de la pantalla

            // Calcula la posición Y de la nueva plataforma
            float posY = initialPlatformY + (i + 1) * stepY;

            GameObject platform = GetPlatformFromPool();
            platform.transform.position = new Vector3(posX, posY, 0);
        }
    }


    GameObject GetPlatformFromPool()
    {
        foreach (GameObject platform in _platformPool)
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
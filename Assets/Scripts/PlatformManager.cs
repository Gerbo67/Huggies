using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private bool isColliding = false;
    public float reboundSpeed = 5f; // Velocidad inicial del rebote
    public float decelerationRate = 0.95f;

    // Start is called before the first frame update
    void Start()
    {
        _screenHeightInUnits = InitialGame.GetHeight();
        _screenWidthInUnits = InitialGame.GetWidth();
        CreatePlatformPool();
        SpawnInitialPlatformAndPlayer();

        Button btn = GameObject.Find("BtnStart").GetComponent<Button>();
        btn.onClick.AddListener(MovePlatformsBasedOnPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatformsBasedOnPlayer();
    }

    public void NotifyCollision()
    {
        Debug.Log("Collision detected 2");
        isColliding = true;
        reboundSpeed = 5f; // Restablece la velocidad de rebote
    }

    void MovePlatformsBasedOnPlayer()
    {
        foreach (GameObject platform in _platformPool)
        {
            if (platform.activeInHierarchy)
            {
                if (isColliding)
                {
                    // Mueve las plataformas hacia abajo y desacelera
                    platform.transform.position += Vector3.down * (reboundSpeed * Time.deltaTime);
                    reboundSpeed *= decelerationRate; // Desaceleraci�n

                    // Si la velocidad es muy baja, det�n el movimiento y prepara para mover hacia arriba
                    if (reboundSpeed < 0.1f)
                    {
                        isColliding = false;
                    }
                }
                else
                {
                    // Mueve las plataformas hacia arriba a velocidad constante
                    platform.transform.position += Vector3.up * (10f * Time.deltaTime);
                }
            }
        }
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
        // Calcular la posici�n inicial de la plataforma basada en el porcentaje de la altura de la pantalla
        StartGeneratingPlatforms();

        float initialPlatformY = -_screenHeightInUnits * 0.2f;

        // Colocar al jugador encima de la plataforma inicial
        float playerY = initialPlatformY + (InitialGame.GetHeightPlayer() * 2) +
                        (InitialGame.GetHeightPlatform() * 2 + (InitialGame.GetHeightPlayer() * 0.1f));
        _player = Instantiate(playerPrefab, new Vector3(0, playerY, 0), Quaternion.identity);
        _playerRigidbody = _player.GetComponent<Rigidbody2D>();
    }

    void StartGeneratingPlatforms()
    {
        float initialPlatformY = -_screenHeightInUnits * 0.9f;
        // Altura entre plataformas basada en el porcentaje de la pantalla
        float stepY = InitialGame.GetHeightPlayer() * 2; // Aumenta al 10% de la altura de la pantalla

        for (int i = 0; i < poolSize; i++)
        {
            // Obt�n el ancho del sprite de la plataforma.
            SpriteRenderer spriteRenderer = platformPrefab.GetComponent<SpriteRenderer>();
            float platformWidth = spriteRenderer.sprite.bounds.size.x * platformPrefab.transform.localScale.x;

            // Aseg�rate de que el pivot del sprite est� en el borde si quieres alinearlas perfectamente.
            float posX = 0; // Posici�n para el lado derecho de la pantalla

            // Calcula la posici�n Y de la nueva plataforma
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

        // Si todas las plataformas est�n en uso, opcionalmente crea una nueva o espera hasta que una est� disponible
        return null; // Aqu� podr�as manejar la l�gica para esperar o crear una nueva plataforma
    }
}
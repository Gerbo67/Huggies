using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialGame : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject playerPrefab;
    public GameObject chunkPrefab;

    private static float _screenHeightInUnits;
    private float _screenWidthInUnits;
    private float _heightPlayer;
    private float _heightPlatform;
    
    void Start()
    {
        ResizePrefabs();
    }
    
    public static float GetHeight()
    {
        return Camera.main.orthographicSize * 2;
    }
    
    public static float GetWidth()
    {
        return GetHeight() * Screen.width / Screen.height;
    }

    public static float GetHeightPlayer()
    {
        return GetHeight() * 0.12f;
    }
    
    public static float GetHeightPlatform()
    {
        return GetHeight() * 0.04f;
    }

    void ResizePrefabs()
    {
        
        _heightPlayer = GetHeightPlayer();
        
        _heightPlatform = GetHeightPlatform();
        
        _screenHeightInUnits = GetHeight();
        
        _screenWidthInUnits = GetWidth();
        
        // Dimensiones para el jugador
        Vector2 playerSize = new Vector2(_screenHeightInUnits * 0.07f, _heightPlayer);
        playerPrefab.transform.localScale = new Vector3(
            playerSize.x / playerPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.x,
            playerSize.y / playerPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.y, 1);

        // Dimensiones para las plataformas
        Vector2 platformSize = new Vector2(_screenWidthInUnits * 0.32f, _screenHeightInUnits * 0.04f);
        platformPrefab.transform.localScale = new Vector3(
            platformSize.x / platformPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.x,
            platformSize.y / platformPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.y, 1);
        
        // Dimensiones para el chunk (contenedor de plataformas) ancho de la pantalla y 1/4 de la altura
        chunkPrefab.transform.localScale = new Vector3(
            _screenWidthInUnits / chunkPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.x,
            _screenHeightInUnits / 4 / chunkPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.y, 1);

        // Ajustar collider para el jugador
        BoxCollider2D playerCollider = playerPrefab.GetComponent<BoxCollider2D>();
        if (playerCollider != null)
        {
            float colliderHeightPlayer = playerSize.y * 0.05f; // 10% del alto del jugador
            float colliderWidthPlayer = playerSize.x; // El ancho del collider igual al ancho del personaje

            playerCollider.size = new Vector2(colliderWidthPlayer, colliderHeightPlayer);
            playerCollider.offset = new Vector2(0, -playerSize.y / 2 + colliderHeightPlayer / 2);
        }

        // Ajustar collider para la plataforma
        BoxCollider2D platformCollider = platformPrefab.GetComponent<BoxCollider2D>();
        if (platformCollider != null)
        {
            float colliderHeightPlatform = platformSize.y * 0.1f; // 10% del alto de la plataforma
            float colliderWidthPlatform = platformSize.x; // El ancho del collider igual al ancho de la plataforma

            platformCollider.size = new Vector2(colliderWidthPlatform, colliderHeightPlatform);
            platformCollider.offset = new Vector2(0, platformSize.y / 2 - colliderHeightPlatform / 2);
        }
        
        // Ajustar collider para el chunk
        BoxCollider2D chunkCollider = chunkPrefab.GetComponent<BoxCollider2D>();
        if (chunkCollider != null)
        {
            float colliderHeightChunk = _screenHeightInUnits / 4 * 0.1f; // 10% del alto del chunk
            float colliderWidthChunk = _screenWidthInUnits; // El ancho del collider igual al ancho de la pantalla

            chunkCollider.size = new Vector2(colliderWidthChunk, colliderHeightChunk);
            chunkCollider.offset = new Vector2(0, _screenHeightInUnits / 4 / 2 - colliderHeightChunk / 2);
        }
    }

    void Update()
    {
        
    }
}

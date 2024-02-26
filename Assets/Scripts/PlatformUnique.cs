using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUnique : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    //collider

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DestructorTag"))
        {
            PlatformManager
                platformManager =
                    FindObjectOfType<PlatformManager>();
            if (platformManager != null)
            {
                // Elimina esta plataforma específica
                platformManager.RemovePlatform();
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
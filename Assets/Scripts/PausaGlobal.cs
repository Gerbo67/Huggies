using UnityEngine;

public class GamePauseController : MonoBehaviour
{
    private bool isGamePaused = false;

    void Update()
    {
        // Detectar cuando se presiona la tecla "P"
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;

        // Si el juego está pausado, detenemos el tiempo. De lo contrario, lo reanudamos.
        Time.timeScale = isGamePaused ? 0 : 1;

        // Opcional: Detener/Reanudar también el audio para que coincida con el estado de pausa.
        AudioListener.pause = isGamePaused;
    }
}

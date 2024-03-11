using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject canvasStart;
    public GameObject canvasEnd;

    public void StartGameButton()
    {
        GameObject leader = GameObject.FindGameObjectWithTag("LeaderTag");
        GameObject creator = GameObject.FindGameObjectWithTag("CreatorTag");

        leader.GetComponent<LeaderMovement>().GravityStartChange();
        creator.GetComponent<ChunkSpawner>().isStartCountTime();

        canvasStart.SetActive(false);
    }

    public void ShowButtonRestart()
    {
        canvasEnd.SetActive(true);
    }

    public void RestartGameButton()
    {
        string sceneActual = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(sceneActual);
    }
}
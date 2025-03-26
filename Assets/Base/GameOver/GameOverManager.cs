using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public int sceneRetry;
    public int sceneMainMenu;
    public void Retry()
    {
        SceneManager.LoadScene(sceneRetry);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(sceneMainMenu);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameUI;
    public GameObject upgradeUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        gameUI.SetActive(true);
        upgradeUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        gameUI.SetActive(false);
        upgradeUI.SetActive(false);
        isGamePaused = true;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private const int MENU_SCENE_ID = 0;
    private const int GAME_SCENE_ID = 1;
    public static bool isPaused;

    [SerializeField]
    private GameObject _pauseMenuUI;
    [SerializeField]
    private GameObject _endMenuUI;

    [SerializeField]
    private TMPro.TMP_Text _scoreText;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
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
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void Pause()
    {
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void EndGame(int waveReached)
    {
        _endMenuUI.SetActive(true);
        _scoreText.text = $"You reached wave {waveReached}!";
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MENU_SCENE_ID);
    }

    public void ReloadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(GAME_SCENE_ID);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    float gameOverDelay = 3f;
    GameSession gameSession;

    private void Start()
    {

    }
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        gameSession = FindObjectOfType<GameSession>();
        if (gameSession != null)
        {
            gameSession.ResetGame();
        }
        SceneManager.LoadScene(1);
    }

    public void LoadGameOver()
    {
        StartCoroutine(GameOverDelay());
    }

    private IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene("Game Over");
    }

    public void LoadWinScreen()
    {
        StartCoroutine(WinScreen());
    }
    private IEnumerator WinScreen()
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene("Win Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

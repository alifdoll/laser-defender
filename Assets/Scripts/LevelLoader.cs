using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    [Header("Game Over Delay")]
    [SerializeField] float gameOverDelay = 2f;

    public void LoadStartMenu()
    {
        Destroy(GameObject.Find("Music Player"));
        SceneManager.LoadScene(0);
    }

    public void LoadMainGame()
    {
        SceneManager.LoadScene("Level 1");
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver()
    {
        StartCoroutine(GameOver());  
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

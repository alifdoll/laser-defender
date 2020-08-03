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
        Destroy(GameObject.Find("You Win"));
        Destroy(GameObject.Find("Music Player"));
        SceneManager.LoadScene(0);
    }

    public void LoadMainGame()
    {
        Destroy(GameObject.Find("You Win"));
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene("Level 1");
    }

    public void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(currentScene);
        SceneManager.LoadScene(currentScene + 1);
    }

    public void LoadGameOver()
    {
        StartCoroutine(GameOver());  
    }

    public void LoadPlayerWin()
    {
        Destroy(GameObject.Find("Music Player"));
        SceneManager.LoadScene("You Win");
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

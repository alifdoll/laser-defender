using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    [SerializeField] int numberOfEnemies = 0;

    private void Awake()
    {
        numberOfEnemies = FindObjectOfType<EnemySpawner>().GetEnemyCount();
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numOfSession = FindObjectsOfType<GameSession>().Length;
        if(numOfSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        
    }

   public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreVal)
    {
        score += scoreVal;
    }

    public void EnemyDestroyed()
    {
        numberOfEnemies--;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}

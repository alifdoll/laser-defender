using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    EnemySpawner enemySpawn;
    

    private void Awake()
    {
        SetUpSingleton();
        enemySpawn = FindObjectOfType<EnemySpawner>();
    }

    private void Update()
    {
        
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


    public void ResetGame()
    {
        Destroy(gameObject);
    }
}

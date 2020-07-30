using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{

    Text scoreText;
    GameSession gameSession = null;

 
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        scoreText = GetComponent<Text>();
    }

   
    void Update()
    {
        scoreText.text = gameSession.GetScore().ToString();
    }
}
